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

    string folderPath;
    string[] filePaths;


    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, transform.position.y);
       

        line = File.ReadAllLines("TestMap.txt");

        string[] size = line[0].Split();
         row = int.Parse(size[0]);
        bulletC  = int.Parse(line[row + 9].Split(':')[1]);
        


     


    }




    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            //ImageLoader("Bullet/Explode",1);
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
           // ImageLoader("Bullet/Explode", 1);
            Destroy(gameObject, 1);
        }
    }


    void ImageLoader(string path, int Pos)
    {
        //Create an array of file paths from which to choose
        folderPath = Application.streamingAssetsPath;  //Get path of folder
        filePaths = Directory.GetFiles(folderPath, path + (Pos).ToString() + ".png"); // Get all files of type .png in this folder

        //Converts desired path into byte array
        byte[] pngBytes = System.IO.File.ReadAllBytes(filePaths[0]);

        //Creates texture and loads byte array data to create image
        Texture2D tex = new Texture2D(1, 1);
        tex.LoadImage(pngBytes);

        //Creates a new Sprite based on the Texture2D
        Sprite fromTex = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

        //Assigns the UI sprite
        gameObject.GetComponent<SpriteRenderer>().sprite = fromTex;
    }


     





}
