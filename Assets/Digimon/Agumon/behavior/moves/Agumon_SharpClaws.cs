﻿using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Agumon_SharpClaws : Move {
        public Agumon_SharpClaws(DigimonFighter user) : base(user) {
            AnimName = "attack_sharpClaws";
            BaseDamage = 2.75f;
            Knockback = new Vector2(6, 3);
            KnockbackCount = 6;
            KnockbackMode = KnockbackMode.Last;
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