using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeFromObstacle : NPCBaseFSM
{
    public void DetectNewObstacle(Vector3 location)
    {
        NavMeshAgent _agent = _navMeshAgent;
        Vector3 direction;
        float distance = Vector3.Distance(location, _NPC.transform.position);

        if (distance < _monsterScript._detectionRadius)
        {
            direction = (_NPC.transform.position - location).normalized;

            Vector3 newGoal = _NPC.transform.position + direction * _monsterScript._fleeRadius;

            NavMeshPath path = new NavMeshPath();
            _agent.CalculatePath(newGoal, path);

            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                _agent.SetDestination(path.corners[path.corners.Length - 1]);
                //_animator.SetTrigger("isRunning");
                _agent.speed = 10;
                _agent.angularSpeed = 500;
            }

        }
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        DetectNewObstacle(_monsterScript._tempThrowableFlareLocation);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
