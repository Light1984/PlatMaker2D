using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationScript : MonoBehaviour
{
    public GameObject[] objects;


    void Start()
    {
        Instantiate(objects[0], new Vector2(0,0), Quaternion.identity);
        Instantiate(objects[9], new Vector3(0, 0,-1), Quaternion.identity);
        Instantiate(objects[3], new Vector2(0,-50), Quaternion.identity);


    }
}
