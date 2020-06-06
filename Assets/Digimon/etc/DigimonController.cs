using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class DigimonController : MonoBehaviour {
        //When the player inputs a jump, there's a window of time in which the character will jump if able.
        protected const float JUMP_BUFFER = 0.2f;
        //When the player presses the horizontal buttons twice in a row, the character will run.
        //protected const float RUN_BUFFER = 0.5f;

        [SerializeField] protected DigimonFighter fighter;
        protected DigimonMovement Movement => fighter.Movement;

        public bool ControlEnabled { get; set; } = true;

        public bool CanAttack {
            get {
                if (fighter.Paralyzed) {
                    return false;
                }
                else return ControlEnabled;
            }
        }

        protected Dictionary<string, string> keys = new Dictionary<string, string>();
        protected float jumpBufferTime = 0f;
        //protected float runBufferTime = 0f;
        //protected int runButtonCount = 0;

        protected float moveX;
        protected bool isLeft;

        protected virtual void Start() {
            keys["horizontal"] = $"p{fighter.Player}_horizontal";
            keys["vertical"] = $"p{fighter.Player}_vertical";
            keys["jump"] = $"p{fighter.Player}_jump";
            keys["run"] = $"p{fighter.Player}_run";
            keys["attack"] = $"p{fighter.Player}_attack";
            keys["taunt"] = $"p{fighter.Player}_taunt";
        }

        protected virtual void Update() {
            //Update jump buffer.
            if (jumpBufferTime > 0) jumpBufferTime -= Time.deltaTime;
            //Jumps can be 'submitted' even while the control is disabled, but will only work if the control is enabled.
            if (Input.GetButtonDown(keys["jump"])) {
                jumpBufferTime = JUMP_BUFFER;
            }

            //TODO: Player should be able to drop through platforms (currently blocked by SetGuarded() ), and not continue walking when using pepper breath.
            //TODO: Maybe player changes physicsmaterial2d while knocked back so it doesn't stuck with the other player.
            if (CanAttack) {
                if (Input.GetAxisRaw(keys["vertical"]) < 0) {
                    if (fighter.CurrentEnergy > 0.2f) {
                        Movement.SetGuarded(true);
                        Movement.SetSpeed(0);
                    }
                }
                else {
                    Movement.SetGuarded(false);
                }

                if (Input.GetButtonDown(keys["run"])) {
                    Movement.IsRunning = true;
                }
                else if (Input.GetButtonUp(keys["run"])) {
                    Movement.IsRunning = false;
                }

                if (Input.GetButtonDown(keys["attack"])) {
                    Movement.SetGuarded(false);
                    UseAttack();
                }

                if (jumpBufferTime > 0f && Movement.IsGrounded) {
                    jumpBufferTime = 0f;
                    if (Input.GetAxisRaw(keys["vertical"]) < 0) {
                        Movement.DropThroughPlatform();
                    }
                    else {
                        if (!fighter.IsGuarded) {
                            Movement.ResetAirJump();
                            Movement.Jump();
                        }
                    }
                }
                else if (!fighter.IsGuarded) {
                    if (Input.GetButtonUp(keys["jump"]) && !Input.GetButton(keys["vertical"])) {
                        Movement.StopJump();
                    }
                    else if (Input.GetButton(keys["horizontal"])) {
                        isLeft = Input.GetAxisRaw(keys["horizontal"]) < 0;
                        Movement.Walk(isLeft);
                    }
                    else if (Input.GetButtonUp(keys["horizontal"])) {
                        Movement.StopWalk();
                    }
                }
            }
            //DEBUG: REMOVE
            if (Input.GetKeyDown(KeyCode.F)) {
                Movement.ApplyAirborne(new Vector2(5, 5));
            }
            if (Input.GetKeyDown(KeyCode.G)) {
                Movement.ApplyKnockback(4);
            }

        }

        protected virtual void UseAttack() {
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
                    if (Input.GetAxis(keys["vertical"]) > 0 && Movement.AirJumpAllowed) {
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