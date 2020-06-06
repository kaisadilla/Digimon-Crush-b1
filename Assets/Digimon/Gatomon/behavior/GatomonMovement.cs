using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class GatomonMovement : DigimonMovement {
        // regular
        public override Move OnAttack0() => new Gatomon_LightningPaw(fighter);
        // horizontal
        public override Move OnAttack1() => new Gatomon_LightningPaw(fighter);
        // up
        public override Move OnAttack2() => new Gatomon_CatTail(fighter);
        // down
        public override Move OnAttack3() => new Gatomon_CatsEye(fighter);
        // Jump + regular
        public override Move OnAttack4() => new Gatomon_LightningPaw_Flying(fighter);
        // jump + horizontal
        public override Move OnAttack5() => new Gatomon_NekoRush(fighter);
        // jump + up
        public override Move OnAttack6() => new Gatomon_CatTail_Flying(fighter);
        // jump + down
        public override Move OnAttack7() => new Gatomon_CatsEye(fighter);
        // running + horizontal
        public override Move OnAttack8() => new Gatomon_Nekodamashi(fighter);

        public override void UseAttack5() {
            if (AirDashAllowed) {
                AirDashAllowed = false;
                Move m = OnAttack5();
                AssignMove(m);
            }
        }
    }
}