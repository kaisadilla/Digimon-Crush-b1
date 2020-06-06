using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Impmon_Badaboom : Move {
        public Impmon_Badaboom(DigimonFighter user) : base(user) {
            AnimName = "attack_badaboom";
            BaseDamage = 5f;
            KnockbackCount = -1;
            Speed = 14f;
            Duration = 3f;
            EndOnEnter = true;
        }

        public override void OnFire() {
            float x = 0.94f;
            float y = 1.42f;
            user.Movement.LaunchProjectile("impmon/badaboom", this, x, y);
        }
    }
}