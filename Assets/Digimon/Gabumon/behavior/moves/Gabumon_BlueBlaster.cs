using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Gabumon_BlueBlaster : Move {
        private int hitCount = 0;
        public Gabumon_BlueBlaster(DigimonFighter user) : base(user) {
            AnimName = "attack_blueBlaster";
            BaseDamage = 4.5f;
            KnockbackCount = -1;
            Speed = new Vector2(0f, 0f);
            Duration = 1.6f;
            BufferTime = 0.5f;
            MaxHits = -1;
            FriendlyFire = false;
            EndOnEnter = true;
        }

        public override void OnFire() {
            float x = 3.2f;
            float y = -0.9f;
            user.Movement.AttachProjectile("gabumon/blue_blaster", this, x, y);
        }

        public override void OnHit(DigimonFighter target) {
            hitCount++;
            if(hitCount == 4) {
                target.ApplyBurn(2f);
            }
        }
    }
}