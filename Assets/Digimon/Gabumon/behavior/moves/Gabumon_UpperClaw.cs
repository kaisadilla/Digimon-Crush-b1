using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Gabumon_UpperClaw : Move {
        public Gabumon_UpperClaw(DigimonFighter user) : base(user) {
            AnimName = "attack_upperClaw";
            BaseDamage = 4f;
            Knockback = new Vector2(1, 4);
            EndOnEnter = true;
        }

        public override void OnStart() {
            user.Movement.Jump(0.75f);
        }
    }
}