using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;



public class Movement : MonoBehaviour
{

    public float jumpForce = 3000f;
    public float speed = 20f;
    private Rigidbody2D rb;
    private bool faceRight = true;
    //GameObject camera;


    private float groundRad = 0.02f;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public bool isGround = false;


    private int nuts = 0;
    private int nutsMax;
    private int lives;

    string[] line;
    private int hg = 0;
    private int jPos = 1;
    private int iPos = 1;
    private int rPos = 1;
    private int condition = 0;
    private int jumpC;
    private int idleC;
    private int runC;

    public AudioClip jumpSound;
    public AudioClip colSound;
    public AudioClip defeatSound;
   // private Sprite userSprite;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        line = File.ReadAllLines("TestMap.txt");
      
        string[] size = line[0].Split();
        int row = int.Parse(size[0]);
        lives = int.Parse(line[row + 1].Split(':')[1]);
        nutsMax = int.Parse(line[row + 2].Split(':')[1]);
        idleC = int.Parse(line[row + 3].Split(':')[1]);
        jumpC = int.Parse(line[row + 4].Split(':')[1]);
        runC = int.Parse(line[row + 5].Split(':')[1]);

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
            jPos = 1;
            if (moveH > 0 && faceRight == false)
                flip();
            if (moveH < 0 && faceRight == true)
                flip();
            if (moveH != 0)
                condition = 2;
            else
                condition = 0;
        }
        else
        {
            condition = 1;
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


    private void FixedUpdate()
    {
        if (condition == 0)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Stay/Stay" + (iPos).ToString());
            if (hg < 5)
                hg++;
            else if (hg == 5 && iPos < idleC)
            {
                hg = 0;
                iPos++;
            }
            else if (hg == 5 && iPos == idleC)
            {
                hg = 0;
                iPos = 1;
            }

        }
        else if (condition == 1)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Jump/Jump" + (jPos).ToString());
            if (hg < 2)
                hg++;
            else if (hg >= 2 && jPos < jumpC)
            {
                hg = 0;
                jPos++;
            }
            else if (hg >= 2 && jPos == jumpC)
            {
                hg = 0;
                jPos -= 1;
            }

        }
        else if (condition == 2)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Run/Run" + (rPos).ToString());
            if (hg < 5)
                hg++;
            else if (hg >= 5 && rPos < runC)
            {
                hg = 0;
                rPos++;
            }
            else if (hg >= 5 && rPos == runC)
            {
                hg = 0;
                rPos = 1;
            }

        }


        } 



    void flip()
    {
        faceRight = !faceRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }




    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Coin")
        {
           nuts += 1;
            GetComponent<AudioSource>().clip = colSound;
            GetComponent<AudioSource>().Play();
        }
        else if (other.tag == "Fallzone" || ((other.tag == "Enemy" || other.tag == "EnemyF") && condition != 1) || other.tag == "Bullet")
        {

            lives--;
            if(faceRight)
            rb.AddForce(new Vector2(-1000000, 70000));
            else
                rb.AddForce(new Vector2(1000000, 7000));



        }
        else if (((other.tag == "Enemy" || other.tag == "EnemyF") && condition == 1) || other.tag == "headEnemy")
        {
            rb.AddForce(new Vector2(0, 15000));
            GetComponent<AudioSource>().clip = defeatSound;
            GetComponent<AudioSource>().Play();
            rb.AddForce(new Vector2(0, jumpForce));
            if ((other.tag == "Enemy" || other.tag == "EnemyF"))
                Destroy(other.gameObject);
        }
        else if (other.tag == "Finish" && nuts >= nutsMax)
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
