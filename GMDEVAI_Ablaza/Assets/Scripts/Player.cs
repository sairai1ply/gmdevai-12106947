using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] Transform _cam;
    public CharacterController _controller;

    public float turnSmoothTime = 0.1f;
    [SerializeField] private float turnSmoothVelocity;
 
    

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDir.normalized * _movementSpeed * Time.deltaTime);

        }
    }

    private void FixedUpdate()
    {
        /*
        if (Input.GetKey(KeyCode.A))
        {
            _rb.velocity += new Vector3(_movementSpeed * Time.fixedDeltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            _rb.velocity += new Vector3(-_movementSpeed * Time.fixedDeltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            _rb.velocity += new Vector3(0, 0, _movementSpeed * Time.fixedDeltaTime);
        }
        */
    }
}
