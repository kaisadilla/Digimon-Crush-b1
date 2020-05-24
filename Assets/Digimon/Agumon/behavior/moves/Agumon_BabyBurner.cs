using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Agumon_BabyBurner : Move {
        public bool AttackLoaded { get; private set; } = false;

        public string AnimName_Succeed { get; private set; } = "attack_babyBurner_succeed";
        public string AnimName_Fail { get; private set; } = "attack_babyBurner_fail";

        public Agumon_BabyBurner(DigimonFighter user) : base(user) {
            AnimName = "attack_babyBurner_fill";
            Damage = 15f;
            Knockback = new Vector2(6, 4);
            KnockbackCount = 1;
            Speed = 16f;
            Distance = 24f;
        }

        public override void OnFire() {
            user.Movement.LaunchProjectile("agumon/baby_burner", this, new Vector3(0, 1f, 0));
        }

        public override void CallEffect(string effect) {
            if (effect == "MoveUser") MoveUser();
            else if (effect == "LoadAttack") LoadAttack();
            else if (effect == "ExpireEffect") ExpireEffect();
        }

        private void MoveUser() {
            user.Movement.PushBack(-2f);
        }

        private void LoadAttack() {
            AttackLoaded = true;
        }

        private void ExpireEffect() {
            user.Anim.TriggerAnim(AnimName_Fail);
        }
    }
}