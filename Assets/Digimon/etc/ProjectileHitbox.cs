using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class ProjectileHitbox : AttackHitbox {
        protected float timeToFriendlyFire = 0.5f;
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
                    f.StartHit(Move, owner.bounds.center, Move.PointConversion);
                    bool isHit = f.EndHit();

                    if(isHit) {
                        hits++;
                        Move.OnHit();
                        ExtraEffects(f);
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
                if (meleeH.Move.GetDamage() >= Move.GetDamage()) Destroy(gameObject);
            }
            else if (collision.GetComponent<AttackHitbox>() is ProjectileHitbox projH) {
                if (projH.Move.GetDamage() >= Move.GetDamage()) Destroy(gameObject);
                //else Move.GetDamage() -= projH.Move.GetDamage();
            }
        }
    }
}