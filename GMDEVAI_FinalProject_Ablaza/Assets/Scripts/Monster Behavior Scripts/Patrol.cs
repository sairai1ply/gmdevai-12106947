using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : NPCBaseFSM
{
    public GameObject[] waypoints;
    private int currentWaypoint;

    private void Awake()
    {
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        foreach (var waypoint in waypoints)
        {
            waypoint.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        currentWaypoint = 0;

        _navMeshAgent.speed *= 0.4f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log(waypoints[currentWaypoint].transform.position);
        if (waypoints.Length <= 0) 
        {
            Debug.Log($"waypoint vector length: {waypoints.Length}");
            return; 
        }

        if (Vector3.Distance(waypoints[currentWaypoint].transform.position,
            _NPC.transform.position) < 3.0f)
        {
            currentWaypoint++;

            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }       
        }

        _navMeshAgent.SetDestination(waypoints[currentWaypoint].transform.position);

        /*
        //rotate
        var direction = waypoints[currentWaypoint].transform.position - _NPC.transform.position;
        _NPC.transform.rotation = Quaternion.Slerp(_NPC.transform.rotation,
                                                   Quaternion.LookRotation(direction),
                                                   1.0f * Time.deltaTime);
        _NPC.transform.Translate(0, 0, Time.deltaTime * 2.0f);
        */


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _navMeshAgent.speed /=  0.4f;
    }
}
