using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Guilmon_SharpClaw : Move {
        private int totalHits = 0;
        public Guilmon_SharpClaw(DigimonFighter user) : base(user) {
            AnimName = "attack_sharpClaw";
            BaseDamage = 4f;
            EndOnEnter = true;
            KnockbackCount = -1;
        }
        public override void OnStart() {
            user.Movement.StopJump();
        }

        public override void OnHit(DigimonFighter target) {
            totalHits++;
            if (totalHits >= 2) {
                if (target.Movement.IsInPlatform) {
                    target.Movement.MoveY(-(target.Movement.CurrentHeight + 0.125f));
                }
                else {
                    target.Movement.ApplyKnockback(4);
                }
            }
        }
    }
}