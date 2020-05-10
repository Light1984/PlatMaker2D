using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class CreationScript : MonoBehaviour
{
    public static int step = 30;
    public GameObject[] objects;
    

    ///Users/romabruks/Desktop/TestMap.txt
    string line;
    string[] lineS;


    void Start()
    {

        line = File.ReadLines("TestMap.txt").Skip(0).First();
       
        string[] size = line.Split();
        int row = int.Parse(size[0]);
        int col = int.Parse(size[1]);

        lineS = new string[row];
        for (int i = 0; i < row; ++i)
             lineS[i] = File.ReadLines("TestMap.txt").Skip(i+1).First();



        char[][] field = new char[row][];
        for (int i = 0; i < row; ++i)
        {
            field[i] = new char[col];
            field[i] = lineS[i].ToCharArray();

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
                    case '3':
                        obj = 2;
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

                }
                if (obj > 0)
                    Instantiate(objects[obj], new Vector2(j * step, (9 - i) * step), Quaternion.identity);
                else if (obj == 0)
                {
                    Instantiate(objects[obj], new Vector2(j * step, (9 - i) * step), Quaternion.identity);
                    Instantiate(objects[1], new Vector2(j * step, (9 - i) * step), Quaternion.identity);
                }
                obj = -1;

            }
        }

        Instantiate(objects[7], new Vector2(0, -step * 7), Quaternion.identity);


        



    }


   
  





}
  