using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    Transform _goal;

    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _accuracy = 1.0f;
    [SerializeField] private float _rotSpeed = 2.0f;

    public GameObject _wpManager;
    GameObject[] _wps;
    [SerializeField] GameObject _currentNode;
    [SerializeField] int _currentWaypointIndex = 0;
    Graph _graph;
    private void Start()
    {
        _wps = _wpManager.GetComponent<WaypointManager>()._waypoints;
        _graph = _wpManager.GetComponent<WaypointManager>()._graph;
        _currentNode = _wps[13];
    }

    private void LateUpdate()
    {
        if (_graph.getPathLength() == 0 || _currentWaypointIndex == _graph.getPathLength()) 
        {
            return;
        }

        _currentNode = _graph.getPathPoint(_currentWaypointIndex);

        if (Vector3.Distance(_graph.getPathPoint(_currentWaypointIndex).transform.position,
            transform.position) < _accuracy)
        {
            _currentWaypointIndex++;
        }

        if (_currentWaypointIndex < _graph.getPathLength())
        {
            _goal = _graph.getPathPoint(_currentWaypointIndex).transform;
            Vector3 lookAtGoal = new Vector3(_goal.position.x, transform.position.y, _goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
                                                        Quaternion.LookRotation(direction),
                                                        Time.deltaTime * _rotSpeed);
            this.transform.Translate(0, 0, _speed * Time.deltaTime);
        }
    }

    public void GoToHelipad()
    {
        _graph.AStar(_currentNode, _wps[0]);
        _currentWaypointIndex = 0;
    }
    public void GoToTwinMountains()
    {
        _graph.AStar(_currentNode, _wps[2]);
        _currentWaypointIndex = 0;
    }
    public void GoToRuins()
    {
        _graph.AStar(_currentNode, _wps[16]);
        _currentWaypointIndex = 0;
    }
    public void GoToBarracks()
    {
        _graph.AStar(_currentNode, _wps[14]);
        _currentWaypointIndex = 0;
    }
    public void GoToCommandCenter()
    {
        _graph.AStar(_currentNode, _wps[20]);
        _currentWaypointIndex = 0;
    }
    public void GoToOilRefineryPumps()
    {
        _graph.AStar(_currentNode, _wps[5]);
        _currentWaypointIndex = 0;
        _currentNode = _graph.getPathPoint(_currentWaypointIndex);
    }

    public void GoToTankers()
    {
        _graph.AStar(_currentNode, _wps[17]);
        _currentWaypointIndex = 0;
    }

    public void GoToRadar()
    {
        _graph.AStar(_currentNode, _wps[18]);
        _currentWaypointIndex = 0;
    }

    public void GoToCommandPost()
    {
        _graph.AStar(_currentNode, _wps[19]);
        _currentWaypointIndex = 0;
    }

    public void GoToMid()
    {
        _graph.AStar(_currentNode, _wps[12]);
        _currentWaypointIndex = 0;
    }
}
