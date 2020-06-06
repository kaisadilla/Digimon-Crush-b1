using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Impmon_DimensionalDash : Move {
        public Impmon_DimensionalDash(DigimonFighter user) : base(user) {
            AnimName = "attack_dimensionalDash";
            Damage = 4f;
            Knockback = new Vector2(0, 5);
            EndOnEnter = true;
            Cooldown = 2f;
            InternalId = 0;
        }

        public override void OnFire() {
            user.Movement.SetGhosted(true);
            user.EnableImmunity(1f);
            user.Movement.SetSpeed(new Vector2(2.25f, 0f), true);
            user.Movement.body.gravityScale = 0f;
        }

        public override void OnEnd() {
            user.Movement.body.gravityScale = 3f;
            user.Movement.SetGhosted(false);
            user.ForceDisableImmunity();
        }
    }
}