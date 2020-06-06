using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Gabumon_HornAttack : Move {
        public Gabumon_HornAttack(DigimonFighter user) : base(user) {
            AnimName = "attack_hornAttack";
            BaseDamage = 8f;
            Knockback = new Vector2(4, 2);
            EndOnEnter = true;
        }

        public override void OnFire() {
            user.Movement.SetSpeed(2f, true);
        }
    }
}