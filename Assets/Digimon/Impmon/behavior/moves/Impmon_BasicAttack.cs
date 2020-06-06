using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Impmon_BasicAttack : Move {
        public Impmon_BasicAttack(DigimonFighter user) : base(user) {
            AnimName = "attack_basicAttack";
            BaseDamage = 3.5f;
            Knockback = new Vector2(4, 2);
        }
    }
}