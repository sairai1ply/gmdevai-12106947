using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Dormant : Monster
{
    [SerializeField] private float _dormantTimer;


    // Start is called before the first frame update
    override protected void Awake()
    {
        base.Awake();

        StartCoroutine(CO_StartDormantTimer(_dormantTimer));
    }

    private IEnumerator CO_StartDormantTimer(float timer)
    {
        float baseSpeed = _agent.speed;
        _agent.speed = 0;

        yield return new WaitForSeconds(timer);

        _agent.speed = _agent.speed;
        Debug.Log("Dormant Monster is now Active");
    }
}
