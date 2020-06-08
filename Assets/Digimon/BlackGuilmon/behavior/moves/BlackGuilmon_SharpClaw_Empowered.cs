using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class BlackGuilmon_SharpClaw_Empowered : Move {
        private int totalHits = 0;
        public BlackGuilmon_SharpClaw_Empowered(DigimonFighter user) : base(user) {
            AnimName = "attack_sharpClaw_empowered";
            BaseDamage = 6f;
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