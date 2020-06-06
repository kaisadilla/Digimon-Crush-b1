using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class MeleeHitbox : AttackHitbox {
        protected override void EnterCollisionWithPlayer(Collider2D collision) {
            if (collision != owner || Move.FriendlyFire) {
                DigimonFighter f = collision.transform.parent.GetComponent<DigimonFighter>();
                if (!f.IsImmune) {
                    f.StartHit(Move, owner.bounds.center, Move.PointConversion);
                    if (Move.EndOnEnter) {
                        EndHit(collision);
                    }
                }
            }
        }

        protected override void ExitCollisionWithPlayer(Collider2D collision) {
            if (!Move.EndOnEnter) {
                EndHit(collision);
            }
        }

        protected void EndHit(Collider2D collision) {
            if (collision != owner) {
                DigimonFighter f = collision.transform.parent.GetComponent<DigimonFighter>();
                bool isHit = f.EndHit();

                if (isHit) {
                    hits++;
                    Move.OnHit();

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