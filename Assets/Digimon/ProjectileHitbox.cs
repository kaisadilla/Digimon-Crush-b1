using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class ProjectileHitbox : AttackHitbox {
        public void SetOwner(Collider2D owner) {
            this.owner = owner;
        }

        protected override void EnterCollisionWithPlayer(Collider2D collision) {
            if (collision != owner) {
                DigimonFighter f = collision.transform.parent.GetComponent<DigimonFighter>();
                if (!f.IsImmune) {
                    f.StartHit(Move.Damage, owner.bounds.center);
                    bool isHit = f.EndHit();

                    if(isHit) {
                        hits++;
                        if (hits == Move.KnockbackCount) {
                            if (Move.Knockback.y == 0) {
                                f.Movement.ApplyKnockback(Move.Knockback.x, Move.Immunity);
                            }
                            else {
                                f.Movement.ApplyAirborne(Move.Knockback, Move.Immunity);
                            }
                        }
                    }

                    Destroy(gameObject);
                }
            }
        }
        protected override void ExitCollisionWithPlayer(Collider2D collision) {

        }
        protected override void EnterCollisionWithAttack(Collider2D collision) {
            if (collision.GetComponent<AttackHitbox>() is MeleeHitbox meleeH) {
                if (meleeH.Move.Damage >= Move.Damage) Destroy(gameObject);
            }
            else if (collision.GetComponent<AttackHitbox>() is ProjectileHitbox projH) {
                if (projH.Move.Damage >= Move.Damage) Destroy(gameObject);
                //else Move.Damage -= projH.Move.Damage;
            }
        }
    }
}