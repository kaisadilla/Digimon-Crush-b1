using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Agumon_Uppercut : Move {
        public Agumon_Uppercut(DigimonFighter user) : base(user) {
            AnimName = "attack_uppercut";
            Damage = 5f;
            Knockback = new Vector2(2, 5);
            KnockbackCount = 1;
            EndOnEnter = true;
        }

        public override void OnStart() {
            user.Movement.Jump(0.75f);
        }
    }
}