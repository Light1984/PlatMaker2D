using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatScript : MonoBehaviour
{
    float dirX;
    public float speed = 30f;
    public bool right = true;

    private void Start()
    {
        dirX = transform.position.x;
    }

    void Update()
    {
        if (transform.position.x > dirX + CreationScript.step*3)
            right = false;

        else if (transform.position.x < dirX)
            right = true;


        if (right)
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y );

    }
}
