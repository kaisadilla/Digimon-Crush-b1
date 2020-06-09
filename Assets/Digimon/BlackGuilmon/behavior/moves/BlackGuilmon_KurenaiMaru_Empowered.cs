using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class BlackGuilmon_KurenaiMaru_Empowered : Move {
        public BlackGuilmon_KurenaiMaru_Empowered(DigimonFighter user) : base(user) {
            AnimName = "attack_kurenaiMaru_empowered";
            BaseDamage = 11.25f;
            Knockback = new Vector2(0, 5);
            EndOnEnter = true;
            InternalId = 0;
            Cooldown = 3f;
        }

        public override void OnStart() {
            user.Movement.SetGhosted(true);
        }
        public override void OnDodge(DigimonFighter target) {
            target.AddEnergy(-target.MaxEnergy);
            user.EnableImmunity(2.25f);
            user.DisableImmunity();
            user.Movement.SetCooldown(InternalId, 0f);
        }
        public override void OnUpdate() {
            user.Movement.SetSpeed(new Vector2(1.5f, 0f), true);
        }
        public override void OnEnd(bool interrupted) {
            user.Movement.SetGhosted(false);
        }
    }
}