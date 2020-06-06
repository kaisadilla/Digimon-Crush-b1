using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class BlackAgumon_NegativeCrunch : Move {
        private bool alreadyGenerated;
        public BlackAgumon_NegativeCrunch(DigimonFighter user) : base(user) {
            AnimName = "attack_negativeCrunch";
            BaseDamage = 12f;
            Knockback = new Vector2(6, 2);
            EndOnEnter = true;
        }

        public override void OnStart() {
            user.Movement.MoveX(1, true);
        }

        public override void OnAttackCollision(Collider2D collision) {
            var proj = collision.gameObject.GetComponent<ProjectileHitbox>();

            if (proj != null && !alreadyGenerated) {
                alreadyGenerated = true;
                // this is so the two diamonds don't always spawn in the exact same relative location, which looks unnatural.
                float offset = Random.Range(-1f, 1f);
                user.SpawnDiamond(collision.transform.position, false, 0.15f);
                user.SpawnDiamond(collision.transform.position += new Vector3(offset, offset, 0), false, 0.15f);
                Object.Destroy(collision.gameObject);
            }
        }
    }
}