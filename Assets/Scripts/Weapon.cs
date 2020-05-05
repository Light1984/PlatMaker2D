using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float speed;
    public float stopDist;
    public float retreatDist;
    private Transform player;

    public float timeBtwShots;
    public float startBtwShots;
    public GameObject projectile;

    private void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startBtwShots;
    }

    private void Update()
    {
        // if(Vector2.Distance)



        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime; 
        }


    }

}
