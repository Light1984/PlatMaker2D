using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class bulletScript : MonoBehaviour
{

    public float speed = 7f;

    private Transform player;
    private Vector2 target;

    string[] line;
    
    private int bulletC;
    private int bPos = 1;
    private int hg;
    private float Dir;
    int row;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, transform.position.y);
       

        line = File.ReadAllLines("TestMap.txt");

        string[] size = line[0].Split();
         row = int.Parse(size[0]);
        bulletC  = int.Parse(line[row + 8].Split(':')[1]);
        





    }




    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y)
        {

            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(line[row + 9]);
            Destroy(gameObject, 1);
        }


    }



    private void FixedUpdate()
    {
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Bullet/Bullet" + (bPos).ToString());
        if (hg < 15)
            hg++;
        else if (hg == 15 && bPos < bulletC-1)
        {
            hg = 0;
            bPos++;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           // GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(line[ParticleSystemAnimationRowMode+9]);
            Destroy(gameObject, 1);
        }
    }
}
