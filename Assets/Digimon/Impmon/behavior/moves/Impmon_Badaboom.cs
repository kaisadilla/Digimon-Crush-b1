using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Impmon_Badaboom : Move {
        public Impmon_Badaboom(DigimonFighter user) : base(user) {
            AnimName = "attack_badaboom";
            Damage = 5f;
            KnockbackCount = -1;
            Speed = 8f;
            Duration = 3f;
            FriendlyFire = true;
        }

        public override void OnFire() {
            user.Movement.LaunchProjectile("impmon/badaboom", this, new Vector3(-0.94f, -1.42f, 0));
        }
    }
}