using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerOp : MonoBehaviour
{
    private Rigidbody2D rb;
    private float Nuts = 0;
    private float Points = 2;
    public bool godMode;
    public double timer = 3000;
    public double startime = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
      
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin")
            Nuts += 1;
        else if (other.tag == "Fallzone" || other.tag == "Enemy" || other.tag == "Bullet")
        {

            Points--;
            rb.AddForce(new Vector2(-1000, 700));
           

        }
        else if (other.tag == "headEnemy")
        {
            rb.AddForce(new Vector2(0, 15000));
        }
        else if (other.tag == "Finish" && Nuts == 3)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);


    }

    private void Update()
    {



        if (Points == -1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        

    }


}