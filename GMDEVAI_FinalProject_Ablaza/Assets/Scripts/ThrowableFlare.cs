using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableFlare : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.transform.parent.gameObject, 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        Monster m = other.GetComponent<Monster>();

        if (m != null)
        {
            m.DetectThrowableFlare(this.transform.parent.gameObject.transform.position);
        }
    }
}
