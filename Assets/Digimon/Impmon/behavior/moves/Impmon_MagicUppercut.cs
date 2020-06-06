using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Impmon_MagicUppercut : Move {
        public Impmon_MagicUppercut(DigimonFighter user) : base(user) {
            AnimName = "attack_magicUppercut";
            BaseDamage = 4f;
            Knockback = new Vector2(0, 5);
            EndOnEnter = true;
        }

        public override void OnStart() {
            user.Movement.Jump(0.75f);
        }
    }
}