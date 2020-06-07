using Kaisa.DigimonCrush.Fighter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Projectile_EvilSphere : Projectile {
        private GameObject target;
        private bool fired = false;

        protected override void OnStart() {
            initialX = transform.position.x;
            AdjustScale();
        }

        protected override void OnUpdate() {
            if (fired) {
                Travel();
                duration += Time.deltaTime;
                if (duration > move.Duration) {
                    Destroy(gameObject);
                }
            }
        }

        protected override void Travel() {
            float moveX = (goingLeft ? -move.Speed.x : move.Speed.x) * Time.deltaTime;
            float moveY;

            moveY = (target?.transform.position.y > transform.position.y ? 4 : -4) * Time.deltaTime;

            transform.position += new Vector3(moveX, moveY, 0);
            AdjustScale();
        }

        public void SetTarget(GameObject target) {
            this.target = target;
        }

        public void Fire() {
            fired = true;
        }
    }
}