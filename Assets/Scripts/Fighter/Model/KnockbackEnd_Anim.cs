using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush {
    public class KnockbackEnd_Anim : StateMachineBehaviour {
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            FighterMovement f = animator.gameObject.GetComponent<FighterMovement>();
            f.RegainControl();
        }
    }
}