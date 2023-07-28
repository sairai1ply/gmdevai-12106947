using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flee_ : NPCBaseFSM
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    private void Seek(Vector3 location)
    {
        NPC.GetComponent<NavMeshAgent>().SetDestination(location);
        //Debug.Log("Seeking" + location);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (opponent == null) { return; }
        Vector3 fleeDirection = opponent.transform.position - NPC.transform.position;
        Seek(NPC.transform.position - fleeDirection);
    }
}
