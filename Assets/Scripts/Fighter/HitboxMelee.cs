using UnityEngine;

namespace Kaisa.DigimonCrush {
    public class HitboxMelee : MonoBehaviour {
        [SerializeField] private GameObject owner; //The character using this attack. Its collision will be ignored.

        private int hits = 0;

        private float damage = 1f;
        private int knockbackCount = 0;
        private float knockbackX, knockbackY;
        private float immunity;

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject != owner && collision.gameObject.CompareTag("Player")) {
                FighterC f = collision.gameObject.GetComponent<FighterC>();
                if (!f.IsImmune) {
                    f.GetHit(damage, transform.position);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.gameObject != owner && collision.gameObject.CompareTag("Player")) {
                FighterC f = collision.gameObject.GetComponent<FighterC>();
                bool hitEnded = f.EndHit();

                if(hitEnded) {
                    if (hits == knockbackCount) {
                        f.movement.ApplyKnockback(knockbackX, knockbackY, immunity);
                    }

                    hits++;
                }
            }
        }

        /// <summary>
        /// Sets up information about the attack in the hitbox.
        /// </summary>
        /// <param name="damage">The damage of each instance of the attack.</param>
        /// <param name="knockbackCount">The number of times the attack has to hit its target to knock it back.
        /// If this equals -1, the attack won't apply any knockback.</param>
        /// <param name="knockbackX">The strength of the knockback in the X axis.</param>
        /// <param name="knockbackY">The strength of the knockback in the Y axis.</param>
        /// <param name="immunity">The duration (in seconds) of the immunity after being knocked back.</param>
        public void Setup(float damage, int knockbackCount, float knockbackX = 0, float knockbackY = 0, float immunity = 0.5f) {
            hits = 0;
            this.damage = damage;
            this.knockbackCount = knockbackCount - 1;
            this.knockbackX = knockbackX;
            this.knockbackY = knockbackY;
            this.immunity = immunity;
        }
        /*
        private void SetDamage(float damage) {
            this.damage = damage;
        }
        public void SetKnockbackCount(int count) {
            this.knockbackCount = count;
        }*/
    }
}