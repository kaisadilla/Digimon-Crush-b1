using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Greymon_MegaFlame : Move {
        public Greymon_MegaFlame(DigimonFighter user) : base(user) {
            AnimName = "attack_megaFlame";
            BaseDamage = 12f;
            Knockback = new Vector2(4, 2);
            Speed = new Vector2(12f, 0f);
            Duration = 1.35f;
            EndOnEnter = true;
        }

        public override void OnFire() {
            float x = 0f;
            float y = 0.8f;
            user.Movement.LaunchProjectile("greymon/mega_flame", this, x, y);
        }

        public override void CallEffect(string effect) {
            if (effect == "MoveUser") MoveUser();
        }

        private void MoveUser() {
            user.Movement.PushBack(2f);
        }
    }
}