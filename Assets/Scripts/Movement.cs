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
    private int lPos = 1;
    private int condition = 0;
    private int jumpC;

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
        jumpC = int.Parse(line[row + 3].Split(':')[1]);
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
            condition = 0;
            jPos = 1;
            if (moveH > 0 && faceRight == false)
                flip();
            if (moveH < 0 && faceRight == true)
                flip(); 
        }
        else
        {
            rb.velocity = new Vector2(moveH * speed/2, rb.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.C) && isGround)
        {
            condition = 1;
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
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Stay/Stay"+(lPos).ToString());
            if (hg < 10)
                hg++;
            else if (hg == 10 && lPos < 3)
            {
                hg = 0;
                lPos++;
            }
            else if (hg == 10 && lPos == 3)
            {
                hg = 0;
                lPos -= 2;
            }

            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Stay/Stay" + (lPos).ToString());
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
