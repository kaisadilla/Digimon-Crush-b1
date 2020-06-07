using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Gabumon_CrushNail : Move {
        public Gabumon_CrushNail(DigimonFighter user) : base(user) {
            AnimName = "attack_crushNail";
            BaseDamage = 0.75f;
            Knockback = new Vector2(2, 4);
            KnockbackCount = 4;
            BufferTime = 0.25f;
        }

        public override void OnStart() {
            user.Movement.MoveX(1, true);
        }
        public override void OnHit(DigimonFighter target) {
            BaseDamage *= 2f;
        }
    }
}