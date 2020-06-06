using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Fighter {
    public class SM_EndMove : StateMachineBehaviour {
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            DigimonFighter f = animator.gameObject.GetComponent<DigimonFighter>();
            f.Movement.SetSpeed(0f, true);
            f.Movement.EndCurrentMove();
        }
    }
}