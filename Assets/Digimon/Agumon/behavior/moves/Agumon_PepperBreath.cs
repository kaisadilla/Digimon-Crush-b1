﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Agumon_PepperBreath : Move {
        public Agumon_PepperBreath(DigimonFighter user) : base(user) {
            AnimName = "attack_pepperBreath";
            Damage = 9.2f;
            Knockback = new Vector2(3, 2);
            KnockbackCount = 1;
            Speed = 12f;
            Duration = 1f;
        }

        public override void OnFire() {
            float x = 0f;
            float y = -0.8f;
            user.Movement.LaunchProjectile("agumon/pepper_breath", this, x, y);
        }

        public override void CallEffect(string effect) {
            if (effect == "MoveUser") MoveUser();
        }

        private void MoveUser() {
            user.Movement.PushBack(1.35f);
        }
    }
}