using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Gabumon_BassSmash : Move {
        public Gabumon_BassSmash(DigimonFighter user) : base(user) {
            AnimName = "attack_bassSmash";
            BaseDamage = 8f;
            KnockbackCount = -1;
            EndOnEnter = true;
            IgnoreGuard = true;
        }

        public override void OnHit(DigimonFighter target) {
            target.ApplySmash(0.35f, 5f);
        }
    }
}