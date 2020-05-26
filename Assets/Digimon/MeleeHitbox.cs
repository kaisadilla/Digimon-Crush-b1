using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class MeleeHitbox : AttackHitbox {
        protected override void EnterCollisionWithPlayer(Collider2D collision) {
            if (collision != owner) {
                DigimonFighter f = collision.transform.parent.GetComponent<DigimonFighter>();
                if (!f.IsImmune) {
                    f.StartHit(Move.Damage, owner.bounds.center);
                }
            }
        }

        protected override void ExitCollisionWithPlayer(Collider2D collision) {
            if (collision != owner) {
                DigimonFighter f = collision.transform.parent.GetComponent<DigimonFighter>();
                bool isHit = f.EndHit();

                if (isHit) {
                    hits++;

                    if (Move.KnockbackMode == KnockbackMode.Stack) {
                        if (isHit) {
                            if (hits == Move.KnockbackCount) {
                                if (Move.Knockback.y == 0) {
                                    f.Movement.ApplyKnockback(Move.Knockback.x, Move.Immunity);
                                }
                                else {
                                    f.Movement.ApplyAirborne(Move.Knockback, Move.Immunity);
                                }
                            }
                        }
                    }
                    else if (Move.KnockbackMode == KnockbackMode.Last) {
                        if (Move.HitCount == Move.KnockbackCount) {
                            if (Move.Knockback.y == 0) {
                                f.Movement.ApplyKnockback(Move.Knockback.x, Move.Immunity);
                            }
                            else {
                                f.Movement.ApplyAirborne(Move.Knockback, Move.Immunity);
                            }
                        }
                    }
                }
            }
        }

        protected override void EnterCollisionWithAttack(Collider2D collision) {

        }
    }
}