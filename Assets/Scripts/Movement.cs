using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

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


    private int nuts = 0;
    private int nutsMax;
    private int lives;

    string[] line;


    public AudioClip jumpSound;
    public AudioClip colSound;
    public AudioClip defeatSound;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        line = File.ReadAllLines("TestMap.txt");

      
        string[] size = line[0].Split();
        int row = int.Parse(size[0]);
        lives = int.Parse(line[row + 1].Split(':')[1]);
        nutsMax = int.Parse(line[row + 2].Split(':')[1]);
        print(lives);


    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundRad, whatIsGround);
        //anim.SetBool("Ground", isGround);



        float moveH = Input.GetAxis("Horizontal");

        if (isGround)
        {
            rb.velocity = new Vector2(moveH * speed, rb.velocity.y);

           /* if (moveH > 0 && faceRight == false)
                flip();
            if (moveH < 0 && faceRight == true)
                flip(); */
        }
        else
        {
            rb.velocity = new Vector2(moveH * speed/2, rb.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.C) && isGround)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            GetComponent<AudioSource>().clip = jumpSound;
            GetComponent<AudioSource>().Play();
        }


        if (lives == -1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);


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



        if (other.tag == "Coin")
        {
           nuts += 1;
            GetComponent<AudioSource>().clip = colSound;
            GetComponent<AudioSource>().Play();
        }
        else if (other.tag == "Fallzone" || other.tag == "Enemy" || other.tag == "Bullet")
        {

            lives--;
            rb.AddForce(new Vector2(-1000, 700));


        }
        else if (other.tag == "headEnemy")
        {
            rb.AddForce(new Vector2(0, 15000));
            GetComponent<AudioSource>().clip = defeatSound;
            GetComponent<AudioSource>().Play();
        }
        else if (other.tag == "Finish" && nuts == nutsMax)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

   

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "flyPlat")
        {
            this.transform.parent = col.transform;
        }


    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "flyPlat")
        {
            this.transform.parent = null;
           
        }


    }


  




}
