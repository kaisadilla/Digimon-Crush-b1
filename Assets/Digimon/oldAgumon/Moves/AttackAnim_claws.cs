using UnityEngine;

namespace Kaisa.DigimonCrush.Moves.Agumon {
    public class AttackAnim_claws : StateMachineBehaviour {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            animator.gameObject.GetComponent<FighterMovement>().EndAttack_claws();
        }
    }
}