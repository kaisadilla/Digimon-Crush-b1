using System.Collections;
using UnityEngine;

namespace Kaisa.DigimonCrush {
    public class FighterAnim : MonoBehaviour {
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Animator anim;
        [SerializeField] private FighterC fighter;

        private void Start() {
            StartCoroutine(FlashImmunity());
        }

        private void Update() {
            //Debug.Log($"MoveSpeed: {movement.MoveSpeed}");
            //Debug.Log($"IsGrounded: {movement.IsGrounded}");
            //Debug.Log($"IsFalling: {movement.IsFalling}");

            if (fighter.movement.FacingLeft) {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else {
                transform.localScale = new Vector3(1, 1, 1);
            }

            //sprite.flipX = movement.facingLeft;

            float velX = fighter.movement.MoveSpeed;
            //If the player is not falling
            if (fighter.movement.IsGrounded) {
                if (velX == 0) {
                    if (fighter.movement.Taunting) {
                        SetAnimState(PlayerState.Taunting);
                    }
                    else {
                        SetAnimState(PlayerState.Idle);
                    }
                }
                else {
                    SetAnimState(PlayerState.Walking);
                }
            }
            else {
                if (fighter.movement.IsKnockedBack && fighter.movement.IsAirborne) {
                    SetAnimState(PlayerState.KnockedBack);
                }
                else if (fighter.movement.IsFalling) {
                    SetAnimState(PlayerState.Falling);
                }
                else {
                    SetAnimState(PlayerState.Jumping);
                }
            }

            if(fighter.IsBeingHit) {
                SetBeingHurt(true);
            }
            else {
                SetBeingHurt(false);
            }
        }

        private IEnumerator FlashImmunity() {
            bool lastFrame = true;
            while (true) {
                if(!fighter.movement.IsKnockedBack && fighter.IsImmune) {
                    lastFrame = !lastFrame;
                }
                else {
                    lastFrame = true;
                }
                sprite.enabled = lastFrame;
                yield return new WaitForSeconds(0.1f);
            }
        }

        public void SetAnimState(PlayerState mode) {
            anim.SetInteger("playerState", (int)mode);
        }

        public void TriggerAnim(string animName) {
            anim.SetTrigger(animName);
        }

        public void SetBeingHurt(bool hurt) {
            anim.SetBool("beingHurt", hurt);
        }

        /*public string GetCurrentAnimation() {
            return anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        }*/

        public bool IsAnimationPlaying(string animation) {
            return anim.GetCurrentAnimatorStateInfo(0).IsName(animation);
        }
    }
}