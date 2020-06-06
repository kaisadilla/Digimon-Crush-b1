using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Renamon_Korenkyaku : Move {
        public Renamon_Korenkyaku(DigimonFighter user) : base(user) {
            AnimName = "attack_korenkyaku";
            Damage = 5f;
            Knockback = new Vector2(2, 6);
        }
    }
}