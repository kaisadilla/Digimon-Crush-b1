using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Greymon_HornAttack : Move {
        public Greymon_HornAttack(DigimonFighter user) : base(user) {
            AnimName = "attack_hornAttack";
            BaseDamage = 8f;
            Knockback = new Vector2(2, 8);
            EndOnEnter = true;
        }
    }
}