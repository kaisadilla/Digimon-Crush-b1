using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class GabumonMovement : DigimonMovement {
        // regular
        public override Move OnAttack0() => new Gabumon_CrushNail(fighter);
        // horizontal
        public override Move OnAttack1() => new Gabumon_CrushNail(fighter);
        // up
        public override Move OnAttack2() => new Gabumon_BlueBlaster(fighter);
        // down
        public override Move OnAttack3() => new Gabumon_PetitFire(fighter);
        // Jump + regular
        public override Move OnAttack4() => new Gabumon_CrushNail(fighter);
        // jump + horizontal
        public override Move OnAttack5() => new Gabumon_HornAttack_Flying(fighter);
        // jump + up
        public override Move OnAttack6() => new Gabumon_UpperClaw(fighter);
        // jump + down
        public override Move OnAttack7() => new Gabumon_GuitarSmash(fighter);
        // running + horizontal
        public override Move OnAttack8() => new Gabumon_HornAttack(fighter);
        public override void UseAttack5() {
            if (AirDashAllowed) {
                AirDashAllowed = false;
                Move m = OnAttack5();
                AssignMove(m);
            }
        }
    }
}