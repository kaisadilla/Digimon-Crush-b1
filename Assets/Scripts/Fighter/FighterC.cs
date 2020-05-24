using System.Collections;
using UnityEngine;

namespace Kaisa.DigimonCrush {
    public class FighterC : MonoBehaviour {
        public int player;

        [Header("Components")]
        [SerializeField] new private FighterAudio audio;
        [SerializeField] public FighterController controller;
        [SerializeField] public FighterMovement movement;
        [SerializeField] public FighterAnim anim;
        public GameObject pepperBreath;

        [Header("Variables")]
        public float maxHP = 100f;

        private float currentHP;
        public bool IsBeingHit { get; private set; }
        public bool IsImmune { get; private set; }
        private float immunityDuration;

        private void Awake() {
            currentHP = maxHP;
        }

        public void SetControllerEnabled(bool enabled) {
            controller.enabled = enabled;
        }

        /// <summary>
        /// Enables immunity. Whenu disabled with DisableImmunity(), this immunity will last for an additional duration.
        /// </summary>
        /// <param name="duration">The additional duration of the immunity once it's toggled off.</param>
        public void EnableImmunity(float duration) {
            //gameObject.layer = Constants.layerImmunePlayer;
            IsImmune = true;
            immunityDuration = duration;
        }
        public void DisableImmunity() {
            StartCoroutine(DisableImmunityAfterDelay(immunityDuration));
        }
        private IEnumerator DisableImmunityAfterDelay(float delay) {
            yield return new WaitForSeconds(delay);
            IsImmune = false;
            //gameObject.layer = Constants.layerPlayer;
        }

        public void GetHit(float damage, Vector3 hitboxPos) {
            if (IsImmune) return;

            audio.PlayHit();
            currentHP -= damage;
            IsBeingHit = true;
            movement.FacingLeft = hitboxPos.x < transform.position.x;
            controller.enabled = false;

            if (currentHP <= 0) Debug.Log($"Player {player} has lost!");
        }

        /// <summary>
        /// Returns true if the fighter was being hit (and thus, a hit has been ended), false otherwise.
        /// </summary>
        public bool EndHit() {
            if(IsBeingHit) {
                IsBeingHit = false;
                controller.enabled = true;
                return true;
            }
            return false;
        }

        public float GetMaxHP() => maxHP;
        public float GetCurrentHP() => currentHP;
    }
}