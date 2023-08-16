using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class Wander : NPCBaseFSM
{
    private float _exStopDistance;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        _exStopDistance = _NPC.GetComponent<NavMeshAgent>().stoppingDistance;
        _NPC.GetComponent<NavMeshAgent>().stoppingDistance = 0;
    }

    private void Seek(Vector3 location)
    {
        _NPC.GetComponent<NavMeshAgent>().SetDestination(location);
        //Debug.Log("Wandering towards" + location);
    }


    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float wanderRadius = 10;
        float wanderDistance = 5;
        float wanderJitter = 0.3f;

        Vector3 _wanderTarget = _NPC.transform.position;

        _wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter,
            0,
            Random.Range(-1.0f, 1.0f) * wanderJitter);
        _wanderTarget.Normalize();
        _wanderTarget *= wanderRadius;

        Vector3 targetLocal = _wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = _NPC.gameObject.transform.InverseTransformVector(targetLocal);

        Seek(targetWorld);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _NPC.GetComponent<NavMeshAgent>().stoppingDistance = _exStopDistance;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
