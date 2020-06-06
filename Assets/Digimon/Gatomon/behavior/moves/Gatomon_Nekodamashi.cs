using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Gatomon_Nekodamashi : Move {
        public Gatomon_Nekodamashi(DigimonFighter user) : base(user) {
            AnimName = "attack_nekodamashi";
            Damage = 12f;
            Knockback = new Vector2(6, 2);
            EndOnEnter = true;
        }

        public override void OnStart() {
            user.Movement.SetSpeed(0f, true);
        }

        public override void OnFire() {
            user.Movement.SetSpeed(new Vector2(4f, 4f), true);
        }

        public override void CallEffect(string effect) {
            if (effect == "EndJump") EndJump();
        }

        private void EndJump() {
            user.Movement.SetSpeed(0.5f, true);
        }
    }
}