using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Gatomon_CatTail : Move {
        public Gatomon_CatTail(DigimonFighter user) : base(user) {
            AnimName = "attack_catTail";
            BaseDamage = 5f;
            Knockback = new Vector2(3, 4);
        }
    }
}