using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AgentBehavior
{
    Pursuer,
    Hider,
    Evader
}

[RequireComponent(typeof(SphereCollider))]
public class AIControl : MonoBehaviour
{
    [SerializeField] private AgentBehavior _type;
    private string _behaviorWhileTargetInRange;
    private bool _isTargetInRange;

    NavMeshAgent _agent;

    public GameObject _target;

    public WASDMovement _playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();  
        _playerMovement = _target.GetComponent<WASDMovement>();
        _isTargetInRange = false;

        switch (_type)
        {
            case AgentBehavior.Pursuer:
                {
                    _behaviorWhileTargetInRange = "Pursue";
                }
                break;
            case AgentBehavior.Hider:
                {
                    _behaviorWhileTargetInRange = "CleverHide";
                }
                break;
            case AgentBehavior.Evader:
                {
                    _behaviorWhileTargetInRange = "Evade";
                }
                break;
            default:
                {
                    _behaviorWhileTargetInRange = "Pursue";
                    Debug.Log("No _type. Default setting Pursue()");
                }
                break;
        }
    }

    void Seek(Vector3 location)
    {
        _agent.SetDestination(location);
    }

    void Flee(Vector3 location)
    {
        Vector3 fleeDirection = location - this.transform.position;
        _agent.SetDestination(this.transform.position - fleeDirection);
    }

    void Pursue()
    {
        Vector3 targetDirection = _target.transform.position - this.transform.position;

        float lookAhead = targetDirection.magnitude / (_agent.speed + _playerMovement.currentSpeed);

        Seek(_target.transform.position + _target.transform.forward * lookAhead);
    }

    void Evade()
    {
        Vector3 targetDirection = _target.transform.position - this.transform.position;

        float lookAhead = targetDirection.magnitude / (_agent.speed + _playerMovement.currentSpeed);

        Flee(_target.transform.position + _target.transform.forward * lookAhead);
    }

    Vector3 _wanderTarget;

    void Wander()
    {
        float wanderRadius = 20;
        float wanderDistance = 10;
        float wanderJitter = 1;

        _wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter,
            0,
            Random.Range(-1.0f, 1.0f) * wanderJitter);
        _wanderTarget.Normalize();
        _wanderTarget *= wanderRadius;

        Vector3 targetLocal = _wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

        Seek(targetWorld);
    }

    void Hide()
    {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;

        int hidingSpotsCount = World.Instance.GetHidingSpots().Length;

        for (int i = 0; i < hidingSpotsCount; i++)
        {
            Vector3 hideDirection = World.Instance.GetHidingSpots()[i].transform.position - _target.transform.position;
            Vector3 hidePosition = World.Instance.GetHidingSpots()[i].transform.position + hideDirection.normalized * 5; //distance offset

            float spotDistance = Vector3.Distance(this.transform.position, hidePosition);

            if (spotDistance < distance)
            {
                chosenSpot = hidePosition;
                distance = spotDistance; 
            }

            Seek(chosenSpot);
        }
    }

    bool CanSeeTarget()
    {
        RaycastHit raycastInfo;
        Vector3 rayToTarget = _target.transform.position - this.transform.position;

        if (Physics.Raycast(this.transform.position, rayToTarget, out raycastInfo))
        {
            return raycastInfo.transform.gameObject.tag == "Player";
        }

        return false;
    }

    void CleverHide()
    {
        if (!CanSeeTarget())
        {
            return;
        }

        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDir = Vector3.zero;
        GameObject chosenGameObject = World.Instance.GetHidingSpots()[0];

        int hidingSpotsCount = World.Instance.GetHidingSpots().Length;

        for (int i = 0; i < hidingSpotsCount; i++)
        {
            Vector3 hideDirection = World.Instance.GetHidingSpots()[i].transform.position - _target.transform.position;
            Vector3 hidePosition = World.Instance.GetHidingSpots()[i].transform.position + hideDirection.normalized * 5; //distance offset

            float spotDistance = Vector3.Distance(this.transform.position, hidePosition);

            if (spotDistance < distance)
            {
                chosenSpot = hidePosition;
                chosenDir = hideDirection;
                chosenGameObject = World.Instance.GetHidingSpots()[i];
                distance = spotDistance;
            }
        }

        Collider hideCol = chosenGameObject.GetComponent<Collider>();
        Ray back = new Ray(chosenSpot, -chosenDir.normalized);
        RaycastHit info;
        float rayDistance = 100f;
        hideCol.Raycast(back, out info, rayDistance);

        Seek(info.point);
    }

    private void Update()
    {
        if (!_isTargetInRange)
        {
            Wander();
        }

        else if (_isTargetInRange)
        {
            Invoke(_behaviorWhileTargetInRange, 0f);
            Debug.Log(_behaviorWhileTargetInRange);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<WASDMovement>() != null)
        {
            _isTargetInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<WASDMovement>() != null)
        {
            _isTargetInRange = false;
        }
    }
}
