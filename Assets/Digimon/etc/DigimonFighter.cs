using System.Collections;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class DigimonFighter : MonoBehaviour {
        [Header("Components")]
        [SerializeField] private Collider2D _hitbox;
        [SerializeField] private DigimonController _controller;
        [SerializeField] private DigimonMovement _movement;
        [SerializeField] private DigimonAnim _anim;
        [SerializeField] private DigimonAudio _audio;
        [Header("Variables")]
        [SerializeField] private int _player;

        public Collider2D Hitbox => _hitbox;
        public DigimonController Controller => _controller;
        public DigimonMovement Movement => _movement;
        public DigimonAnim Anim => _anim;
        public DigimonAudio Audio => _audio;

        [SerializeField] private GameObject pPoint;

        public bool IsImmune { get; private set; }
        private float immunityDuration;

        public bool IsGuarded { get; set; }

        public int Player { get => _player; set => _player = value; }

        public float MaxHP => 100f;

        private float _currentHP;
        public float CurrentHP {
            get {
                return _currentHP;
            }
            protected set {
                if (value < 0) value = 0;
                else if (value > MaxHP) value = MaxHP;
                _currentHP = value;
            }
        }

        public float MaxEnergy => 5f;

        private float _currentEnergy;
        public float CurrentEnergy {
            get {
                return _currentEnergy;
            }
            protected set {
                if (value < 0) value = 0;
                else if (value > MaxEnergy) value = MaxEnergy;
                _currentEnergy = value;
            }
        }

        public float DamageMultiplier {
            get {
                float m = 1f;
                if (Enhanced) m *= 1.5f;
                if (IsSmashed) m *= Smash;
                return m;
            }
        }

        private int _currentPower = 0;

        public int CurrentPower {
            get {
                return _currentPower;
            }
            set {
                _currentPower = value;
                if (_currentPower >= 15) {
                    _currentPower = 15;
                    if (!Enhanced) {
                        Enhanced = true;
                    }
                }
                else if (_currentPower <= 0) {
                    _currentPower = 0;
                    if (Enhanced) {
                        Enhanced = false;
                    }
                }
            }
        }
        public bool Enhanced { get; protected set; } = false;

        public bool IsBeingHit { get; protected set; }

        private void Awake() {
            CurrentHP = MaxHP;
            CurrentEnergy = MaxEnergy;
        }

        private float enhancementDownturn = 0.75f;

        public bool Paralyzed { get; protected set; } = false;
        public bool Burned { get; protected set; } = false;
        public float Slow { get; protected set; } = 0f;
        public bool IsSlowed => Slow > 0f;
        public float Smash { get; protected set; } = 1f;
        public bool IsSmashed => Smash < 1f;

        public void ApplyParalyze(float duration) {
            if (!IsImmune && !Paralyzed) {
                Paralyzed = true;
                Movement.body.gravityScale = 0f;
                Movement.body.mass = 1000000;
                Movement.SetSpeed(new Vector2(0f, 0f));
                StartCoroutine(EndParalyze(duration));
            }
        }
        private IEnumerator EndParalyze(float duration) {
            yield return new WaitForSeconds(duration);
            Movement.body.gravityScale = 3f;
            Movement.body.mass = 1;
            Paralyzed = false;
        }

        public void ApplyBurn(float duration) {
            if (!IsImmune && !Burned) {
                Audio.Stop();
                Burned = true;
                Movement.SetSpeed(-2f, true);
                StartCoroutine(EndBurn(duration));
            }
        }
        private IEnumerator EndBurn(float duration) {
            yield return new WaitForSeconds(duration);
            Burned = false;
            Movement.SetSpeed(0);
            IsImmune = true;
            yield return new WaitForSeconds(Constants.DEFAULT_IMMUNITY);
            IsImmune = false;
        }

        public void ApplySlow(float amount, float duration) {
            Slow = amount;
            StartCoroutine(EndSlow(duration));
        }
        private IEnumerator EndSlow(float duration) {
            yield return new WaitForSeconds(duration);
            Slow = 0f;
        }
        public void ApplySmash(float amount, float duration) {
            if (!IsSmashed) {
                Smash = amount;
                StartCoroutine(EndSmash(duration));
            }
        }
        private IEnumerator EndSmash(float duration) {
            yield return new WaitForSeconds(duration);
            Smash = 1f;
        }

        /// <summary>
        /// Returns the current scale of the Digimon, based on its stats.
        /// This scale does not account for left-facing transformations and is unrelated
        /// on the scale of the gameObject associated with the Digimon.
        /// </summary>
        public Vector2 Scale {
            get {
                float scaleX, scaleY;
                float baseScale = Enhanced ? 1.5f : 1f;
                scaleX = baseScale;
                scaleY = baseScale * (IsSmashed ? Smash : 1f);

                return new Vector2(scaleX, scaleY);
            }
        }

        private Coroutine endImmunity;
        private Coroutine setController;

        private void Update() {
            if (IsGuarded) {
                CurrentEnergy -= Time.deltaTime;
                if (CurrentEnergy <= 0) {
                    IsGuarded = false;
                }
            }
            else {
                CurrentEnergy += 0.175f * Time.deltaTime;
            }

            if (Enhanced) {
                enhancementDownturn -= Time.deltaTime;
                if(enhancementDownturn < 0) {
                    enhancementDownturn = 0.75f;
                    CurrentPower--;
                }
            }

            if (Burned) {
                float fDir = Random.Range(0, 50) == 0 ? -1.5f : 1.5f;
                Movement.SetSpeed(fDir, true);
            }
        }

        public void EnableImmunity(float immunity) {
            IsImmune = true;
            immunityDuration = immunity;
        }

        public void DisableImmunity() {
            endImmunity = StartCoroutine(DisableImmunityAfterDelay(immunityDuration));
        }

        private IEnumerator DisableImmunityAfterDelay(float delay) {
            yield return new WaitForSeconds(delay);
            IsImmune = false;
        }

        public void ForceDisableImmunity() {
            if (endImmunity != null) StopCoroutine(endImmunity);
            IsImmune = false;
        }

        public void SetControllerEnabled(bool enabled, float delay = 0f) {
            if (setController != null) StopCoroutine(setController);
            if (delay == 0f) {
                Controller.ControlEnabled = enabled;
            }
            else {
                setController = StartCoroutine(SetControllerEnabledAfterDelay(enabled, delay));
            }
        }
        public IEnumerator SetControllerEnabledAfterDelay(bool enabled, float delay) {
            yield return new WaitForSeconds(delay);
            Controller.ControlEnabled = enabled;
        }

        /// <summary>
        /// Outputs whether the player was guarded.
        /// </summary>
        public void StartHit(Move move, Vector3 hitPos, out bool guarded, float pointConversion = 0.5f) {
            guarded = false;
            if (IsImmune) return;

            if (IsGuarded) {
                guarded = true;
                if (move.IgnoreGuard) {
                    if (IsGuarded) IsGuarded = false;
                    OnApplyHit(move, hitPos, pointConversion);
                }
                else {
                    OnDodgeHit(move, hitPos, pointConversion);
                }
            }
            else {
                OnApplyHit(move, hitPos, pointConversion);
            }
        }

        public bool EndHit(Move move) {
            if (!IsBeingHit) {
                return false;
            }
            else {
                IsBeingHit = false;
                SetControllerEnabled(true, move.BufferTime);
                return true;
            }
        }

        public virtual void OnDodgeHit(Move move, Vector3 hitPos, float pointConversion) {
            CurrentEnergy -= move.GetDamage() / 5f;
            if (CurrentEnergy > 0) {
                Audio.PlayDodge();
                Movement.FacingLeft = hitPos.x < transform.position.x;
            }
            else {
                OnApplyHit(move, hitPos, pointConversion);
            }
        }

        public virtual void OnApplyHit(Move move, Vector3 hitPos, float pointConversion) {
            IsBeingHit = true;
            Audio.PlayHit();

            int HPbefore = Mathf.CeilToInt(CurrentHP * pointConversion);
            CurrentHP -= move.GetDamage();
            int HPafter = Mathf.CeilToInt(CurrentHP * pointConversion);

            Movement.FacingLeft = hitPos.x < transform.position.x;
            SetControllerEnabled(false);

            if (move.Interrupt) Movement.InterruptCurrentMove();

            int pointsToLaunch = HPbefore - HPafter;
            while (pointsToLaunch > 0) {
                if (pointsToLaunch >= 3) {
                    SpawnPoint(transform.position, true, true);
                    pointsToLaunch -= 3;
                }
                else {
                    SpawnPoint(transform.position, false, true);
                    pointsToLaunch--;
                }
            }

            if (Random.Range(0, 5) == 0) {
                SpawnDiamond(transform.position);
            }

            if (CurrentHP <= 0) Debug.Log($"Player {Player} has lost!");
        }

        public virtual void SpawnPoint(Vector3 position, bool big, bool forOpponent, bool launch = true, float pickupTime = 0.5f) {
            GameObject goPoint = Instantiate(pPoint, position, Quaternion.Euler(0, 0, 0));
            PointBehavior p = goPoint.GetComponent<PointBehavior>();
            p.SetupPoint(forOpponent ? Player : GetOppositePlayerIndex(), big, launch, pickupTime);
        }
        public virtual void SpawnDiamond(Vector3 position, bool launch = true, float pickupTime = 0.5f) {
            GameObject goPoint = Instantiate(pPoint, position, Quaternion.Euler(0, 0, 0));
            PointBehavior p = goPoint.GetComponent<PointBehavior>();
            p.SetupDiamond(launch, pickupTime);
        }

        public void GatherPoint(int player, bool isBig) {
            Audio.PlayGather();
            if(player != Player) {
                CurrentPower += isBig ? 3 : 1;
                //Debug.Log($"Player {Player}: {CurrentPower}");
            }
        }
        public void GatherDiamond() {
            Audio.PlayGather();
            CurrentHP += .05f * MaxHP;
            CurrentEnergy += .2f * MaxEnergy;
        }

        public GameObject GetOppositePlayer() {
            if (Player == 0) {
                return GameManager.Instance.player1;
            }
            else {
                return GameManager.Instance.player0;
            }
        }

        public int GetOppositePlayerIndex() {
            if (Player == 0) return 1;
            else return 0;
        }

        public float AddEnergy(float amount) {
            CurrentEnergy += amount;
            return CurrentEnergy;
        }

        public float AddHP(float amount) {
            CurrentHP += amount;
            return CurrentHP;
        }
        public float AddPower(int amount) {
            CurrentPower += amount;
            return CurrentPower;
        }
    }
}