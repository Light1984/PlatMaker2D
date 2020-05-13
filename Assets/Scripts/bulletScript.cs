using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class bulletScript : MonoBehaviour
{

    public float speed = 7f;

    private Transform player;
    private Vector2 target;

    string line;
    
    private int bulletC;
    private int bPos = 1;
    private int hg;
    private float Dir;
    int row;

    string folderPath;
    string[] filePaths;


    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, transform.position.y);


        line = File.ReadLines("TestMap.txt").Skip(0).First();

        string[] size = line.Split();
        int row = int.Parse(size[0]);

        line = File.ReadLines("TestMap.txt").Skip(row + 9).First();
        bulletC = int.Parse(line.Split(':')[1]);


    }


    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            Destroy(gameObject, 1);
        }


    }



    private void FixedUpdate()
    {
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
        if (collision.gameObject.tag == "Player")
        {

            Destroy(gameObject, 1);
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
