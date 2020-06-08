using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class BlackGuilmon_RockBreaker_Empowered : Move {
        public BlackGuilmon_RockBreaker_Empowered(DigimonFighter user) : base(user) {
            AnimName = "attack_rockBreaker_empowered";
            BaseDamage = 6f;
            Knockback = new Vector2(6, 2);
            EndOnEnter = true;
        }
    }
}