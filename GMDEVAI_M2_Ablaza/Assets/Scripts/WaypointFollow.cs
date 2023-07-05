using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour
{
    //public GameObject[] _waypoints;
    int _currentWaypointIndex = 0;

    public UnityStandardAssets.Utility.WaypointCircuit _circuit;

    [SerializeField] private float _speed;
    [SerializeField] private float _rotSpeed;
    [SerializeField] private float _accuracy;    

    // Start is called before the first frame update
    void Start()
    {
        //_waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_circuit.Waypoints.Length == 0) return;

        GameObject currentWaypoint = _circuit.Waypoints[_currentWaypointIndex].gameObject;

        Vector3 lookAtGoal = new Vector3(currentWaypoint.transform.position.x,
                                        this.transform.position.y,
                                        currentWaypoint.transform.position.z);

        Vector3 direction = lookAtGoal - this.transform.position;

        if (direction.magnitude < 1.0f)
        {
            _currentWaypointIndex++;

            if (_currentWaypointIndex >= _circuit.Waypoints.Length)
            {
                _currentWaypointIndex = 0;
            }
        }


        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    Time.deltaTime * _rotSpeed);

        this.transform.Translate(0, 0, _speed * Time.deltaTime);
    }
}
