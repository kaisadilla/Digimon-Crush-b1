using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Impmon_ImpExplosion : Move {
        public bool ExplosionHit { get; private set; } = false;
        public Impmon_ImpExplosion(DigimonFighter user) : base(user) {
            AnimName = "attack_impExplosion";
            BaseDamage = 12f;
            Knockback = new Vector2(7, 7);
            EndOnEnter = true;
            PointConversion = 1f;
            IgnoreGuard = true;
        }

        public override void OnHit(DigimonFighter target) {
            ExplosionHit = true;
        }

        public override void CallEffect(string effect) {
            if (effect == "TriggerEnd") TriggerEnd();
        }

        public void TriggerEnd() {
            if (ExplosionHit) {
                user.Anim.TriggerAnim("attack_impExplosion_succeed");
            }
            else {
                user.Anim.TriggerAnim("attack_impExplosion_failed");
            }
        }

        public override void OnEnd(bool interrupted) {
            if (interrupted) {
                if (ExplosionHit) {
                    user.Anim.TriggerAnim("cancel");
                }
                else {
                    user.Anim.TriggerAnim("cancel");
                }
            }
        }
    }
}