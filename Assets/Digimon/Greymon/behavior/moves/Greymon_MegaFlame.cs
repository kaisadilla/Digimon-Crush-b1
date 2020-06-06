using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Greymon_MegaFlame : Move {
        public Greymon_MegaFlame(DigimonFighter user) : base(user) {
            AnimName = "attack_megaFlame";
            Damage = 12f;
            Knockback = new Vector2(4, 2);
            Speed = 12f;
            Duration = 1.35f;
        }

        public override void OnFire() {
            user.Movement.LaunchProjectile("greymon/mega_flame", this, new Vector3(0, 0.8f, 0));
        }

        public override void CallEffect(string effect) {
            if (effect == "MoveUser") MoveUser();
        }

        private void MoveUser() {
            user.Movement.PushBack(-1.5f);
        }
    }
}