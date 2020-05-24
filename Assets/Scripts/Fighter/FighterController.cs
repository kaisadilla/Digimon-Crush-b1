using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kaisa.DigimonCrush {
    public class FighterController : MonoBehaviour {
        [Header("Components")]
        [SerializeField] private FighterC fighter;

        [Header("Variables")]

        private Dictionary<string, string> keys = new Dictionary<string, string>();
        private bool controlAllowed = true;

        private float moveX = 0f;

        private FighterMovement Movement => fighter.movement;

        private void Awake() {
            keys["horizontal"] = $"p{fighter.player}_horizontal";
            keys["vertical"] = $"p{fighter.player}_vertical";
            keys["jump"] = $"p{fighter.player}_jump";
            keys["run"] = $"p{fighter.player}_run";
            keys["attack"] = $"p{fighter.player}_attack";
            keys["taunt"] = $"p{fighter.player}_taunt";
        }

        private float jumpRequestTime = 0f;

        private void Update() {
            if (fighter.player > 1) return;
            jumpRequestTime -= Time.deltaTime;

            if (Input.GetButtonDown(keys["horizontal"])
                || Input.GetButtonDown(keys["vertical"])
                || Input.GetButtonDown(keys["jump"])
                || Input.GetButtonDown(keys["run"])
                || Input.GetButtonDown(keys["attack"])
                )
            {
                Movement.Taunting = false;
            }

            if (controlAllowed) {
                if (Input.GetButtonDown(keys["attack"])) {
                    UseAttack();
                    return;
                }
                /* When the player jumps, there is a 0.2s window in which they will jump automatically as soon
                 * as they become grounded, even if they weren't grounded when they pressed it */
                if (Input.GetButtonDown(keys["jump"])) {
                    jumpRequestTime = 0.2f;
                }
                if (jumpRequestTime > 0f && Movement.IsGrounded) {
                    jumpRequestTime = 0f;
                    if (Input.GetButton(keys["vertical"])) {
                        Movement.DropThroughPlatform();
                    }
                    else {
                        Movement.Jump();
                    }
                }
                else if (Input.GetButtonUp(keys["jump"])) {
                    Movement.StopJump();
                }
                else if (Input.GetButton(keys["horizontal"])) {
                    moveX = Input.GetAxisRaw(keys["horizontal"]);
                    if (Input.GetButton(keys["run"]) && Movement.IsGrounded) {
                        Movement.SetSpeedX(moveX * 1.5f);
                    }
                    else {
                        Movement.SetSpeedX(moveX);
                    }
                }
                else if (Input.GetButtonUp(keys["horizontal"])) {
                    Movement.SetSpeedX(0);
                }
                else if (Input.GetButtonDown(keys["taunt"])) {
                    Movement.Taunting = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.F)) {
                Movement.ApplyKnockback(4, 4, Constants.DEFAULT_IMMUNITY);
            }
        }

        /* 
         * KEY BINDINGS:
         * grounded + x        : attack0
         * grounded + x + hor  : attack1
         * grounded + x + up   : attack2
         * grounded + x + down : attack3
         * airborne + x        : attack4
         * airborne + x + hor  : attack5
         * airborne + x + up   : attack6
         * airborne + x + down : attack7
         * running + x + hor : attack8
         * (hor = left/right)
         */
        private void UseAttack() {
            if (Movement.IsGrounded) {
                if (Input.GetButton(keys["horizontal"])) {
                    if (Input.GetButton(keys["run"])) {
                        Movement.UseAttack8();
                    }
                    else {
                        Movement.UseAttack1();
                    }
                }
                else if (Input.GetButton(keys["vertical"])) {
                    if (Input.GetAxis(keys["vertical"]) > 0) {
                        Movement.UseAttack2();
                    }
                    else if (Input.GetAxis(keys["vertical"]) < 0) {
                        Movement.UseAttack3();
                    }
                }
                else {
                    Movement.UseAttack0();
                }
            }
            else {
                if (Input.GetButton(keys["horizontal"])) {
                    Movement.UseAttack5();
                }
                else if (Input.GetButton(keys["vertical"])) {
                    if (Input.GetAxis(keys["vertical"]) > 0) {
                        Movement.UseAttack6();
                    }
                    else if (Input.GetAxis(keys["vertical"]) < 0) {
                        Movement.UseAttack7();
                    }
                }
                else {
                    Movement.UseAttack4();
                }
            }
        }
    }
}