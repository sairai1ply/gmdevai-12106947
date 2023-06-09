using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class TravelToGoal : MonoBehaviour
{
    public Transform _goal;
    float _movementSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt( _goal );
        Vector3 direction = _goal.position - this.transform.position;

        if (direction.magnitude > 1)
        {
            transform.Translate(direction.normalized * _movementSpeed * Time.deltaTime, Space.World);
        }
    }
}
