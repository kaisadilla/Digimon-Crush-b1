using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Gatomon_CatsEye : Move {
        public Gatomon_CatsEye(DigimonFighter user) : base(user) {
            AnimName = "attack_catsEye";
            BaseDamage = 5f;
            KnockbackCount = 0;
            Speed = 12f;
            Duration = 0.85f;
        }

        public override void OnFire() {
            float x = 0f;
            float y = -0.8f;
            user.Movement.LaunchProjectile("gatomon/cats_eye", this, x, y);
        }
    }
}