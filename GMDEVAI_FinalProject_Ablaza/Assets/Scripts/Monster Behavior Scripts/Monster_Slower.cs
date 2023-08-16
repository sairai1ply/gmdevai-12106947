using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Slower : Monster
{
    [SerializeField] private float _slowDuration;

    public override void Attack()
    {
        //Change movement speed of player;
        StartCoroutine(CO_StartSlowDuration());
    }

    private IEnumerator CO_StartSlowDuration()
    {
        FPS_Controller controller = _playerGO.GetComponent<FPS_Controller>();
        controller.walkingSpeed *= 0.5f;
        
        yield return new WaitForSeconds(_slowDuration);

        controller.walkingSpeed /= 0.5f;
    }
}
