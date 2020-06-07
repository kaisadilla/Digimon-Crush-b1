using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Guilmon_PyroSphere_Flying : Move {
        public Guilmon_PyroSphere_Flying(DigimonFighter user) : base(user) {
            AnimName = "attack_pyroSphere_flying";
            BaseDamage = 8.5f;
            Knockback = new Vector2(3, 2);
            KnockbackCount = 1;
            Speed = new Vector2(13f, 0f);
            Duration = 0.9f;
        }

        public override void OnStart() {
            user.Movement.StopJump();
        }

        public override void OnFire() {
            float x = 1.2f;
            float y = -0.6f;
            user.Movement.LaunchProjectile("guilmon/pyro_sphere", this, x, y);
        }
    }
}