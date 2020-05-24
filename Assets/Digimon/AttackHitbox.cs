using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public abstract class AttackHitbox : MonoBehaviour {
        [SerializeField] protected Collider2D owner; //The collider of this Digimon. Collisions with it will be ignored.
        [SerializeField] protected Collider2D hitbox; //The collider of this attack.

        public Move Move { get; private set; }
        protected int hits = 0; //The amount of times this attack has hit its target.

        public void Setup(Move move) {
            if (move == null) return;

            hits = 0;
            Move = move;
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag("PlayerHitbox")) {
                EnterCollisionWithPlayer(collision);
            }
            else if (collision.CompareTag("AttackHitbox")) {
                EnterCollisionWithAttack(collision);
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collision) {
            if (collision.CompareTag("PlayerHitbox")) {
                ExitCollisionWithPlayer(collision);
            }
        }

        protected abstract void EnterCollisionWithPlayer(Collider2D collision);
        protected abstract void ExitCollisionWithPlayer(Collider2D collision);
        protected abstract void EnterCollisionWithAttack(Collider2D collision);

    }
}