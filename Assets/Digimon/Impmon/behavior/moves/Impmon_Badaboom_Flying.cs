using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Impmon_Badaboom_Flying : Move {
        public Impmon_Badaboom_Flying(DigimonFighter user) : base(user) {
            AnimName = "attack_badaboom_flying";
            Damage = 5f;
            KnockbackCount = -1;
            Speed = 14f;
            Duration = 3f;
            FriendlyFire = true;
        }

        public override void OnUpdate() {
            user.Movement.SetSpeed(new Vector2(0f, 0f));
        }

        public override void OnFire() {
            user.Movement.LaunchProjectile("impmon/badaboom", this, new Vector3(-1.94f, -1.42f, 0));
        }
    }
}