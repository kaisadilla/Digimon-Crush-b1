using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Gatomon_LightningPaw : Move {
        public Gatomon_LightningPaw(DigimonFighter user) : base(user) {
            AnimName = "attack_lightningPaw";
            Damage = 3.5f;
            Knockback = new Vector2(5, 0);
            KnockbackCount = 2;
        }

        public override void OnStart() {
            user.Movement.SetSpeed(1f, true);
        }
    }
}