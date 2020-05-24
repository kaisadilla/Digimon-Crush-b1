using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush {
    public class FighterMovement : MonoBehaviour {
        [Header("Components")]
        public FighterC fighter;
        [SerializeField] private HitboxMelee attackHitbox;
        [SerializeField] private Rigidbody2D body;
        [SerializeField] new private Collider2D collider;

        [Header("Variables")]
        [SerializeField] private float baseSpeed;
        [SerializeField] private float baseJumpSpeed;

        private bool lockSpeed;
        
        public bool FacingLeft { get; set; }
        public float MoveSpeed { get; private set; }
        public bool Taunting { get; set; }
        public bool IsGrounded { get; private set; }
        public bool IsKnockedBack { get; private set; }
        public bool IsAirborne { get; private set; }

        public bool IsFalling {
            get => body.velocity.y < 0;
        }

        public float CurrentSpeed {
            get => baseSpeed;
            private set => baseSpeed = value;
        }
        public float CurrentJumpSpeed {
            get => baseJumpSpeed;
            private set => baseJumpSpeed = value;
        }

        private void Update() {
            if (usingAttack_claws) {
                SetSpeedX(Directional(0.5f));
            }
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.collider.tag == "Ground" || collision.collider.tag == "Platform") {
                if (collision.enabled) {
                    IsGrounded = true;
                }
            }
        }

        private void OnCollisionStay2D(Collision2D collision) {
            if (IsKnockedBack) {
                if (Mathf.Abs(body.velocity.x) < 2 && body.velocity.y == 0) {
                    IsKnockedBack = false;
                    body.mass = 1;
                    if (!IsAirborne) RegainControl();
                }
            }
        }

        public void RegainControl() {
            fighter.DisableImmunity();
            fighter.SetControllerEnabled(true);
            IsAirborne = false;
            /*
            if (!IsKnockedBack) {
                Debug.Log("Control regained!");
                fighter.DisableImmunity();
                fighter.SetControllerEnabled(true);
            }
            else {

                Debug.Log("Control regained behorehand which is bad :(");
            }*/
        }

        private void OnCollisionExit2D(Collision2D collision) {
            if (collision.collider.tag == "Ground" || collision.collider.tag == "Platform") {
                if (collision.enabled) {
                    IsGrounded = false;
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision) {
            if (collision.gameObject.tag == "Ground") {
                //If the 'platform' you are in is the ground, you instantly lose the ability to go through platforms.
                collider.isTrigger = false;
            }
        }
        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform") {
                //When you exit the platform you are in, you stop being able to go through platforms.
                IsGrounded = false;
                collider.isTrigger = false;
            }
        }

        /// <summary>
        /// Returns the same float, changed to negative if the fighter is facing left.
        /// </summary>
        private float Directional(float speed) {
            return FacingLeft ? -speed : speed;
        }

        public void SetLockSpeed(bool lockSpeed) {
            this.lockSpeed = lockSpeed;
        }

        public void SetSpeedX(float amount) {
            if (lockSpeed) return;

            MoveSpeed = amount * CurrentSpeed;
            if (!IsGrounded) MoveSpeed *= 0.75f;

            //transform.position += new Vector3(MoveSpeed, 0, 0);
            body.velocity = new Vector2(MoveSpeed, body.velocity.y);

            if (MoveSpeed > 0) {
                FacingLeft = false;
            }
            else if (MoveSpeed < 0) {
                FacingLeft = true;
            }
        }

        public void Jump() {
            body.velocity = new Vector2(body.velocity.x, CurrentJumpSpeed);
            //body.AddForce(new Vector2(0f, CurrentJumpFloat), ForceMode2D.Impulse);
        }

        public void StopJump() {
            if(body.velocity.y > 0) {
                body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.5f);
            }
        }

        public void DropThroughPlatform() {
            if (IsGrounded) {
                body.velocity += new Vector2(0, -2f);
                collider.isTrigger = true;
            }
        }

        /*
         * grounded + x : attack0
         * grounded + x + x-arrow : attack1
         * grounded + x + up-arrow : attack2
         * grounded + x + down-arrow : attack3
         * flying + x : attack4
         * flying + x + x-arrow : attack5
         * flying + x + up-arrow : attack6
         * flying + x + down-arrow : attack7
         */
        public void UseAttack0() {
            Attack_claws();
        }
        public void UseAttack1() {
            Attack_claws();
        }
        public void UseAttack2() {

        }
        public void UseAttack3() {
            Attack_pepperBreath();
        }
        public void UseAttack4() {
            Attack_flyingKick();
        }
        public void UseAttack5() {
            Attack_flyingKick();
        }
        public void UseAttack6() {

        }
        public void UseAttack7() {

        }
        public void UseAttack8() {
            Attack_claws();
        }

        private bool usingAttack_claws;
        private void Attack_claws() {
            usingAttack_claws = true;
            fighter.SetControllerEnabled(false);
            attackHitbox.Setup(3f, 2, 4, 0);
            transform.position += new Vector3(Directional(1), 0, 0);
            body.velocity = new Vector2(0, 0);
            fighter.anim.TriggerAnim("attack_claws");
        }
        public void EndAttack_claws() {
            usingAttack_claws = false;
            fighter.SetControllerEnabled(true);
        }

        private void Attack_flyingKick() {
            attackHitbox.Setup(5f, 2, 4, 4);
            fighter.anim.TriggerAnim("attack_flyingKick");
            lockSpeed = true;
            body.velocity += new Vector2(0, -3f);
        }

        private void Attack_pepperBreath() {
            fighter.SetControllerEnabled(false);
            fighter.anim.TriggerAnim("attack_pepperBreath");
        }

        public void Launch_pepperBreath() {
            GameObject p = Instantiate(fighter.pepperBreath, transform.position - new Vector3(0, 0.8f, 0), Quaternion.Euler(0, 0, 0), null);
            //p.GetComponent<Projectile_PepperBreath>().Setup(gameObject, FacingLeft, 10f, 0, 3, 2);
        }

        public void ApplyKnockback(float forceX, float forceY, float immunity) {
            IsKnockedBack = true;
            IsAirborne = (forceY != 0) ? true : false;
            fighter.SetControllerEnabled(false);
            fighter.EnableImmunity(immunity);
            body.velocity = new Vector2(-Directional(3 * forceX), 3 * forceY);
            body.mass = 1000000;

            //body.velocity = new Vector2(0, 0);
            //body.AddForce(new Vector2(-Directional(3 * forceX), 3 * forceY), ForceMode2D.Impulse);
        }
    }
}