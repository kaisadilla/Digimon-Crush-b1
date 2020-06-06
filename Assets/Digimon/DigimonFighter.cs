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
                if (Enhanced) return 1.5f;
                else return 1f;
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

        public void ApplyParalyze(float duration) {
            if (!IsImmune && !Paralyzed) {
                Paralyzed = true;
                Movement.body.gravityScale = 0f;
                Movement.SetSpeed(new Vector2(0f, 0f));
                StartCoroutine(EndParalyze(duration));
            }
        }
        private IEnumerator EndParalyze(float duration) {
            yield return new WaitForSeconds(duration);
            Movement.body.gravityScale = 3f;
            Paralyzed = false;
        }

        public void ApplyBurn(float duration) {
            if (!IsImmune && !Burned) {
                Audio.Stop();
                Burned = true;
                IsImmune = true;
                Controller.ControlEnabled = false;
                Movement.SetSpeed(-2f, true);
                StartCoroutine(EndBurn(duration));
            }
        }
        private IEnumerator EndBurn(float duration) {
            yield return new WaitForSeconds(duration);
            Controller.ControlEnabled = true;
            Burned = false;
            Movement.SetSpeed(0);
            yield return new WaitForSeconds(Constants.DEFAULT_IMMUNITY / 2f);
            IsImmune = false;
        }

        public float Scale {
            get => Enhanced ? 1.5f : 1f;
        }

        private Coroutine endImmunity;

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
        
        public void SetControllerEnabled(bool enabled) {
            Controller.ControlEnabled = enabled;
        }

        public void StartHit(Move move, Vector3 hitPos, float pointConversion = 0.5f) {
            if (IsImmune) return;

            if (IsGuarded && !move.IgnoreGuard) {
                OnDodgeHit(move, hitPos, pointConversion);
            }
            else {
                OnApplyHit(move, hitPos, pointConversion);
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
            Movement.InterruptCurrentMove();

            int pointsToLaunch = HPbefore - HPafter;
            while (pointsToLaunch > 0) {
                GameObject goPoint = Instantiate(pPoint, transform.position, Quaternion.Euler(0, 0, 0));
                PointBehavior p = goPoint.GetComponent<PointBehavior>();

                //p.SetupPoint(Player, false);
                //pointsToLaunch--;

                if (pointsToLaunch >= 3) {
                    p.SetupPoint(Player, true);
                    pointsToLaunch -= 3;
                }
                else {
                    p.SetupPoint(Player, false);
                    pointsToLaunch--;
                }
            }

            if (Random.Range(0, 5) == 0) {
                GameObject goPoint = Instantiate(pPoint, transform.position, Quaternion.Euler(0, 0, 0));
                PointBehavior p = goPoint.GetComponent<PointBehavior>();
                p.SetupDiamond();
            }

            if (CurrentHP <= 0) Debug.Log($"Player {Player} has lost!");
        }

        public bool EndHit() {
            if (!IsBeingHit) {
                return false;
            }
            else {
                IsBeingHit = false;
                SetControllerEnabled(true);
                return true;
            }
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
            else if (Player == 1) {
                return GameManager.Instance.player0;
            }
            
            return null;
        }
    }
}