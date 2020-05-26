using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Agumon_SharpClaws : Move {
        public Agumon_SharpClaws(DigimonFighter user) : base(user) {
            AnimName = "attack_sharpClaws";
            Damage = 2.5f;
            Knockback = new Vector2(6, 3);
            KnockbackCount = 6;
            KnockbackMode = KnockbackMode.Last;
        }

        public override void OnStart() {
            user.Movement.MoveX(1, true);
        }
        public override void OnUpdate() {
            user.Movement.SetSpeed(0.5f, true);
        }
    }
}