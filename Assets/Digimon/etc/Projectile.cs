using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Projectile : MonoBehaviour {
        [SerializeField] protected ProjectileHitbox hitbox;
        public GameObject user;
        protected Move move;

        protected bool goingLeft = false;
        protected float initialX;
        protected int hits = 0;
        protected float scale = 1;
        protected float duration = 0f;

        public virtual void Setup(GameObject user, Collider2D userHitbox, Move move, bool goingLeft, float scale) {
            this.user = user;
            this.move = move;
            this.goingLeft = goingLeft;
            this.scale = scale;
            hitbox.Setup(move);
            hitbox.SetOwner(userHitbox);
        }

        protected virtual void Start() {
            OnStart();
        }
        protected virtual void Update() {
            OnUpdate();
        }

        protected virtual void OnStart() {
            initialX = transform.position.x;
        }

        protected virtual void OnUpdate() {
            Travel();
            duration += Time.deltaTime;
            if (duration > move.Duration) {
                Destroy(gameObject);
            }
        }

        protected virtual void Travel() {
            float moveX = (goingLeft ? -move.Speed : move.Speed ) * Time.deltaTime;
            transform.position += new Vector3(moveX, 0, 0);
            AdjustScale();
        }

        protected virtual void AdjustScale() {
            if (goingLeft) {
                transform.localScale = new Vector3(-scale, scale, scale);
            }
            else {
                transform.localScale = new Vector3(scale, scale, scale);
            }
        }
    }
}