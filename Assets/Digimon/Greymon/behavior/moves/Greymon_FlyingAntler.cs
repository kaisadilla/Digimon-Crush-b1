using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Greymon_FlyingAntler : Move {
        private bool charging = true;

        public Greymon_FlyingAntler(DigimonFighter user) : base(user) {
            AnimName = "attack_flyingAntler";
            Damage = 8f;
            Knockback = new Vector2(8, 2);
            EndOnEnter = true;
        }

        public override void OnUpdate() {
            if (charging) {
                user.Movement.SetSpeed(new Vector2(3f, 0f), true);
            }
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