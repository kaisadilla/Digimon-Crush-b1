using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Renamon_SpinKick : Move {
        public Renamon_SpinKick(DigimonFighter user) : base(user) {
            AnimName = "attack_spinKick";
            BaseDamage = 3.5f;
            Knockback = new Vector2(3, 8);
            KnockbackCount = 2;
            KnockbackMode = KnockbackMode.Last;
        }
    }
}