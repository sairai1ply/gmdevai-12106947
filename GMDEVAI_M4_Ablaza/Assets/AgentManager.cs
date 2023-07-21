using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public GameObject[] _agents;
    [SerializeField] private GameObject _player;

    private void Start()
    {
        _agents = GameObject.FindGameObjectsWithTag("AI");

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit _hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, 1000))
            {
                foreach (GameObject ai in _agents)
                {
                    ai.GetComponent<AIControl>()._agent.SetDestination(_hit.point);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Agents set to follow player");

            foreach (GameObject ai in _agents)
            {
                ai.GetComponent<AIControl>()._agent.SetDestination(_player.transform.position);
            }
        }
    }
}
