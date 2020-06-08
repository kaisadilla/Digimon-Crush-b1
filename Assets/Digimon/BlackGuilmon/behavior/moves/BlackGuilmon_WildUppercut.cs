using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class BlackGuilmon_WildUppercut : Move {
        public BlackGuilmon_WildUppercut(DigimonFighter user) : base(user) {
            AnimName = "attack_wildUppercut";
            BaseDamage = 6f;
            Knockback = new Vector2(0, 5);
            EndOnEnter = true;
        }
    }
}