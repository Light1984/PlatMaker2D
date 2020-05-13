using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using System.Linq;


public class Movement : MonoBehaviour
{

    private float jumpForce;
    private float speed;
    private Rigidbody2D rb;
    private bool faceRight = true;
    private bool godeMode = false;
    private int timeGode = 0;


    private float groundRad = 0.02f;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public bool isGround = false;


    public int nuts = 0;
    private int nutsMax;
    public int lives;

    string folderPath;
    string[] filePaths;



    string line;
    private int hg = 0;
    private int jPos = 1;
    private int iPos = 1;
    private int rPos = 1;
    private int dPos = 1;
    private int condition = 0;
    private int jumpC;
    private int idleC;
    private int runC;
    private int damC;
    private string jumpSD;
    private int idealJ;
    private string runSD;
    private int idealR;

    private AudioClip jumpSound;
    private AudioClip colSound;
    private AudioClip defeatSound;

    private string attack;
    private int typeAttack;
    public GameObject projectile;

    private string jumpPath = "/Sounds/Jump.wav";
    private string hitPath = "/Sounds/Hit.wav";
    private string colPath = "/Sounds/Coin.wav";


    public string url;
    public AudioSource source;




    // Start is called before the first frame update
    IEnumerator Start()
    {
        source = GetComponent<AudioSource>();
        url = "file://" + Application.streamingAssetsPath + jumpPath;
        using (var www = new WWW(url))
        {
            yield return www;
            jumpSound = www.GetAudioClip();
        }

        url = "file://" + Application.streamingAssetsPath + hitPath;
        using (var www = new WWW(url))
        {
            yield return www;
            defeatSound = www.GetAudioClip();
        }

        url = "file://" + Application.streamingAssetsPath + colPath;
        using (var www = new WWW(url))
        {
            yield return www;
            colSound = www.GetAudioClip();
        }


        rb = GetComponent<Rigidbody2D>();

        line = File.ReadLines("TestMap.txt").Skip(0).First();

        string[] size = line.Split();
        int row = int.Parse(size[0]);

        lives = int.Parse(File.ReadLines("TestMap.txt").Skip(row + 1).First().Split(':')[1]);
        nutsMax = int.Parse(File.ReadLines("TestMap.txt").Skip(row + 2).First().Split(':')[1]);
        idleC = int.Parse(File.ReadLines("TestMap.txt").Skip(row + 3).First().Split(':')[1]);

        jumpSD = (File.ReadLines("TestMap.txt").Skip(row + 4).First().Split(':')[1]);
        jumpC = int.Parse(jumpSD.Split(',')[0]);
        idealJ = int.Parse(jumpSD.Split(',')[1]);


        runSD = (File.ReadLines("TestMap.txt").Skip(row + 5).First().Split(':')[1]);
        runC = int.Parse(runSD.Split(',')[0]);
        idealR = int.Parse(runSD.Split(',')[1]);



        damC = int.Parse(File.ReadLines("TestMap.txt").Skip(row + 6).First().Split(':')[1]);
        speed = float.Parse(File.ReadLines("TestMap.txt").Skip(row + 10).First().Split(':')[1]);
       jumpForce = float.Parse(File.ReadLines("TestMap.txt").Skip(row + 11).First().Split(':')[1]);



        attack = (File.ReadLines("TestMap.txt").Skip(row + 15).First().Split(':')[1]);
        typeAttack = int.Parse(attack.Split(',')[0]);
        


    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundRad, whatIsGround);
        //anim.SetBool("Ground", isGround);



        if (!godeMode)
        {
            float moveH = Input.GetAxis("Horizontal");

            if (isGround)
            {
                rb.velocity = new Vector2(moveH * speed, rb.velocity.y);
                jPos = 1;
               
                if (moveH != 0)
                    condition = 2;
                else
                    condition = 0;
            }
            else
            {
                condition = 1;
                rb.velocity = new Vector2(moveH * speed / 2, rb.velocity.y);
            }

            if (moveH > 0 && faceRight == false)
                flip();
            if (moveH < 0 && faceRight == true)
                flip();


            if (Input.GetKeyDown(KeyCode.C) && isGround)
            {
                rb.AddForce(new Vector2(0, jumpForce));
                GetComponent<AudioSource>().clip = jumpSound;
                GetComponent<AudioSource>().Play();
            }

            if (typeAttack == 2 && Input.GetKeyDown(KeyCode.X))
            {

                Instantiate(projectile, transform.position, transform.rotation);
            }



        }
        else
            condition = 3;

        if (lives == -1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);



    }


    private void FixedUpdate()
    {
        if (condition == 0)
        {
            ImageLoader("Stay/Stay", iPos);
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
            if (idealJ > 0)
            {
                ImageLoader("Jump/Jump", jPos);
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
                    jPos -= idealJ-1;
                }
            }
            else
            {
                ImageLoader("Jump/Jump", jPos);
                if (hg < 5)
                    hg++;
                else if (hg >= 5 && jPos < jumpC)
                {
                    hg = 0;
                    jPos++;
                }
                else if (hg >= 5 && jPos == jumpC)
                {
                    hg = 0;
                    jPos = 1;
                }
            }
            
        }
        else if (condition == 2)
        {

            if (idealR > 0)
            {
                ImageLoader("Run/Run", rPos);
                if (hg < 2)
                    hg++;
                else if (hg >= 2 && jPos < runC)
                {
                    hg = 0;
                    rPos++;
                }
                else if (hg >= 2 && jPos == runC)
                {
                    hg = 0;
                    rPos -= idealR - 1;
                }
            }
            else
            {
                ImageLoader("Run/Run", rPos);
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
        else if (condition == 3)
        {
            ImageLoader("Hit/Hit", 1);
            if (hg < 5)
                hg++;
            else if (hg >= 5 && dPos < damC)
            {
                hg = 0;
                rPos++;
            }
            else if (hg >= 5 && dPos == damC)
            {
                hg = 0;
                dPos = 1;
            }
        }

        if (!godeMode)
            timeGode = 0;
        else
        {
            timeGode += 1;
            if (timeGode == 60)
                godeMode = false;

        }




        } 



    void flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0f, 180f, 0f);
    }




    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Coin")
        {
           nuts += 1;
            GetComponent<AudioSource>().clip = colSound;
            GetComponent<AudioSource>().Play();
        }
        else if ((other.tag == "Fallzone" || ((other.tag == "Enemy" || other.tag == "EnemyF") && (condition != 1 || typeAttack == 2)) || other.tag == "Bullet") && !godeMode)
        {
            godeMode = true;
            lives--;
            if(faceRight)
            rb.AddForce(new Vector2(-9000, 7000));
            else
                rb.AddForce(new Vector2(9000, 7000));



        }
        else if (((other.tag == "Enemy" || other.tag == "EnemyF") && condition == 1 && typeAttack == 1))
        {
            rb.AddForce(new Vector2(0, 20000));
            GetComponent<AudioSource>().clip = defeatSound;
            GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
        }
        else if (other.tag == "Finish" && nuts >= nutsMax)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
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


    void ImageLoader(string path, int Pos)
    {
        //Create an array of file paths from which to choose
        folderPath = Application.streamingAssetsPath;  //Get path of folder
        filePaths = Directory.GetFiles(folderPath, path + (Pos).ToString() + ".png"); // Get all files of type .png in this folder

        //Converts desired path into byte array
        byte[] pngBytes = System.IO.File.ReadAllBytes(filePaths[0]);

        //Creates texture and loads byte array data to create image
        Texture2D tex = new Texture2D(1, 1);
        tex.LoadImage(pngBytes);

        //Creates a new Sprite based on the Texture2D
        Sprite fromTex = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

        //Assigns the UI sprite
        gameObject.GetComponent<SpriteRenderer>().sprite = fromTex;
    }


}
