using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class AgumonController : DigimonController {
        protected override void Update() {
            base.Update();
            if(fighter.Movement.CurrentMove is Agumon_BabyBurner a) {
                if(Input.GetButtonUp(keys["attack"])) {
                    if(a.AttackLoaded) {
                        fighter.Anim.TriggerAnim(a.AnimName_Succeed);
                        //Movement.EndCurrentMove();
                    }
                    else {
                        fighter.Anim.TriggerAnim(a.AnimName_Fail);
                        //Cancel baby burner.
                    }
                }
            }
        }
    }
}