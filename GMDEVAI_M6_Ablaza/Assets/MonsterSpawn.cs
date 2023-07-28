using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType
{
    Flee,
    Flock
}

public class MonsterSpawn : MonoBehaviour
{
    public GameObject[] _obstacle;
    [SerializeField] private Material[] _material;

    GameObject[] _agents;

    private void Start()
    {
        _agents = GameObject.FindGameObjectsWithTag("agent");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Instantiate(_obstacle[0], hit.point, _obstacle[0].transform.rotation);

                foreach (GameObject a in _agents)
                {
                    a.GetComponent<AIControl>().DetectNewObstacle(hit.point, ObstacleType.Flee);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Instantiate(_obstacle[1], hit.point, _obstacle[1].transform.rotation);

                foreach (GameObject a in _agents)
                {
                    a.GetComponent<AIControl>().DetectNewObstacle(hit.point, ObstacleType.Flock);
                }
            }
        }
    }
}
