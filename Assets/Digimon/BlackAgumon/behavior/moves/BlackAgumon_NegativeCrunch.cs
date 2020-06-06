using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class BlackAgumon_NegativeCrunch : Move {
        public BlackAgumon_NegativeCrunch(DigimonFighter user) : base(user) {
            AnimName = "attack_negativeCrunch";
            Damage = 12f;
            Knockback = new Vector2(6, 2);
            EndOnEnter = true;
        }

        public override void OnStart() {
            user.Movement.MoveX(1, true);
        }

        public override void OnAttackCollision(Collider2D collision) {
            var proj = collision.gameObject.GetComponent<ProjectileHitbox>();

            if (proj != null) {
                user.SpawnDiamond(collision.transform.position, false);
                Object.Destroy(collision.gameObject);
            }
        }
    }
}