using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class RenamonMovement : DigimonMovement {
        // regular
        public override Move OnAttack0() => new Renamon_SpinKick(fighter);
        // horizontal
        public override Move OnAttack1() => new Renamon_SpinKick(fighter);
        // up
        public override Move OnAttack2() => new Renamon_Kohenkyo(fighter);
        // down
        public override Move OnAttack3() => new Renamon_DiamondStorm(fighter);
        // Jump + regular
        public override Move OnAttack4() => new Renamon_Korenkyaku(fighter);
        // jump + horizontal
        public override Move OnAttack5() => new Renamon_Korenkyaku(fighter);
        // jump + up
        public override Move OnAttack6() => new Renamon_Kohenkyo(fighter);
        // jump + down
        public override Move OnAttack7() => new Renamon_DiamondStorm_Flying(fighter);
        // running + horizontal
        public override Move OnAttack8() => new Renamon_SwiftStrike(fighter);
    }
}