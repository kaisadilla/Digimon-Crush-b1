using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class GatomonMovement : DigimonMovement {
        // regular
        public override void UseAttack0() {
            CurrentMove = new Gatomon_LightningPaw(fighter);
            StartCurrentMove();
        }
        // horizontal
        public override void UseAttack1() {
            CurrentMove = new Gatomon_LightningPaw(fighter);
            StartCurrentMove();
        }
        // up
        public override void UseAttack2() {
            CurrentMove = new Gatomon_CatTail(fighter);
            StartCurrentMove();
        }
        // down
        public override void UseAttack3() {
            CurrentMove = new Gatomon_CatsEye(fighter);
            StartCurrentMove();
        }
        // Jump + regular
        public override void UseAttack4() {
            CurrentMove = new Gatomon_LightningPaw_Flying(fighter);
            StartCurrentMove();
        }
        // jump + horizontal
        public override void UseAttack5() {
            CurrentMove = new Gatomon_NekoRush(fighter);
            StartCurrentMove();
        }
        // jump + up
        public override void UseAttack6() {
            if (AirJumpAllowed) {
                AirJumpAllowed = false;
                CurrentMove = new Gatomon_CatTail_Flying(fighter);
                StartCurrentMove();
            }
        }
        // jump + down
        public override void UseAttack7() {
            CurrentMove = new Gatomon_CatsEye(fighter);
            StartCurrentMove();
        }
        // running + horizontal
        public override void UseAttack8() {
            CurrentMove = new Gatomon_Nekodamashi(fighter);
            StartCurrentMove();
        }
    }
}