using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bullet;
    public GameObject turret;

    [SerializeField] private float shootDelay;

    private void Start()
    {
        this.gameObject.GetComponent<Health>().Initialize();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartFiring();
        }

        else if (Input.GetMouseButtonUp(0))
        {
            StopFiring();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Fire()
    {
        GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        b.GetComponent<Rigidbody>().AddForce(turret.transform.forward * 500);
    }

    public void StartFiring()
    {
        InvokeRepeating("Fire", 0.0f, shootDelay);
    }

    public void StopFiring()
    {
        CancelInvoke("Fire");
    }

}
