using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    public Transform _goal;

    public float _speed = 0;

    public float _rotSpeed = 5;

    public float _acceleration = 50;

    public float _deceleration = 50;

    public float _minSpeed = 0;
    public float _maxSpeed = 100;

    public float _breakAngle = 20;
    private void LateUpdate()
    {
        Vector3 lookAtGoal = new Vector3(_goal.position.x, this.transform.position.y, _goal.position.z);
        Vector3 direction = lookAtGoal - this.transform.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                        Quaternion.LookRotation(direction),
                                                        Time.deltaTime * _rotSpeed);

        if (Vector3.Angle(_goal.forward, this.transform.forward) > _breakAngle && _speed > 20)
        {
            _speed = Mathf.Clamp(_speed - (_deceleration * Time.deltaTime), _minSpeed, _maxSpeed);
        }

        else
        {
            _speed = Mathf.Clamp(_speed + (_acceleration * Time.deltaTime), _minSpeed, _maxSpeed);
        }

        this.transform.Translate(0,0, _speed * Time.deltaTime);
    }
}
