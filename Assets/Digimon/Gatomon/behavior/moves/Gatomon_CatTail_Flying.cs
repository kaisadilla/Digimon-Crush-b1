using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Gatomon_CatTail_Flying : Move {
        public Gatomon_CatTail_Flying(DigimonFighter user) : base(user) {
            AnimName = "attack_catTail_flying";
            BaseDamage = 5f;
            Knockback = new Vector2(3, 4);
        }

        public override void OnStart() {
            user.Movement.Jump(0.75f);
        }
    }
}