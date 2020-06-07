using Kaisa.DigimonCrush.Fighter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Projectile_CatsEye : ProjectileHitbox {
        protected override void EnterCollisionWithPlayer(Collider2D collision) {
            if (collision != owner) {
                DigimonFighter f = collision.transform.parent.GetComponent<DigimonFighter>();
                if (!f.IsImmune) {
                    f.StartHit(Move, owner.bounds.center, out bool _); // doesn't check for dodge.
                    bool isHit = f.EndHit(Move);

                    if (isHit) {
                        hits++;
                        f.ApplyParalyze(2f);
                    }

                    Destroy(gameObject);
                }
            }
        }
    }
}