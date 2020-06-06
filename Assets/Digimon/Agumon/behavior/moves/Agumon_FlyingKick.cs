using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Agumon_FlyingKick : Move {
        public Agumon_FlyingKick(DigimonFighter user) : base(user) {
            AnimName = "attack_flyingKick";
            BaseDamage = 5f;
            Knockback = new Vector2(4, 2);
            KnockbackCount = 2;
        }

        public override void OnStart() {
            user.Movement.StopJump();
        }
    }
}