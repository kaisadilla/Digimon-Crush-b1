using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class BlackGuilmon_WildUppercut_Empowered : Move {
        public BlackGuilmon_WildUppercut_Empowered(DigimonFighter user) : base(user) {
            AnimName = "attack_wildUppercut_empowered";
            BaseDamage = 9f;
            Knockback = new Vector2(0, 5);
            EndOnEnter = true;
        }
    }
}