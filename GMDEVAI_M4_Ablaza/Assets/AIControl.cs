using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent _agent;

    private void Start()
    {
        this._agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        _agent.stoppingDistance = 7f;
    }
}
