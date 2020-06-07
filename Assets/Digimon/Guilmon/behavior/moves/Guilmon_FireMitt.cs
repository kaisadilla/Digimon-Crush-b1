using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Guilmon_FireMitt : Move {
        private bool launched;
        public Guilmon_FireMitt(DigimonFighter user) : base(user) {
            AnimName = "attack_fireMitt";
            BaseDamage = 10f;
            Knockback = new Vector2(1, 5);
            KnockbackCount = 1;
            Speed = new Vector2(0f, 13f);
            Duration = 3f;
        }

        public override void OnUpdate() {
            if (!launched) {
                user.Movement.SetSpeed(new Vector2(0f, 0f));
            }
        }

        public override void OnFire() {
            launched = true;
            float x = 0f;
            float y = 0f;
            user.Movement.LaunchProjectile("guilmon/fire_mitt", this, x, y);
            user.Movement.SetSpeed(new Vector2(0f, -20f));
        }
    }
}