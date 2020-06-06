using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class AgumonMovement : DigimonMovement {
        // regular
        public override Move OnAttack0() => new Agumon_Claws(fighter);
        // horizontal
        public override Move OnAttack1() => new Agumon_Claws(fighter);
        // up
        public override Move OnAttack2() => new Agumon_BabyBurner(fighter);
        // down
        public override Move OnAttack3() => new Agumon_PepperBreath(fighter);
        // Jump + regular
        public override Move OnAttack4() => new Agumon_FlyingKick(fighter);
        // jump + horizontal
        public override Move OnAttack5() => new Agumon_FlyingKick(fighter);
        // jump + up
        public override Move OnAttack6() => new Agumon_Uppercut(fighter);
        // jump + down
        public override Move OnAttack7() => new Agumon_FlyingPepperBreath(fighter);
        // running + horizontal
        public override Move OnAttack8() => new Agumon_SharpClaws(fighter);
    }
}