using System.Collections;
using System.Collections.Generic;
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

        public int Player => _player;

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

        public bool IsBeingHit { get; private set; }

        private void Awake() {
            CurrentHP = MaxHP;
            CurrentEnergy = MaxEnergy;
        }

        private void Update() {
            if(IsGuarded) {
                CurrentEnergy -= Time.deltaTime;
                if (CurrentEnergy <= 0) {
                    IsGuarded = false;
                }
            }
            else {
                CurrentEnergy += 0.25f * Time.deltaTime;
            }
        }

        public void EnableImmunity(float immunity) {
            IsImmune = true;
            immunityDuration = immunity;
        }

        public void DisableImmunity() {
            StartCoroutine(DisableImmunityAfterDelay(immunityDuration));
        }
        
        public void SetControllerEnabled(bool enabled) {
            Controller.ControlEnabled = enabled;
        }

        public void StartHit(float damage, Vector3 hitPos) {
            if (IsImmune) return;

            if (IsGuarded) {
                Audio.PlayDodge();
                Movement.FacingLeft = hitPos.x < transform.position.x;
                CurrentEnergy -= damage / 5f;
                if (CurrentEnergy <= 0) _ApplyHit();
            }
            else {
                _ApplyHit();
            }

            void _ApplyHit() {
                IsBeingHit = true;
                Audio.PlayHit();

                int HPbefore = Mathf.CeilToInt(CurrentHP / 2f);
                CurrentHP -= damage;
                int HPafter = Mathf.CeilToInt(CurrentHP / 2f);

                Movement.FacingLeft = hitPos.x < transform.position.x;
                SetControllerEnabled(false);
                Movement.InterruptCurrentMove();

                int pointsToLaunch = HPbefore - HPafter;
                while(pointsToLaunch > 0) {
                    GameObject goPoint = Instantiate(pPoint, transform.position, Quaternion.Euler(0, 0, 0));
                    PointBehavior p = goPoint.GetComponent<PointBehavior>();
                    if(pointsToLaunch >= 3) {
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

        private IEnumerator DisableImmunityAfterDelay(float delay) {
            yield return new WaitForSeconds(delay);
            IsImmune = false;
        }

        int points = 0;

        public void GatherPoint(int player, bool isBig) {
            Audio.PlayGather();
            if(player != Player) {
                points += isBig ? 3 : 1;
                Debug.Log($"Player {Player}: {points}");
            }
        }
        public void GatherDiamond() {
            Audio.PlayGather();
            CurrentHP += .05f * MaxHP;
            CurrentEnergy += .2f * MaxEnergy;
        }
    }
}