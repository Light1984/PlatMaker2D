using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationScript : MonoBehaviour
{
    public int step = 30;
    public GameObject[] objects;


    void Start()
    {
        Instantiate(objects[0], new Vector2(0,step), Quaternion.identity);
        Instantiate(objects[9], new Vector3(0, 0,-1), Quaternion.identity);
        Instantiate(objects[2], new Vector2(0,-step), Quaternion.identity);
        Instantiate(objects[2], new Vector2(step, -step), Quaternion.identity);
        Instantiate(objects[2], new Vector2(-step, -step), Quaternion.identity);
        Instantiate(objects[2], new Vector2(step, -step), Quaternion.identity);
        Instantiate(objects[2], new Vector2(step*2, -step), Quaternion.identity);
        Instantiate(objects[2], new Vector2(step*3, -step), Quaternion.identity);
        Instantiate(objects[4], new Vector2(step*3, 0), Quaternion.identity);
        Instantiate(objects[6], new Vector2(step * 2, step/2), Quaternion.identity);
        Instantiate(objects[10], new Vector2(step * 4, 0), Quaternion.identity);

    }
}
