using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class SM_InterruptMove : StateMachineBehaviour {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            DigimonFighter f = animator.gameObject.GetComponent<DigimonFighter>();
            f.Movement.InterruptCurrentMove();
        }
    }
}