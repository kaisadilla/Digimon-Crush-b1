using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Gabumon_HornAttack_Flying : Move {
        public Gabumon_HornAttack_Flying(DigimonFighter user) : base(user) {
            AnimName = "attack_hornAttack_flying";
            BaseDamage = 6f;
            Knockback = new Vector2(3, 2);
            EndOnEnter = true;
        }

        public override void OnUpdate() {
            user.Movement.SetSpeed(new Vector2(1.75f, 0f), true);
        }

        public override void OnEnd() {
            user.Movement.SetSpeed(0f, true);
        }
    }
}