using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float dirY;
    float dirX;
    public float speed = 30f;
    public bool up = true;

    public float timeBtwShots;
    public float startBtwShots;
    public GameObject projectile;

    string line;
    private int moveC;
    private int mPos = 1;
    private int hg = 0;
    private int ePos = 1;

    string folderPath;
    string[] filePaths;

    private string fire;
    private int fireC;
    private int efireC;
    private bool faceR = true;



    private void Start()
    {

        dirY = transform.position.y;
        dirX = transform.position.x;
        timeBtwShots = startBtwShots;


        line = File.ReadLines("TestMap.txt").Skip(0).First();
        string[] size = line.Split();
        int row = int.Parse(size[0]);

       


        if (gameObject.tag == "Enemy")
        {
            line = File.ReadLines("TestMap.txt").Skip(row + 7).First();
            moveC = int.Parse(line.Split(':')[1]);
        }
        else
        {
            line = File.ReadLines("TestMap.txt").Skip(row + 8).First();
            fire = line.Split(':')[1];
            fireC = int.Parse(fire.Split(',')[0]);
            efireC = int.Parse(fire.Split(',')[1]);

        }



    }
    private void Update()
    {
        if (gameObject.tag == "EnemyF")
        {
            if (GameObject.FindGameObjectWithTag("Player").transform.position.x < transform.position.x && !faceR || GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x && faceR)
            {
                faceR = !faceR;
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            }
       


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


    private void FixedUpdate()
    {
        if (gameObject.tag == "Enemy")
        {
            if (transform.position.y > dirY + CreationScript.step)
                up = false;

            else if (transform.position.y < dirY - CreationScript.step)
                up = true;

            if (up)
                transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
            else
                transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);




            ImageLoader("Enemy1/Fly", mPos);
            if (hg < 5)
                hg++;
            else if (hg == 5 && mPos < moveC)
            {
                hg = 0;
                mPos++;
            }
            else if (hg == 5 && mPos == moveC)
            {
                hg = 0;
                mPos = 1;
            }
        }
        else
        {
            
            if (timeBtwShots <= 2 && efireC !=0 || efireC == 0)
            {
                ImageLoader("Enemy2/Fire", mPos);
                if (hg < 5)
                    hg++;
                else if (hg == 5 && mPos < fireC)
                {
                    hg = 0;
                    mPos++;
                }
                else if (hg == 5 && mPos == fireC)
                {
                    hg = 0;
                    mPos = 1;
                }
            }
            else
            {
                ImageLoader("Enemy2/eFire", ePos);
                if (hg < 5)
                    hg++;
                else if (hg == 5 && ePos < efireC)
                {
                    hg = 0;
                    ePos++;
                }
                else if (hg == 5 && ePos == efireC)
                {
                    hg = 0;
                    ePos = 1;
                }
            }

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
