﻿using System.Collections;
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

        protected Dictionary<string, string> keys = new Dictionary<string, string>();
        protected float jumpBufferTime = 0f;
        //protected float runBufferTime = 0f;
        //protected int runButtonCount = 0;

        protected float moveX;

        protected virtual void Awake() {
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

            if (ControlEnabled) {
                if (Input.GetButtonDown(keys["attack"])) {
                    UseAttack();
                }
                else if (jumpBufferTime > 0f && Movement.IsGrounded) {
                    jumpBufferTime = 0f;
                    if (Input.GetAxisRaw(keys["vertical"]) < 0) {
                        Movement.DropThroughPlatform();
                    }
                    else {
                        Movement.Jump();
                    }
                }
                else if (Input.GetButtonUp(keys["jump"]) && !Input.GetButton(keys["vertical"])) {
                    Movement.StopJump();
                }
                else if (Input.GetButton(keys["horizontal"])) {
                    moveX = Input.GetAxisRaw(keys["horizontal"]);
                    if(Movement.IsGrounded) {
                        if (Input.GetButton(keys["run"])) moveX *= 1.5f;
                    }
                    else {
                        moveX *= 0.75f;
                    }
                    Movement.SetSpeed(moveX);
                }
                else if (Input.GetButtonUp(keys["horizontal"])) {
                    Movement.SetSpeed(0);
                }
                else if (Movement.IsGrounded && Input.GetAxisRaw(keys["vertical"]) < 0) {
                    if(fighter.currentEnergy > 0) {
                        fighter.currentEnergy -= Time.deltaTime;
                    }
                }
                else {
                    if(fighter.currentEnergy < fighter.maxEnergy) {
                        fighter.currentEnergy += 0.25f * Time.deltaTime;
                    }
                }

                if (fighter.Player == 1 && Input.GetKeyDown(KeyCode.F)) {
                    Movement.ApplyAirborne(new Vector2(5, 5));
                }
                if (fighter.Player == 1 && Input.GetKeyDown(KeyCode.G)) {
                    Movement.ApplyKnockback(4);
                }
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