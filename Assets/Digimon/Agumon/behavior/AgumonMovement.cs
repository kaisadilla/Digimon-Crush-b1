using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class AgumonMovement : DigimonMovement {
        public override void UseAttack0() {
            CurrentMove = new Agumon_Claws(fighter);
            StartCurrentMove();
        }
        public override void UseAttack1() {
            CurrentMove = new Agumon_Claws(fighter);
            StartCurrentMove();
        }
        public override void UseAttack2() {
            CurrentMove = new Agumon_BabyBurner(fighter);
            StartCurrentMove();
        }
        public override void UseAttack3() {
            CurrentMove = new Agumon_PepperBreath(fighter);
            StartCurrentMove();
        }
        public override void UseAttack4() {
            CurrentMove = new Agumon_FlyingKick(fighter);
            StartCurrentMove();
        }
        public override void UseAttack5() {
            CurrentMove = new Agumon_FlyingKick(fighter);
            StartCurrentMove();
        }
        public override void UseAttack6() {
            //Uppercut (jump + up + attack)
        }
        public override void UseAttack7() {
            CurrentMove = new Agumon_FlyingPepperBreath(fighter);
            StartCurrentMove();
        }
        public override void UseAttack8() {
            CurrentMove = new Agumon_SharpClaws(fighter);
            StartCurrentMove();
        }
    }
}