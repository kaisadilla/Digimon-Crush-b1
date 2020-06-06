using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class GreymonMovement : DigimonMovement {
        // regular
        public override void UseAttack0() {
            CurrentMove = new Greymon_HornAttack(fighter);
            StartCurrentMove();
        }
        // horizontal
        public override void UseAttack1() {
            CurrentMove = new Greymon_HornAttack(fighter);
            StartCurrentMove();
        }
        // up
        public override void UseAttack2() {
            //CurrentMove = new Agumon_BabyBurner(fighter);
            //StartCurrentMove();
        }
        // down
        public override void UseAttack3() {
            CurrentMove = new Greymon_MegaFlame(fighter);
            StartCurrentMove();
        }
        // Jump + regular
        public override void UseAttack4() {
            CurrentMove = new Greymon_HornAttack(fighter);
            StartCurrentMove();
        }
        // jump + horizontal
        public override void UseAttack5() {
            CurrentMove = new Greymon_FlyingAntler(fighter);
            StartCurrentMove();
        }
        // jump + up
        public override void UseAttack6() {
            if (AirJumpAllowed) {
                AirJumpAllowed = false;
                CurrentMove = new Greymon_HornAttack(fighter);
                StartCurrentMove();
            }
        }
        // jump + down
        public override void UseAttack7() {
            CurrentMove = new Greymon_MegaFlame(fighter);
            StartCurrentMove();
        }
        // running + horizontal
        public override void UseAttack8() {
            CurrentMove = new Greymon_GreatAntler(fighter);
            //CurrentMove = new Greymon_SavageAntler(fighter);
            StartCurrentMove();
        }
    }
}