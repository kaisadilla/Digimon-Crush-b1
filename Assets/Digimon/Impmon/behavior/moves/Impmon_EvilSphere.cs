using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Impmon_EvilSphere : Move {
        private Projectile_EvilSphere proj;
        private bool failed = false;

        public Impmon_EvilSphere(DigimonFighter user) : base(user) {
            AnimName = "attack_evilSphere";
            BaseDamage = 10f;
            Knockback = new Vector2(6, 4);
            Speed = new Vector2(14f, 0f);
            Duration = 4f;
            IgnoreGuard = true;
            EndOnEnter = true;
        }

        public override void OnFire() {
            if (user.CurrentPower >= 2) {
                user.CurrentPower -= 2;
                float x = 1.9f;
                float y = -0.75f;
                GameObject go = user.Movement.LaunchProjectile("impmon/evil_sphere", this, x, y);
                proj = go.GetComponent<Projectile_EvilSphere>();
                proj.SetTarget(user.GetOppositePlayer());
            }
            else {
                failed = true;
            }

        }

        public override void CallEffect(string effect) {
            if (effect == "Launch") Launch();
        }

        private void Launch() {
            if (!failed) {
                proj.Fire();
            }
        }
    }
}