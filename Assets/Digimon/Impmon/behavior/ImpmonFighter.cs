using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class ImpmonFighter : DigimonFighter {
        private bool generatePoint;
        public override void OnDodgeHit(Move move, Vector3 hitPos, float pointConversion) {
            if (generatePoint && !move.IgnoreGuard) {
                CurrentEnergy -= move.GetDamage() / 5f;
                if (CurrentEnergy > 0) {
                    Audio.PlayGather();
                    CurrentPower++;
                    Movement.FacingLeft = hitPos.x < transform.position.x;
                }
                else {
                    if (IsGuarded) IsGuarded = false;
                    OnApplyHit(move, hitPos, pointConversion);
                }
            }
            else {
                CurrentEnergy -= move.GetDamage() / 5f;
                if (CurrentEnergy > 0) {
                    Audio.PlayDodge();
                    Movement.FacingLeft = hitPos.x < transform.position.x;
                }
                else {
                    OnApplyHit(move, hitPos, pointConversion);
                }
            }
        }

        public void EnablePointGeneration() {
            generatePoint = true;
        }
        public void DisablePointGeneration() {
            generatePoint = false;
        }
    }
}