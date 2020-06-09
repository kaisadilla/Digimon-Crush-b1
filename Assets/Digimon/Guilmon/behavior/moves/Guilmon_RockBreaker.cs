using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Guilmon_RockBreaker : Move {
        public Guilmon_RockBreaker(DigimonFighter user) : base(user) {
            AnimName = "attack_rockBreaker";
            BaseDamage = 4f;
            Knockback = new Vector2(4, 0);
            EndOnEnter = true;
        }
    }
}