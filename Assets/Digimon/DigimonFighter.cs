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

        public bool IsImmune { get; private set; }
        private float immunityDuration;

        public int Player => _player;

        private float maxHP = 100f;
        private float currentHP;

        public float maxEnergy = 5f;
        public float currentEnergy;

        public bool IsBeingHit { get; private set; }

        private void Awake() {
            currentHP = maxHP;
            currentEnergy = maxEnergy;
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

            IsBeingHit = true;
            Audio.PlayHit();
            currentHP -= damage;
            Movement.FacingLeft = hitPos.x < transform.position.x;
            SetControllerEnabled(false);
            Movement.InterruptCurrentMove();

            if (currentHP <= 0) Debug.Log($"Player {Player} has lost!");
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

        public float GetMaxHP() => maxHP;
        public float GetCurrentHP() => currentHP;
        public float GetMaxEnergy() => maxEnergy;
        public float GetCurrentEnergy() => currentEnergy;
    }
}