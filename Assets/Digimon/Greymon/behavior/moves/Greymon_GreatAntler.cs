using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Greymon_GreatAntler : Move {
        private bool charging = false;

        public Greymon_GreatAntler(DigimonFighter user) : base(user) {
            AnimName = "attack_greatAntler";
            BaseDamage = 6f;
            Knockback = new Vector2(5, 2);
            EndOnEnter = true;
        }

        public override void OnUpdate() {
            if (charging) {
                user.Movement.SetSpeed(2f, true);
            }
        }

        public override void OnFire() {
            charging = true;
        }

        public override void CallEffect(string effect) {
            if (effect == "EndCharge") EndCharge();
        }

        private void EndCharge() {
            charging = false;
            user.Movement.SetSpeed(1f, true);
        }
    }
}