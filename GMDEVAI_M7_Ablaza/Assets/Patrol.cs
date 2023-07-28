using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : NPCBaseFSM
{
    GameObject[] waypoints;
    int currentWaypoint;

    private void Awake()
    {
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        currentWaypoint = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (waypoints.Length == 0) { return; }

        if (Vector3.Distance(waypoints[currentWaypoint].transform.position,
            NPC.transform.position) < 3.0f)
        {
            currentWaypoint++;
            
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }

        //rotate
        var direction = waypoints[currentWaypoint].transform.position - NPC.transform.position;
         NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation,
                                                    Quaternion.LookRotation(direction), 
                                                    1.0f * Time.deltaTime);
        NPC.transform.Translate(0, 0, Time.deltaTime * 2.0f);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
