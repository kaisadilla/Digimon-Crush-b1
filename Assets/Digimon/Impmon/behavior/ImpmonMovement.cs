using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class ImpmonMovement : DigimonMovement {
        // regular
        public override void UseAttack0() {
            CurrentMove = new Impmon_Badaboom(fighter);
            StartCurrentMove();
        }
        // horizontal
        public override void UseAttack1() {
            CurrentMove = new Impmon_BasicAttack(fighter);
            StartCurrentMove();
        }
        // up
        public override void UseAttack2() {
            CurrentMove = new Impmon_EvilSphere(fighter);
            StartCurrentMove();
        }
        // down
        public override void UseAttack3() {
            CurrentMove = new Impmon_ImpExplosion(fighter);
            StartCurrentMove();
        }
        // Jump + regular
        public override void UseAttack4() {
            CurrentMove = new Impmon_Badaboom_Flying(fighter);
            StartCurrentMove();
        }
        // jump + horizontal
        public override void UseAttack5() {
            if (Cooldowns[5] <= 0f) {
                Cooldowns[5] = 2f;
                CurrentMove = new Impmon_DimensionalDash(fighter);
                StartCurrentMove();
            }
        }
        // jump + up
        public override void UseAttack6() {
            if (AirJumpAllowed) {
                AirJumpAllowed = false;
                CurrentMove = new Impmon_MagicUppercut(fighter);
                StartCurrentMove();
            }
        }
        // jump + down
        public override void UseAttack7() {
            CurrentMove = new Impmon_ImpExplosion(fighter);
            StartCurrentMove();
        }
        // running + horizontal
        public override void UseAttack8() {
            if(Cooldowns[8] <= 0f) {
                Cooldowns[8] = 2f;
                CurrentMove = new Impmon_DimensionalDash(fighter);
                StartCurrentMove();
            }
        }
    }
}