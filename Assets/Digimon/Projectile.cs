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

        public void Setup(GameObject user, Collider2D userHitbox, Move move, bool goingLeft) {
            this.user = user;
            this.move = move;
            this.goingLeft = goingLeft;
            hitbox.Setup(move);
            hitbox.SetOwner(userHitbox);
        }

        protected virtual void Start() {
            initialX = transform.position.x;
        }
        protected virtual void Update() {
            float moveX = move.Speed * Time.deltaTime;
            transform.position += new Vector3(goingLeft ? -moveX : moveX, 0, 0);
            if (goingLeft) transform.localScale = new Vector3(-1, 1, 1);
            if ((!goingLeft && transform.position.x >= initialX + move.Distance)
                || (goingLeft && transform.position.x <= initialX - move.Distance)) {
                Destroy(gameObject);
            }
        }
    }
}