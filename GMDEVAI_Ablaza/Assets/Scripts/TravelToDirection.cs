using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelToDirection : MonoBehaviour
{
    [SerializeField] Vector3 _direction = new Vector3(8, 0, 4);
    [SerializeField] private float _movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void LateUpdate()
    {
        transform.Translate(_direction.normalized * _movementSpeed * Time.deltaTime);
    }
}
