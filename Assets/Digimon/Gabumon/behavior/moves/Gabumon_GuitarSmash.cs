using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Gabumon_GuitarSmash : Move {
        public Gabumon_GuitarSmash(DigimonFighter user) : base(user) {
            AnimName = "attack_guitarSmash";
            BaseDamage = 8f;
            KnockbackCount = -1;
            EndOnEnter = true;
        }

        public override void OnHit(DigimonFighter target) {
            target.ApplySmash(0.35f, 5f);
        }
    }
}