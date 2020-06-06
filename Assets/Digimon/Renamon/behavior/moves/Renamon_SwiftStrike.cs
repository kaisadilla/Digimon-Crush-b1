using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Renamon_SwiftStrike : Move {
        public Renamon_SwiftStrike(DigimonFighter user) : base(user) {
            AnimName = "attack_swiftStrike";
            BaseDamage = 6f;
            Knockback = new Vector2(7, 0);
            EndOnEnter = true;
        }

        public override void OnUpdate() {
            user.Movement.SetSpeed(1f, true);
        }
    }
}