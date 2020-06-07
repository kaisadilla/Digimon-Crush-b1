using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Guilmon_KurenaiMaru : Move {
        public Guilmon_KurenaiMaru(DigimonFighter user) : base(user) {
            AnimName = "attack_kurenaiMaru";
            BaseDamage = 8f;
            Knockback = new Vector2(0, 5);
            EndOnEnter = true;
            InternalId = 0;
            Cooldown = 3f;
        }

        public override void OnDodge(DigimonFighter target) {
            target.AddEnergy(-target.MaxEnergy);
            user.EnableImmunity(1.5f);
            user.DisableImmunity();
            user.Movement.SetCooldown(InternalId, 0f);
        }
        public override void OnUpdate() {
            user.Movement.SetSpeed(new Vector2(1.5f, 0f), true);
        }
    }
}