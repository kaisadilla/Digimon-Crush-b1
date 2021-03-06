﻿using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Renamon_DiamondStorm_Flying : Move {
        private bool launched;
        public Renamon_DiamondStorm_Flying(DigimonFighter user) : base(user) {
            AnimName = "attack_diamondStorm";
            BaseDamage = 10f;
            Knockback = new Vector2(4, 2);
            Speed = new Vector2(14f, -14f);
            Duration = 1f;
            EndOnEnter = true;
            Immunity = 0f;
        }

        public override void OnUpdate() {
            if (!launched) user.Movement.SetSpeed(new Vector2(0f, 0f));
        }

        public override void OnFire() {
            float x = 1.6f;
            float y = -0.9f;
            user.Movement.LaunchProjectile("renamon/diamond_storm_flying", this, x, y);
            launched = true;
            user.Movement.PushBack(new Vector2(1f, 1f));
        }

        public override void OnHit(DigimonFighter target) {
            user.EnableImmunity(3f);
            user.DisableImmunity();
        }
    }
}