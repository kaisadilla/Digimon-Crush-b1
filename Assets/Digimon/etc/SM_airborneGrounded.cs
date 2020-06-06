using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class SM_airborneGrounded : StateMachineBehaviour {
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle")) {
                DigimonFighter f = animator.gameObject.GetComponent<DigimonFighter>();
                f.Movement.StopAirborne();
            }
        }
    }
}