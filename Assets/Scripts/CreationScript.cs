using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using System;

public class CreationScript : MonoBehaviour
{
    public static int step = 90;
    public GameObject[] objects;
    

    string[] textFile;
    int length;
    string before;

    string folderPath;
    string[] filePaths;

    public float speedBullet1;
    public float speedBullet2;
    public int fire;
    public int efire;
    public int bullet;
    public int period;
    public int fly;
    public int speed;


    

    void Start()
    {


        ImageLoader("Block/Block1.png", 1);
        ImageLoader("Block/Block2.png", 2);
        ImageLoader("Block/Block3.png", 3);
        ImageLoader("Block/Others1.png",14);
        ImageLoader("Block/BlockF.png", 10);
        ImageLoader("Key/Key1.png", 4);
        ImageLoader("Back.png", 12);
        ImageLoader("Finish.png", 11);

        textFile = File.ReadAllLines("TestMap.txt");


        string[] size = textFile[0].Split();
        int row = int.Parse(size[0]);
        int col = int.Parse(size[1]);


         length = textFile.Length;




        char[][] field = new char[row][];
        for (int i = 0; i < row; ++i)
        {
            field[i] = new char[col];
            field[i] = textFile[i+1].ToCharArray();

        }

        int obj = -1;
        for (int i = 0; i < row; ++i)
        {
            for (int j = 0; j < col; ++j)
            {
                switch (field[i][j])
                {
                    case '1':
                        obj = 0;
                        break;
                    case '2':
                        obj = 1;
                        break;
                    case '3':
                        obj = 2;
                        break;
                    case '4':
                        obj = 3;
                        break;
                    case '5':
                        obj = 4;
                        break;
                    case '6':
                        obj = 5;
                        break;
                    case '7':
                        obj = 6;
                        break;
                    case 'b':
                        obj = 10;
                        break;
                    case 'c':
                        obj = 11;
                        break;
                    case 'd':
                        obj = 14;
                        break;

                }
                if (obj > 0 && obj <14)
                    Instantiate(objects[obj], new Vector2(j * step, (9 - i) * step), Quaternion.identity);
                else if (obj == 0)
                {
                    Instantiate(objects[obj], new Vector2(j * step, (9 - i) * step), Quaternion.identity);
                    Instantiate(objects[13], new Vector2(j * step, (9 - i) * step), Quaternion.identity);
                }
                else if(obj == 14)
                    Instantiate(objects[obj], new Vector2(j * step, (10 - i) * step), Quaternion.identity);
                obj = -1;

            }
        }

        Instantiate(objects[7], new Vector2(0, -step * 7), Quaternion.identity);
        Instantiate(objects[12], new Vector2(0, 0), Quaternion.identity);
        Instantiate(objects[12], new Vector2(2700, 0), Quaternion.identity);



        for (int i = row + 1; i < length; ++i)
        {
            before = textFile[i].Split(':')[0];
            switch (before)
            {
                case "Health":
                    FindObjectOfType<Movement>().lives = int.Parse(textFile[i].Split(':')[1]);
                    break;
                case "Keys":
                    FindObjectOfType<Movement>().nutsMax = int.Parse(textFile[i].Split(':')[1]);
                    break;
                case "spriteStay":
                    FindObjectOfType<Movement>().idleC = int.Parse(textFile[i].Split(':')[1]);
                    break;
                case "spriteJump":
                    FindObjectOfType<Movement>().jumpC = int.Parse(textFile[i].Split(':')[1].Split(',')[0]);
                    FindObjectOfType<Movement>().idealJ = int.Parse(textFile[i].Split(':')[1].Split(',')[1]);
                    break;
                case "spriteSpeed":
                    FindObjectOfType<Movement>().runC = int.Parse(textFile[i].Split(':')[1].Split(',')[0]);
                    FindObjectOfType<Movement>().idealR = int.Parse(textFile[i].Split(':')[1].Split(',')[1]);
                    break;
                case "spriteHit":
                    FindObjectOfType<Movement>().damC = int.Parse(textFile[i].Split(':')[1]);
                    break;
                case "Speed":
                    FindObjectOfType<Movement>().speed = int.Parse(textFile[i].Split(':')[1]);
                    break;
                case "jumpForce":
                    FindObjectOfType<Movement>().jumpForce = int.Parse(textFile[i].Split(':')[1]);
                    break;
                case "heroAttack":
                    FindObjectOfType<Movement>().typeAttack = int.Parse(textFile[i].Split(':')[1].Split(',')[0]);
                    speedBullet1 = int.Parse(textFile[i].Split(':')[1].Split(',')[1]);
                    break;
                case "spriteFly":
                    fly = int.Parse(textFile[i].Split(':')[1]);
                    break;
                case "enemySpeed":
                    speed = int.Parse(textFile[i].Split(':')[1]);
                    break;
                case "bulletPeriod":
                    period = int.Parse(textFile[i].Split(':')[1]);
                    break;
                case "spriteBullet":
                   bullet = int.Parse(textFile[i].Split(':')[1]);
                    break;
                case "bulletSpeed":
                    speedBullet2 = int.Parse(textFile[i].Split(':')[1]);
                    break;
                case "spriteFire/eFire":
                    fire = int.Parse(textFile[i].Split(':')[1].Split(',')[0]);
                    efire = int.Parse(textFile[i].Split(':')[1].Split(',')[1]);
                    break;




            }


        }




    }


    


    void ImageLoader(string path, int obj)
    {
        //Create an array of file paths from which to choose
        folderPath = Application.streamingAssetsPath;  //Get path of folder
            filePaths = Directory.GetFiles(folderPath, path); // Get all files of type .png in this folder
       

        //Converts desired path into byte array
        byte[] pngBytes = System.IO.File.ReadAllBytes(filePaths[0]);

        //Creates texture and loads byte array data to create image
        Texture2D tex = new Texture2D(1, 1);
        tex.LoadImage(pngBytes);

        //Creates a new Sprite based on the Texture2D
        Sprite fromTex = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

        //Assigns the UI sprite
        objects[obj].GetComponent<SpriteRenderer>().sprite = fromTex;
    }



   



}
