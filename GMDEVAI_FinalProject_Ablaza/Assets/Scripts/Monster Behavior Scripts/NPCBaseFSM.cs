using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBaseFSM : StateMachineBehaviour
{
    public GameObject _NPC;
    public NavMeshAgent _navMeshAgent;
    public Monster _monsterScript;
    public GameObject _player;
    public float speed = 2.0f;
    public float rotSpeed = 1.0f;
    public float accuracy = 3.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //base.OnStateEnter(animator, stateInfo, layerIndex);
        _NPC = animator.gameObject;
        _navMeshAgent = _NPC.GetComponent<NavMeshAgent>();
        _monsterScript = _NPC.GetComponent<Monster>();
        _player = _NPC.GetComponent<Monster>().GetPlayer();
    }
}
