using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public abstract class Move {
        public string AnimName { get; protected set; } = "null";
        public float Damage { get; set; } = 1f;

        public float Cooldown { get; protected set; } = 0f;

        /// <summary>
        /// The amount of times this attack has to hit its target to knock it back.
        /// </summary>
        public Vector2 Knockback { get; protected set; } = new Vector2(0, 0);
        public int KnockbackCount { get; protected set; } = 1;
        public KnockbackMode KnockbackMode { get; protected set; } = KnockbackMode.Stack;

        /// <summary>
        /// The time, in seconds, the player will be immuned after recovering from the knockback.
        /// </summary>
        public float Immunity { get; protected set; } = Constants.DEFAULT_IMMUNITY;
        public bool AllowInput { get; protected set; } = false;
        /// <summary>
        /// The speed at which projectiles launched by this move travel.
        /// </summary>
        public float Speed { get; protected set; } = 0f;
        /// <summary>
        /// The distance projectiles launched by this move travel before dissappearing.
        /// </summary>
        //public float Distance { get; protected set; } = 0f;

        public float Duration { get; protected set; } = 0f;

        public int HitCount { get; protected set; } = 0;

        public bool EndOnEnter { get; protected set; } = false;

        public float PointConversion { get; protected set; } = 0.5f;

        public bool FriendlyFire { get; protected set; } = true;
        public bool IgnoreGuard { get; protected set; } = false;
        /// <summary>
        /// An id used to identify this attack between all the attacks of the same digimon. Used to group cooldowns.
        /// </summary>
        public int InternalId { get; protected set; } = -1;
        /// <summary>
        /// The amount of time between the end of the hit and the moment the player regains control of their character,
        /// used to prevent the player from escaping in the middle of a chained attack.
        /// </summary>
        public float BufferTime { get; protected set; } = 0f;

        public DigimonFighter user;

        public Move(DigimonFighter user) {
            this.user = user;
        }

        public virtual float GetDamage() {
            return Damage * user.DamageMultiplier;
        }

        public virtual void OnStart() { }
        public virtual void OnUpdate() { }
        public virtual void OnEnd() { }
        public virtual void OnInterrupt() { }
        public virtual void OnHit() { }
        public virtual void OnFire() { }
        public virtual void OnAttackCollision(Collider2D collision) { }
        public virtual void CallEffect(string effect) { }

        public void IncreaseHitCount() {
            HitCount++;
        }
    }

    /*
     * Stack: after n successful hits.
     * Last: the last hit, regardless of how many hits landed.
     */
    public enum KnockbackMode {
        Stack = 0,
        Last
    }
}