using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Projectile_DiamondStorm : Projectile {
        [SerializeField] private bool diagonal;
        private bool launched;

        protected override void OnStart() {
            AdjustScale();
        }

        protected override void OnUpdate() {
            if (launched) {
                Travel();
                duration += Time.deltaTime;
                if (duration > move.Duration) {
                    Destroy(gameObject);
                }
            }
        }

        protected override void Travel() {
            float moveX = (goingLeft ? -move.Speed : move.Speed ) * Time.deltaTime;
            float moveY = (diagonal ? -move.Speed : 0f) * Time.deltaTime;
            transform.position += new Vector3(moveX, moveY, 0);
            AdjustScale();
        }

        public void Launch() {
            launched = true;
        }
    }
}