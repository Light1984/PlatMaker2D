using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;


public class bulletScript : MonoBehaviour
{

    private float speed = 200;

    private Transform player;
    private Vector2 target;


    
    private int bulletC = 1;
    private int bPos = 1;
    private int hg;
    private float Dir;


    string folderPath;
    string[] filePaths;
    public Rigidbody2D rb;
    private int time = 0;


  


    void Start()
    {


        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, transform.position.y);



       
        if (gameObject.tag == "Bullet" && FindObjectOfType<CreationScript>().speedBullet2 > 0)
        {
            speed = FindObjectOfType<CreationScript>().speedBullet2;
        }
        else if (FindObjectOfType<CreationScript>().speedBullet1 > 0)
        {
            speed = FindObjectOfType<CreationScript>().speedBullet1;
            rb.velocity = transform.right * speed;
        }

        if(FindObjectOfType<CreationScript>().bullet > 0)
        bulletC = FindObjectOfType<CreationScript>().bullet;
       





    }


    private void Update()
    {
        if (gameObject.tag == "Bullet")
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (transform.position.x == target.x && transform.position.y == target.y)
            {
                Destroy(gameObject, 1);
            }
      
        }




    }



    private void FixedUpdate()
    {

        if (gameObject.tag != "Bullet")
        {
            time++;
            if (time == 180)
                Destroy(gameObject, 1);
        }

        ImageLoader("Bullet/Bullet", bPos);
        if (hg < 15)
            hg++;
        else if (hg == 15 && bPos < bulletC)
        {
            hg = 0;
            bPos++;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.tag == "Bullet")
        {

            Destroy(gameObject, 1);
        }

        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject, 1);
            if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyF")
                Destroy(collision.gameObject);

        }

    }


    void ImageLoader(string path, int Pos)
    {

        folderPath = Application.streamingAssetsPath; 
        filePaths = Directory.GetFiles(folderPath, path + (Pos).ToString() + ".png");

        byte[] pngBytes = System.IO.File.ReadAllBytes(filePaths[0]);

        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(pngBytes);

        Sprite fromTex = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

        gameObject.GetComponent<SpriteRenderer>().sprite = fromTex;
    }


}
