using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class Seek : NPCBaseFSM
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        _NPC.GetComponent<NavMeshAgent>().stoppingDistance = 2;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var direction = _player.transform.position - _NPC.transform.position;
        _NPC.transform.rotation = Quaternion.Slerp(_NPC.transform.rotation,
            Quaternion.LookRotation(direction),
            rotSpeed * Time.deltaTime);

        //NPC.transform.Translate(0, 0, Time.deltaTime * speed);
        _NPC.GetComponent<NavMeshAgent>().SetDestination(_player.transform.position);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
