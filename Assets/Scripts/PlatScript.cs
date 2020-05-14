using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatScript : MonoBehaviour
{
    float dirX;
    public float speed = 30f;
    public bool right = true;

    private float groundRad = 0.5f;
    public LayerMask whatIsGround;
    public Transform CheckL;
    public Transform CheckR;
    

    private void Start()
    {
        dirX = transform.position.x;
    }

    void Update()
    {

       

        if (right)
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);



    }


    private void FixedUpdate()
    {

        if (Physics2D.OverlapCircle(CheckL.position, groundRad, whatIsGround) || Physics2D.OverlapCircle(CheckR.position, groundRad, whatIsGround))
            right = !right;
    }
}
