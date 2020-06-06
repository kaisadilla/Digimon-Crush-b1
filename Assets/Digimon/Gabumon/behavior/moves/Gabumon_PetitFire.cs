using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Gabumon_PetitFire : Move {
        public Gabumon_PetitFire(DigimonFighter user) : base(user) {
            AnimName = "attack_petitFire";
            BaseDamage = 4f;
            Knockback = new Vector2(3f, 0f);
            Speed = 10f;
            Duration = 0.5f;
        }

        public override void OnFire() {
            float x = 1.5f;
            float y = -1f;
            user.Movement.LaunchProjectile("gabumon/petit_fire", this, x, y);
            user.Movement.LaunchProjectile("gabumon/petit_fire", this, -x, y, true);
        }

        public override void OnHit(DigimonFighter target) {
            target.ApplySlow(0.35f, 4f);
        }
    }
}