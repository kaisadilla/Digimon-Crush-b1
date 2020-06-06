using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class ImpmonMovement : DigimonMovement {
        // regular
        public override Move OnAttack0() => new Impmon_Badaboom(fighter);
        // horizontal
        public override Move OnAttack1() => new Impmon_BasicAttack(fighter);
        // up
        public override Move OnAttack2() => new Impmon_EvilSphere(fighter);
        // down
        public override Move OnAttack3() => new Impmon_ImpExplosion(fighter);
        // Jump + regular
        public override Move OnAttack4() => new Impmon_Badaboom_Flying(fighter);
        // jump + horizontal
        public override Move OnAttack5() => new Impmon_BasicAttack(fighter);
        // jump + up
        public override Move OnAttack6() => new Impmon_MagicUppercut(fighter);
        // jump + down
        public override Move OnAttack7() => new Impmon_ImpExplosion(fighter);
        // running + horizontal
        public override Move OnAttack8() => new Impmon_DimensionalDash(fighter);
    }
}