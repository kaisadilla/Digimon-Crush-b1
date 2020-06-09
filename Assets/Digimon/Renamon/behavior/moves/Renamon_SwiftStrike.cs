using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Renamon_SwiftStrike : Move {
        public bool charge = true;
        public Renamon_SwiftStrike(DigimonFighter user) : base(user) {
            AnimName = "attack_swiftStrike";
            BaseDamage = 5f;
            Knockback = new Vector2(6, 6);
            KnockbackCount = 3;
            KnockbackMode = KnockbackMode.Last;
            BufferTime = 0.75f;
            Cooldown = 3f;
        }

        public override void OnUpdate() {
            if (charge) user.Movement.SetSpeed(1.75f, true);
        }

        public override void CallEffect(string effect) {
            if (effect == "StopCharge") StopCharge();
        }

        private void StopCharge() {
            charge = false;
            user.Movement.SetSpeed(0.5f, true);
            user.GetOppositePlayer().GetComponent<DigimonFighter>().Movement.SetSpeed(0f, true);
        }
    }
}