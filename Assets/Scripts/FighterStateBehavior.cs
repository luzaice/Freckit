using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterStateBehavior : StateMachineBehaviour
{
    public float horizontalForce;
    public float verticalForce;

    public FighterStates behaviorState;
    public AudioClip soundEffect;

    protected Fighter fighter;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (fighter == null)
        {
            fighter = animator.gameObject.GetComponent<Fighter>();
        }

        fighter.currentState = behaviorState;

        if (soundEffect != null)
        {
            fighter.playSound(soundEffect);
        }

        fighter.body.AddRelativeForce(new Vector3(0, verticalForce, 0));
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fighter.body.AddRelativeForce(new Vector3(0, 0, horizontalForce * fighter.speedMultiplier));
    }
}
