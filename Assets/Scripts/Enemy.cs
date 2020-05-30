using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float dirY;
    float dirX;
    private float speed = 60;
    public bool up = true;

    public float timeBtwShots = 5;
    private float startBtwShots;
    public GameObject projectile;


    private int moveC = 4;
    private int mPos = 1;
    private int hg = 0;
    private int ePos = 1;

    string folderPath;
    string[] filePaths;

    private int fireC = 4;
    private int efireC = 0;
    private bool faceR = true;





    private void Start()
    {
        if(FindObjectOfType<CreationScript>().speed > 0)
        speed = FindObjectOfType<CreationScript>().speed;

        if(FindObjectOfType<CreationScript>().fire > 0)
        fireC = FindObjectOfType<CreationScript>().fire;

        if(FindObjectOfType<CreationScript>().efire > -1)
        efireC = FindObjectOfType<CreationScript>().efire;

        if(FindObjectOfType<CreationScript>().fly > 0)
        moveC = FindObjectOfType<CreationScript>().fly;

        if(FindObjectOfType<CreationScript>().period > 0)
        startBtwShots = FindObjectOfType<CreationScript>().period;


        dirY = transform.position.y;
        dirX = transform.position.x;
        timeBtwShots = startBtwShots;

       

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




            ImageLoader("Fly/Fly", mPos);
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
                ImageLoader("Fire/Fire", mPos);
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
                ImageLoader("Fire/eFire", ePos);
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
