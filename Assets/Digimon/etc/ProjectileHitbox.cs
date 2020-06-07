using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class ProjectileHitbox : AttackHitbox {
        protected float timeToFriendlyFire = 0.5f;
        protected int absoluteHits = 0; // counts every hit, even if the target was not affected.
        public void SetOwner(Collider2D owner) {
            this.owner = owner;
        }

        protected void Update() {
            if (timeToFriendlyFire > 0f) timeToFriendlyFire -= Time.deltaTime;
        }

        protected override void EnterCollisionWithPlayer(Collider2D collision) {
            if (collision != owner || (Move.FriendlyFire && timeToFriendlyFire <= 0f)) {
                DigimonFighter f = collision.transform.parent.GetComponent<DigimonFighter>();
                if (!f.IsImmune) {
                    f.StartHit(Move, owner.bounds.center, out bool dodged, Move.PointConversion);
                    if (dodged) {
                        Move.OnDodge(f);
                    }
                    if (Move.EndOnEnter) {
                        EndHit(f);
                    }
                }
                absoluteHits++;

                if (Move.MaxHits > 0 && absoluteHits >= Move.MaxHits) {
                    Destroy(gameObject);
                }
            }
        }
        protected override void ExitCollisionWithPlayer(Collider2D collision) {
            DigimonFighter f = collision.transform.parent.GetComponent<DigimonFighter>();
            if (!Move.EndOnEnter) {
                EndHit(f);
            }
        }

        protected void EndHit(DigimonFighter f) {
            bool isHit = f.EndHit(Move);

            if (isHit) {
                hits++;
                Move.OnHit(f);
                //ExtraEffects(f);
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

        protected override void EnterCollisionWithAttack(Collider2D collision) {
            if (collision.GetComponent<AttackHitbox>() is MeleeHitbox meleeH) {
                if (meleeH.Move.GetDamage() >= Move.GetDamage()) Destroy(gameObject);
            }
            else if (collision.GetComponent<AttackHitbox>() is ProjectileHitbox projH) {
                if (projH.Move.GetDamage() >= Move.GetDamage()) Destroy(gameObject);
                //else Move.GetDamage() -= projH.Move.GetDamage();
            }
        }
    }
}