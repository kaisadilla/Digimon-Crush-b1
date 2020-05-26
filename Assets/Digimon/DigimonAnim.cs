using System.Collections;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class DigimonAnim : MonoBehaviour {
        [Header("Components")]
        [SerializeField] private DigimonFighter fighter;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Animator animator;

        private void Start() {
            StartCoroutine(FlashImmunity());
        }

        private void Update() {
            if (fighter.Movement.FacingLeft) {
                fighter.transform.localScale = new Vector3(-1, 1, 1);
            }
            else {
                fighter.transform.localScale = new Vector3(1, 1, 1);
            }

            float speed = fighter.Movement.CurrentSpeed;
            SetGuarded(fighter.IsGuarded);

            if (fighter.Movement.IsGrounded) {
                if (speed == 0) {
                    SetAnimState(AnimState.Idle);
                }
                else {
                    SetAnimState(AnimState.Walking);
                }
            }
            else {
                if (fighter.Movement.IsAirborne) {
                    SetAnimState(AnimState.Airborne);
                }
                else if (fighter.Movement.IsFalling) {
                    SetAnimState(AnimState.Falling);
                }
                else {
                    SetAnimState(AnimState.Jumping);
                }
            }

            if(fighter.IsBeingHit) {
                SetBeingHurt(true);
            }
            else {
                SetBeingHurt(false);
            }
            /*if(fighter.Movement.IsAirborne) {
                SetBeingAirborne(true);
            }
            else {
                SetBeingAirborne(false);
            }*/
        }

        public void SetAnimState(AnimState state) {
            animator.SetInteger("state", (int)state);
        }

        public void TriggerAnim(string animName) {
            animator.SetTrigger(animName);
        }

        public void SetBeingHurt(bool hurt) {
            animator.SetBool("hurt", hurt);
        }
        public void SetBeingAirborne(bool airborne) {
            animator.SetBool("airborne", airborne);
        }
        public void SetGuarded(bool guarded) {
            animator.SetBool("guarded", guarded);
        }

        private IEnumerator FlashImmunity() {
            bool lastFrame = true;
            while (true) {
                if (fighter.IsImmune && !(fighter.Movement.IsKnockedBack || fighter.Movement.IsAirborne)) {
                    lastFrame = !lastFrame;
                }
                else {
                    lastFrame = true;
                }
                sprite.enabled = lastFrame;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}