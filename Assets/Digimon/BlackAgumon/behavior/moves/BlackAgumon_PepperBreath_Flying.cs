using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class BlackAgumon_PepperBreath_Flying : Move {
        public static GameObject projectile;
        public BlackAgumon_PepperBreath_Flying(DigimonFighter user) : base(user) {
            AnimName = "attack_pepperBreath_flying";
            BaseDamage = 10f;
            Knockback = new Vector2(3, 2);
            KnockbackCount = 1;
            Speed = new Vector2(12f, 0f);
            Duration = 1f;
            EndOnEnter = true;
        }

        public override void OnStart() {
            user.Movement.StopJump();
        }

        public override void OnFire() {
            float x = 0f;
            float y = -0.8f;
            user.Movement.LaunchProjectile("blackAgumon/pepper_breath", this, x, y);
        }

        public override void CallEffect(string effect) {
            if (effect == "MoveUser") MoveUser();
        }

        private void MoveUser() {
            user.Movement.PushBack(1.35f);
        }
    }
}