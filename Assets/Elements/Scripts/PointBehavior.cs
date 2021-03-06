﻿using UnityEngine;
using Kaisa.DigimonCrush.Fighter;
using System.Collections;

namespace Kaisa.DigimonCrush {
    public class PointBehavior : MonoBehaviour {
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Rigidbody2D body;

        [SerializeField] private Sprite diamond;
        [SerializeField] private Sprite[] point;
        [SerializeField] private Sprite[] bigPoint;

        private bool canBePicked;
        private bool isDiamond;
        private int player;
        private bool isBig;

        public void SetupPoint(int player, bool isBig, bool launch = true, float pickupTime = 0.5f) {
            isDiamond = false;
            this.player = player;
            this.isBig = isBig;

            if (isBig) {
                sprite.sprite = bigPoint[player];
            }
            else {
                sprite.sprite = point[player];
            }

            if (launch) {
                Launch();
            }
            StartCoroutine(EnablePickupPoint(pickupTime));
        }

        public void SetupDiamond(bool launch = true, float pickupTime = 0.5f) {
            isDiamond = true;
            sprite.sprite = diamond;

            if (launch) {
                Launch();
            }
            StartCoroutine(EnablePickupPoint(pickupTime));
        }

        private void Launch() {
            float forceX = Random.Range(-15f, 15f);
            float forceY = Random.Range(20f, 30f);

            //body.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);
            //body.AddForce(new Vector2(1f, 3f), ForceMode2D.Impulse);
            body.velocity = new Vector2(forceX, forceY);
        }

        private IEnumerator EnablePickupPoint(float pickupTime = 0.5f) {
            yield return new WaitForSeconds(pickupTime);
            canBePicked = true;
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (canBePicked) {
                if (collision.gameObject.CompareTag("Player")) {
                    DigimonFighter f = collision.gameObject.GetComponent<DigimonFighter>();
                    if (isDiamond) {
                        f.GatherDiamond();
                    }
                    else {
                        f.GatherPoint(player, isBig);
                    }
                    Destroy(gameObject);
                }
            }
        }

    }
}