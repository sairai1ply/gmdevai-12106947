using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelForwardToGoal : MonoBehaviour
{
    public Transform _goal;
    [SerializeField] private float _movementSpeed = 5;
    [SerializeField] private float _rotSpeed = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookAtGoal = new Vector3(_goal.position.x, this.transform.position.y, _goal.position.z);

        Vector3 direction = lookAtGoal - this.transform.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), _rotSpeed * Time.deltaTime);

        //transform.LookAt(lookAtGoal);
        
        if (Vector3.Distance(lookAtGoal, transform.position) > 1)
        {
            transform.Translate(0, 0, _movementSpeed * Time.deltaTime);
        }

    }
}
