using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Agumon_PepperBreath : Move {
        public Agumon_PepperBreath(DigimonFighter user) : base(user) {
            AnimName = "attack_pepperBreath";
            Damage = 10f;
            Knockback = new Vector2(3, 2);
            KnockbackCount = 1;
            Speed = 12f;
            Distance = 12f;
        }

        public override void OnFire() {
            user.Movement.LaunchProjectile("agumon/pepper_breath", this, new Vector3(0, 0.8f, 0));
        }

        public override void CallEffect(string effect) {
            if (effect == "MoveUser") MoveUser();
        }

        private void MoveUser() {
            user.Movement.PushBack(-1f);
        }
    }
}