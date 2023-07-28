using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class TankAI : MonoBehaviour
{
    public Animator anim;
    public Health health;
    public GameObject player;
    public GameObject bullet;
    public GameObject turret;

    public GameObject GetPlayer() { return player; }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); 

        health = GetComponent<Health>();
        health.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        anim.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));
    }

    private void Fire()
    {
        GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        
        b.GetComponent<Rigidbody>().AddForce(turret.transform.forward * 500);
    }

    public void StartFiring()
    {
        InvokeRepeating("Fire", 2.5f, 2.5f);
    }

    public void StopFiring()
    {
        CancelInvoke("Fire");
    }
}
