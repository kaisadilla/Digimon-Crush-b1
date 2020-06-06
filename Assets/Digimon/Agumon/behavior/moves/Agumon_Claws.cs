using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Agumon_Claws : Move {
        public Agumon_Claws(DigimonFighter user) : base(user) {
            AnimName = "attack_claws";
            BaseDamage = 3f;
            Knockback = new Vector2(4, 0);
            KnockbackCount = 2;
            BufferTime = 0.5f;
        }

        public override void OnStart() {
            user.Movement.MoveX(1, true);
        }
        public override void OnUpdate() {
            user.Movement.SetSpeed(0.5f, true);
        }
    }
}