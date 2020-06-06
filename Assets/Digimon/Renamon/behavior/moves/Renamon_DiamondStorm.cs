using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Renamon_DiamondStorm : Move {
        public Renamon_DiamondStorm(DigimonFighter user) : base(user) {
            AnimName = "attack_diamondStorm";
            Damage = 10f;
            Knockback = new Vector2(4, 2);
            Speed = 12f;
            Duration = 1f;
        }

        public override void OnFire() {
            float x = 2.9f;
            float y = -1.6f;
            user.Movement.LaunchProjectile("renamon/diamond_storm", this, x, y);
        }

        public override void OnHit() {
            user.EnableImmunity(3f);
            user.DisableImmunity();
        }
    }
}