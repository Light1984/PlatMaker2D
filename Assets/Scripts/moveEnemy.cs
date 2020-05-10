using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveEnemy : MonoBehaviour
{
    float dirY;
    public float speed = 30f;
    public bool up = true;

    private void Start()
    {
        dirY = transform.position.y;
    }

    void Update()
    {
        if (transform.position.y > dirY+ CreationScript.step)
            up = false;

        else if (transform.position.y < dirY - CreationScript.step)
            up = true;


        if (up)
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        else
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);






    }   
    
}
