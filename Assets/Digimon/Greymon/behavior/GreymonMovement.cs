using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class GreymonMovement : DigimonMovement {
        // regular
        public override Move OnAttack0() => new Greymon_HornAttack(fighter);
        // horizontal
        public override Move OnAttack1() => new Greymon_HornAttack(fighter);
        // up
        public override Move OnAttack2() => new Agumon_BabyBurner(fighter);
        // down
        public override Move OnAttack3() => new Greymon_MegaFlame(fighter);
        // Jump + regular
        public override Move OnAttack4() => new Greymon_HornAttack(fighter);
        // jump + horizontal
        public override Move OnAttack5() => new Greymon_FlyingAntler(fighter);
        // jump + up
        public override Move OnAttack6() => new Greymon_HornAttack(fighter);
        // jump + down
        public override Move OnAttack7() => new Greymon_MegaFlame(fighter);
        // running + horizontal
        public override Move OnAttack8() => new Greymon_GreatAntler(fighter);
    }
}