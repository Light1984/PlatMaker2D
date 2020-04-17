﻿using UnityEngine;

public class Movement : MonoBehaviour
{

    public float jumpForce = 3000f;
    public float speed = 20f;
    private Rigidbody2D rb;
    private bool faceRight = true;


    private float groundRad = 0.02f;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public bool isGround = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundRad, whatIsGround);
        //anim.SetBool("Ground", isGround);



        float moveH = Input.GetAxis("Horizontal");
       rb.velocity = new Vector2(moveH * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.C) && isGround)
            rb.AddForce(new Vector2(0, jumpForce));

        if (moveH > 0 && faceRight == false)
            flip();
       if (moveH < 0 && faceRight == true)
            flip();
    }

    void flip()
    {
        faceRight = !faceRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }




    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemyHead")
            rb.AddForce(new Vector2(0, jumpForce * 6/5));
    }

    }
