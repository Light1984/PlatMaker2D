using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOp : MonoBehaviour
{
    private Rigidbody2D rb;
    public float Points = 0;
    public bool godMode;
    public double timer = 3000;
    public double startime = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        godMode = false;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin")
            Points += 5;
        else if ((other.tag == "Fall" || other.tag == "Enemy") && godMode == false)
        {

            Points--;
            rb.AddForce(new Vector2(-1000, 700));
            godMode = true;

        }
        else if (other.tag == "headEnemy")
        {
            rb.AddForce(new Vector2(0, 15000));
        }


    }

    private void Update()
    {



        if (godMode)
        {

            while (startime <= timer)
            {
                startime += 0.01;
             //   print(startime);
            }
            startime = 0;
            godMode = false;
        }

        if (Points == -1)
            Destroy(gameObject);

       

    }


}