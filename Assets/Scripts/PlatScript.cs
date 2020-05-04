using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatScript : MonoBehaviour
{
    private Vector2 startP;
    private Vector2 endP;
    private Vector2 begP;
    public float speed = 3f;
    public bool isBegin = true;
    private float progress;

    private void Start()
    {
        startP = transform.position;
        endP = new Vector2(transform.position.x + 100f,0);
        begP.x = transform.position.x - 200f;
    }

    void Update()
    {
        if (transform.position.x >= endP.x)
            isBegin = false;
        else if (transform.position.x <= startP.x)
            isBegin = true;


        if (isBegin)
            transform.Translate(endP * Time.deltaTime/2);
        else
            transform.Translate(begP * Time.deltaTime);


    }
}
