using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Gatomon_LightningPaw_Flying : Move {
        public Gatomon_LightningPaw_Flying(DigimonFighter user) : base(user) {
            AnimName = "attack_lightningPaw_flying";
            BaseDamage = 4.5f;
            Knockback = new Vector2(5, 0);
            AllowInput = true;
        }
    }
}