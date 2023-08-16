using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class Flee : NPCBaseFSM
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    private void Seek(Vector3 location)
    {
        _NPC.GetComponent<NavMeshAgent>().SetDestination(location);
        //Debug.Log("Seeking" + location);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_player == null) { return; }
        Vector3 fleeDirection = _player.transform.position - _NPC.transform.position;
        Seek(_NPC.transform.position - fleeDirection);
    }
}
