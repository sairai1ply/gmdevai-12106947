using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    [SerializeField] private GameObject[] _goalLocations;

    NavMeshAgent _agent;

    Animator _animator;
    float _speedMultiplier;

    float _detectionRadius = 5;
    float _fleeRadius = 10;

    private void Start()
    {
        _goalLocations = GameObject.FindGameObjectsWithTag("goal");
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _agent.SetDestination(_goalLocations[Random.Range(0, _goalLocations.Length)].transform.position);

        _animator.SetFloat("wOffset", Random.Range(0.05f, 1.0f));

        ResetAgent();
    }

    private void LateUpdate()
    {
        if (_agent.remainingDistance < 1)
        {
            ResetAgent();
            _agent.SetDestination(_goalLocations[Random.Range(0, _goalLocations.Length)].transform.position);
        }
    }

    public void ResetAgent()
    {
        _speedMultiplier = Random.Range(0.1f, 1.5f);
        _agent.speed = 2 * _speedMultiplier;
        _agent.angularSpeed = 120;

        _animator.SetFloat("speedMult", _speedMultiplier);
        _animator.SetTrigger("isWalking");
        _agent.ResetPath();
    }

    public void DetectNewObstacle(Vector3 location, ObstacleType obstacleType)
    {
        Vector3 direction;
        float distance = Vector3.Distance(location, this.transform.position);

        if (distance < _detectionRadius)
        {
            if (obstacleType == ObstacleType.Flee)
            {
                direction = (this.transform.position - location).normalized;

                Vector3 newGoal = this.transform.position + direction * _fleeRadius;

                NavMeshPath path = new NavMeshPath();
                _agent.CalculatePath(newGoal, path);

                if (path.status != NavMeshPathStatus.PathInvalid)
                {
                    _agent.SetDestination(path.corners[path.corners.Length - 1]);
                    _animator.SetTrigger("isRunning");
                    _agent.speed = 10;
                    _agent.angularSpeed = 500;
                }
            }

            else if (obstacleType == ObstacleType.Flock)
            {
                _agent.SetDestination(location);
            }
        } 
    }
}
