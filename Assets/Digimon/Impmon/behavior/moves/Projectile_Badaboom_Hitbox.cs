using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class Projectile_Badaboom_Hitbox : ProjectileHitbox {
        protected override void EnterCollisionWithPlayer(Collider2D collision) {
            if (collision != owner || (Move.FriendlyFire && timeToFriendlyFire <= 0f)) {
                DigimonFighter f = collision.transform.parent.GetComponent<DigimonFighter>();
                if (!f.IsImmune) {
                    f.StartHit(Move, owner.bounds.center, out bool _); // doesn't check for dodge.
                    bool isHit = f.EndHit(Move);

                    if (isHit) {
                        hits++;
                        f.ApplyBurn(2f);
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}