using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class AgumonMovement : DigimonMovement {
        // regular
        public override void UseAttack0() {
            CurrentMove = new Agumon_Claws(fighter);
            StartCurrentMove();
        }
        // horizontal
        public override void UseAttack1() {
            CurrentMove = new Agumon_Claws(fighter);
            StartCurrentMove();
        }
        // up
        public override void UseAttack2() {
            CurrentMove = new Agumon_BabyBurner(fighter);
            StartCurrentMove();
        }
        // down
        public override void UseAttack3() {
            CurrentMove = new Agumon_PepperBreath(fighter);
            StartCurrentMove();
        }
        // Jump + regular
        public override void UseAttack4() {
            CurrentMove = new Agumon_FlyingKick(fighter);
            StartCurrentMove();
        }
        // jump + horizontal
        public override void UseAttack5() {
            CurrentMove = new Agumon_FlyingKick(fighter);
            StartCurrentMove();
        }
        // jump + up
        public override void UseAttack6() {
            if (AirJumpAllowed) {
                AirJumpAllowed = false;
                CurrentMove = new Agumon_Uppercut(fighter);
                StartCurrentMove();
            }
        }
        // jump + down
        public override void UseAttack7() {
            CurrentMove = new Agumon_FlyingPepperBreath(fighter);
            StartCurrentMove();
        }
        // running + horizontal
        public override void UseAttack8() {
            CurrentMove = new Agumon_SharpClaws(fighter);
            StartCurrentMove();
        }
    }
}