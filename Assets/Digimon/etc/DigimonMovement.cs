using System.Collections;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Kaisa.DigimonCrush.Fighter {
    public abstract class DigimonMovement : MonoBehaviour {
        [Header("Components")]
        [SerializeField] protected DigimonFighter fighter;
        [SerializeField] public Rigidbody2D body;
        [SerializeField] protected MeleeHitbox meleeHitbox;

        [Header("Variables")]
        [SerializeField] protected float baseSpeed;
        [SerializeField] protected float baseJumpSpeed;
        [Tooltip("The layers that are considered ground.")]
        [SerializeField] protected LayerMask ground;

        public float CurrentSpeed { get; protected set; }
        public bool FacingLeft { get; set; }
        public bool IsKnockedBack { get; protected set; }
        public bool IsAirborne { get; protected set; }
        public bool AirJumpAllowed { get; protected set; } = true;
        public bool AirDashAllowed { get; protected set; } = true;

        public float[] Cooldowns { get; protected set; } = new float[10];

        private Move _currentMove = null;
        public Move CurrentMove {
            get {
                return _currentMove;
            }
            protected set {
                _currentMove = value;
                meleeHitbox.Setup(_currentMove);
                if(_currentMove != null) fighter.Anim.TriggerAnim(_currentMove.AnimName);
            }
        }

        public bool IsRunning { get; set; }

        public float MoveSpeed => baseSpeed * ExtraSpeed * (1f - fighter.Slow);

        public float ExtraSpeed {
            get {
                if (IsGrounded) {
                    if (!fighter.Enhanced) {
                        return IsRunning ? 1.5f : 1f;
                    }
                    else {
                        return IsRunning ? 2f : 1.5f;
                    }
                }
                else {
                    return 0.75f;
                }
            }
        }
        public float JumpSpeed {
            get => baseJumpSpeed;
            protected set => baseJumpSpeed = value;
        }

        public bool IsGrounded {
            get {
                if (body.velocity.y > 0.1f || body.velocity.y < -0.1f) return false;
                RaycastHit2D hit = Physics2D.Raycast(fighter.Hitbox.bounds.center, Vector2.down, fighter.Hitbox.bounds.extents.y + .01f, ground);

#if UNITY_EDITOR
                Color rayColor = hit.collider == null ? Color.red : Color.green;
                Debug.DrawRay(fighter.Hitbox.bounds.center, Vector2.down * (fighter.Hitbox.bounds.extents.y + 0.1f), rayColor);
#endif

                return hit.collider != null;
            }
        }

        public bool IsFalling {
            get => body.velocity.y < 0;
        }

        public string GroundTag {
            get {
                RaycastHit2D hit = Physics2D.Raycast(fighter.Hitbox.bounds.center, Vector2.down, fighter.Hitbox.bounds.extents.y + .01f, ground);
                return hit.collider == null ? null : hit.collider.tag;
            }
        }

        private void Update() {
            CurrentMove?.OnUpdate();
            for (int i = 0; i < Cooldowns.Length; i++) {
                if (Cooldowns[i] > 0f) {
                    Cooldowns[i] -= Time.deltaTime;
                    if (Cooldowns[i] < 0f) Cooldowns[i] = 0f;
                }
            }
        }

        private void OnCollisionStay2D(Collision2D collision) {
            if (IsKnockedBack) {
                if(Mathf.Abs(body.velocity.x) < 2) {
                    StopKnockback();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {
            fighter.Hitbox.isTrigger = false;
        }

        public void SetSpeed(float amount, bool directional = false) {
            if (directional) amount = Directional(amount);

            CurrentSpeed = amount * MoveSpeed;

            body.velocity = new Vector2(CurrentSpeed, body.velocity.y);

            if (CurrentSpeed > 0) FacingLeft = false;
            else if (CurrentSpeed < 0) FacingLeft = true;
        }

        public void SetSpeed(Vector2 amount, bool directional = false) {
            if (directional) amount = new Vector2(Directional(amount.x) * MoveSpeed, amount.y);

            CurrentSpeed = amount.x;

            body.velocity = amount;

            if (CurrentSpeed > 0) FacingLeft = false;
            else if (CurrentSpeed < 0) FacingLeft = true;
        }

        public void Walk(bool left) {
            float amount = left ? -1 : 1;
            CurrentSpeed = amount * MoveSpeed;

            body.velocity = new Vector2(CurrentSpeed, body.velocity.y);

            if (CurrentSpeed > 0) FacingLeft = false;
            else if (CurrentSpeed < 0) FacingLeft = true;
        }
        
        public void StopWalk() {
            CurrentSpeed = 0;
            body.velocity = new Vector2(CurrentSpeed, body.velocity.y);
        }

        public void PushBack(float amount) {
            amount = Directional(-amount) * 6f;
            body.velocity = new Vector2(amount, body.velocity.y);
        }

        public void PushBack(Vector2 amount) {
            amount = new Vector2(Directional(-amount.x) * 6f, amount.y * 12f);
            body.velocity = amount;
        }

        public void MoveX(float amount, bool directional = false) {
            if (directional) amount = Directional(amount);
            body.transform.position += new Vector3(amount, 0, 0);
        }

        public void Jump(float amount = 1f) {
            body.velocity = new Vector2(body.velocity.x, JumpSpeed * amount);
        }

        public virtual void ResetAirJump() {
            AirJumpAllowed = true;
            AirDashAllowed = true;
        }

        public void StopJump(float amount = 0.5f) {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * amount);
        }

        public void DropThroughPlatform(float amount = 2f) {
            if (IsGrounded && GroundTag == "Platform") {
                body.velocity += new Vector2(0, -amount);
                fighter.Hitbox.isTrigger = true;
            }
        }

        public void ApplyKnockback(float force, float immunity = Constants.DEFAULT_IMMUNITY) {
            if (fighter.Enhanced) force /= 2f;

            StopAirborne();
            IsKnockedBack = true;
            fighter.SetControllerEnabled(false);
            fighter.EnableImmunity(immunity);
            body.velocity = new Vector2(-Directional(3 * force), body.velocity.y);
            body.mass = 1000000;
            CurrentSpeed = 0f; //The player could be moving when the knockback ocurred but not move again after the knockback ends.
        }

        public void StopKnockback() {
            if(IsKnockedBack) {
                IsKnockedBack = false;
                fighter.SetControllerEnabled(true);
                fighter.DisableImmunity();
                body.mass = 1;
            }
        }

        public void ApplyAirborne(Vector2 force, float immunity = Constants.DEFAULT_IMMUNITY) {
            if (!fighter.Enhanced) {
                StopKnockback();
                IsAirborne = true;
                fighter.SetControllerEnabled(false);
                fighter.EnableImmunity(immunity);
                body.velocity = new Vector2(-Directional(force.x) * 3, force.y * 3);
                body.mass = 100;
                CurrentSpeed = 0f;
            }
            else {
                ApplyKnockback(force.x / 2f);
            }
        }

        public void StopAirborne() {
            if(IsAirborne) {
                IsAirborne = false;
                fighter.SetControllerEnabled(true);
                fighter.DisableImmunity();
                body.mass = 1;
            }
        }

        protected float Directional(float n) {
            return FacingLeft ? -n : n;
        }
        protected Vector2 Directional(Vector2 v) {
            return FacingLeft ? new Vector2(-v.x, v.y) : v;
        }
        protected Vector2 Directional(Vector3 v) {
            return FacingLeft ? new Vector3(-v.x, v.y, v.z) : v;
        }

        public void StartCurrentMove() {
            if (CurrentMove != null) {
                fighter.ForceDisableImmunity();
                fighter.SetControllerEnabled(CurrentMove.AllowInput);
                CurrentMove.OnStart();
            }
        }

        public void CurrentMove_Fire() {
            CurrentMove?.OnFire();
        }

        public void CurrentMove_IncreaseCount() {
            CurrentMove?.IncreaseHitCount();
        }

        public void CurrentMove_CallEffect(string effect) {
            CurrentMove?.CallEffect(effect);
        }

        public void EndCurrentMove() {
            if(CurrentMove != null) {
                CurrentMove.OnEnd();
                fighter.SetControllerEnabled(true);
                CurrentMove = null;
            }
        }

        public void InterruptCurrentMove(bool enableControl = false) {
            if(CurrentMove != null) {
                CurrentMove.OnInterrupt();
                fighter.SetControllerEnabled(enableControl);
                CurrentMove = null;
            }
        }

        public void EndCurrentMove(float delay) {
            StartCoroutine(EndCurrentMoveAfterDelay(delay));
        }

        private IEnumerator EndCurrentMoveAfterDelay(float delay) {
            yield return new WaitForSeconds(delay);
            EndCurrentMove();
        }

        public GameObject LaunchProjectile(string name, Move move, float offsetX, float offsetY, bool oppositeSide = false) {
            GameObject prefab = Resources.Load<GameObject>($"moves/projectiles/{name}");
            Vector3 offset = new Vector3(Directional(-offsetX), -offsetY, 0);
            GameObject p = Instantiate(prefab, transform.position - offset, Quaternion.Euler(0, 0, 0), null);
            p.GetComponent<Projectile>().Setup(gameObject, fighter.Hitbox, move, FacingLeft ^ oppositeSide, fighter.Scale); //xor: ^
            return p;
        }

        public GameObject AttachProjectile(string name, Move move, float offsetX, float offsetY) {
            GameObject prefab = Resources.Load<GameObject>($"moves/projectiles/{name}");
            Vector3 offset = new Vector3(Directional(-offsetX), -offsetY, 0);
            GameObject p = Instantiate(prefab, transform.position - offset, Quaternion.Euler(0, 0, 0), fighter.transform);
            p.GetComponent<Projectile>().Setup(gameObject, fighter.Hitbox, move, false, fighter.Scale);
            return p;
        }

        public GameObject LaunchProjectile(string name, Move move) {
            return LaunchProjectile(name, move, 0, 0);
        }

        public void SetGuarded(bool guarded) {
            fighter.IsGuarded = guarded;
        }

        public void SetGhosted(bool ghosted) {
            if (ghosted) {
                fighter.gameObject.layer = Constants.layerGhostedPlayer;
                foreach(Transform child in fighter.transform) {
                    child.gameObject.layer = Constants.layerGhostedPlayer;
                }
            }
            else {
                fighter.gameObject.layer = Constants.layerPlayer;
                foreach (Transform child in fighter.transform) {
                    child.gameObject.layer = Constants.layerPlayer;
                }
            }
        }

        public virtual void UseAttack0() {
            Move m = OnAttack0();
            AssignMove(m);
        }
        public virtual void UseAttack1() {
            Move m = OnAttack1();
            AssignMove(m);
        }
        public virtual void UseAttack2() {
            Move m = OnAttack2();
            AssignMove(m);
        }
        public virtual void UseAttack3() {
            Move m = OnAttack3();
            AssignMove(m);
        }
        public virtual void UseAttack4() {
            Move m = OnAttack4();
            AssignMove(m);
        }
        public virtual void UseAttack5() {
            Move m = OnAttack5();
            AssignMove(m);
        }
        public virtual void UseAttack6() {
            if (AirJumpAllowed) {
                AirJumpAllowed = false;
                Move m = OnAttack6();
                AssignMove(m);
            }
        }
        public virtual void UseAttack7() {
            Move m = OnAttack7();
            AssignMove(m);
        }
        public virtual void UseAttack8() {
            Move m = OnAttack8();
            AssignMove(m);
        }

        public virtual void AssignMove(Move m) {
            if (m == null) return;

            if (m.InternalId >= 0) {
                if (Cooldowns[m.InternalId] <= 0f) {
                    Cooldowns[m.InternalId] = m.Cooldown;
                    CurrentMove = m;
                    StartCurrentMove();
                }
            }
            else {
                CurrentMove = m;
                StartCurrentMove();
            }
        }

        public abstract Move OnAttack0(); // regular
        public abstract Move OnAttack1(); // arrows
        public abstract Move OnAttack2(); // up
        public abstract Move OnAttack3(); // down
        public abstract Move OnAttack4(); // jump + regular
        public abstract Move OnAttack5(); // jump + arrows
        public abstract Move OnAttack6(); // jump + up
        public abstract Move OnAttack7(); // jump + down
        public abstract Move OnAttack8(); // sprint

    }
}