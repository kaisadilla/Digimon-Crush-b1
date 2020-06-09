using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public abstract class Move {
        #region attributes
        /// <summary>
        /// The name of the animation for this attack.
        /// </summary>
        public string AnimName { get; protected set; } = "null";
        /// <summary>
        /// The base damage of this attack. Use GetDamage() to get the actual damage of this attack at any given moment.
        /// </summary>
        public float BaseDamage { get; set; } = 0f;

        /// <summary>
        /// The cooldown of the attack. Will only work if the InternalId of the attack is between 0 and 9 (inclusive).
        /// </summary>
        public float Cooldown { get; protected set; } = 0f;
        /// <summary>
        /// An id used to identify this attack between all the attacks of the same digimon. Used to group cooldowns.
        /// </summary>
        public int InternalId { get; protected set; } = -1;

        /// <summary>
        /// The speed applied to the target when the knockback is applied. If y is not 0, it will become an airborne.
        /// </summary>
        public Vector2 Knockback { get; protected set; } = new Vector2(0, 0);
        /// <summary>
        /// The amount of times this attack has to hit its target to knock it back.
        /// </summary>
        public int KnockbackCount { get; protected set; } = 1;
        /// <summary>
        /// The mode in which the attack decides when it should apply a knockback.
        /// </summary>
        public KnockbackMode KnockbackMode { get; protected set; } = KnockbackMode.Stack;

        /// <summary>
        /// The speed at which projectiles launched by this move travel.
        /// </summary>
        public Vector2 Speed { get; protected set; } = Vector2.zero;
        /// <summary>
        /// The duration in seconds of projectiles launched by this move before dissappearing.
        /// </summary>
        public float Duration { get; protected set; } = 0f;
        /// <summary>
        /// The amount of times this projectile can hit before being destroyed. Set to -1 if the projectile should never be destroyed.
        /// </summary>
        public int MaxHits { get; protected set; } = 1;

        /// <summary>
        /// The time, in seconds, the player will be immuned after recovering from the knockback.
        /// </summary>
        public float Immunity { get; protected set; } = Constants.DEFAULT_IMMUNITY;
        /// <summary>
        /// Whether the USER of the attack can still move while the attack is being used.
        /// </summary>
        public bool AllowInput { get; protected set; } = false;
        /// <summary>
        /// If true, the attack finishes its hit in the same frame it starts. If false, the attack doesn't finish until the player leaves the hitbox of the attack.
        /// </summary>
        public bool EndOnEnter { get; protected set; } = false;
        /// <summary>
        /// The amount of time between the end of the hit and the moment the player regains control of their character,
        /// used to prevent the player from escaping in the middle of a chained attack.
        /// </summary>
        public float BufferTime { get; protected set; } = 0f;
        /// <summary>
        /// If true, the attack can hit its target even if they're guarding.
        /// </summary>
        public bool IgnoreGuard { get; protected set; } = false;
        /// <summary>
        /// Whether the attack can hit its user.
        /// </summary>
        public bool FriendlyFire { get; protected set; } = true;
        /// <summary>
        /// The amount of damage that is converted into points.
        /// </summary>
        public float PointConversion { get; protected set; } = 0.5f;
        /// <summary>
        /// If true, enemy actions (mainly moves) are interrupted when hit with this move.
        /// </summary>
        public bool Interrupt { get; protected set; } = true;
        #endregion

        public DigimonFighter user;
        public int HitCount { get; protected set; } = 0;

        public Move(DigimonFighter user) {
            this.user = user;
        }

        public virtual float GetDamage() {
            return BaseDamage * user.DamageMultiplier;
        }

        public virtual void OnStart() { }
        public virtual void OnUpdate() { }
        public virtual void OnEnd(bool interrupted) { }
        //public virtual void OnInterrupt() { }
        public virtual void OnHit(DigimonFighter target) { }
        public virtual void OnDodge(DigimonFighter target) { }
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