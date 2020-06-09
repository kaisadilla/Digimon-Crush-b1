using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Projectile : MonoBehaviour {
        [SerializeField] protected ProjectileHitbox hitbox;
        [SerializeField] protected bool staticSprite = false;
        public GameObject user;
        protected Move move;

        protected bool goingLeft = false;
        protected float initialX;
        protected int hits = 0;
        protected Vector2 scale = Vector2.zero;
        protected float duration = 0f;

        public virtual void Setup(GameObject user, Collider2D userHitbox, Move move, bool goingLeft, Vector2 scale) {
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
            float moveX = (goingLeft ? -move.Speed.x : move.Speed.x ) * Time.deltaTime;
            float moveY = move.Speed.y * Time.deltaTime;
            transform.position += new Vector3(moveX, moveY, 0);
            AdjustScale();
        }

        protected virtual void AdjustScale() {
            if (!staticSprite) {
                if (goingLeft) {
                    transform.localScale = new Vector2(-scale.x, scale.y);
                }
                else {
                    transform.localScale = scale;
                }
            }
        }
    }
}