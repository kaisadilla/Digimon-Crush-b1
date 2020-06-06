using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Gatomon_NekoRush : Move {
        public Gatomon_NekoRush(DigimonFighter user) : base(user) {
            AnimName = "attack_nekoRush";
            Damage = 4f;
            Knockback = new Vector2(6, 2);
            Cooldown = 2f;
        }

        public override void OnUpdate() {
            user.Movement.SetSpeed(new Vector2(3f, 0f), true);
        }
    }
}