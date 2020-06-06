using System.Collections;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class DigimonAnim : MonoBehaviour {
        [Header("Components")]
        [SerializeField] private DigimonFighter fighter;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Animator animator;
        [Header("Particles")]
        [SerializeField] public GameObject burn;

        private void Start() {
            StartCoroutine(FlashImmunity());
        }

        private void Update() {
            if (fighter.Paralyzed) {
                animator.speed = 0f;
                return;
            }
            else {
                animator.speed = 1f;
            }

            if (fighter.Burned) {
                burn.SetActive(true);
            }
            else {
                burn.SetActive(false);
            }


            //animator.speed = !fighter.Paralyzed ? 1f : 0f;

            float scale = fighter.Scale;

            float walkSpeed = fighter.Movement.ExtraSpeed;//fighter.Movement.IsRunning ? 1.5f : 1f;
            animator.SetFloat("walk_speed", walkSpeed);

            if (fighter.Movement.FacingLeft) {
                fighter.transform.localScale = new Vector3(-scale, scale, scale);
            }
            else {
                fighter.transform.localScale = new Vector3(scale, scale, scale);
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
                if (fighter.IsImmune && !(fighter.Movement.IsKnockedBack || fighter.Movement.IsAirborne || fighter.Burned || fighter.Movement.CurrentMove != null)) {
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