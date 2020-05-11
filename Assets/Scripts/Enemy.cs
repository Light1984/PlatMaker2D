using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float dirY;
    float dirX;
    public float speed = 30f;
    public bool up = true;

    public float timeBtwShots;
    public float startBtwShots;
    public GameObject projectile;

    string[] line;
    private int moveC;
    private int mPos = 1;
    private int hg = 0;
    private int ePos = 1;

    private string fire;
    private int fireC;
    private int efireC;
    private bool faceR = true;



    private void Start()
    {

        dirY = transform.position.y;
        dirX = transform.position.x;
        timeBtwShots = startBtwShots;

        line = File.ReadAllLines("TestMap.txt");
        string[] size = line[0].Split();
        int row = int.Parse(size[0]);


        if (gameObject.tag == "Enemy")
            moveC = int.Parse(line[row + 6].Split(':')[1]);
        else
        {
            fire = line[row + 7].Split(':')[1];
            fireC = int.Parse(fire.Split(',')[0]);
            efireC = int.Parse(fire.Split(',')[1]);
           
        }



    }
    private void Update()
    {
        if (gameObject.tag == "EnemyF")
        {
            if (GameObject.FindGameObjectWithTag("Player").transform.position.x < transform.position.x && faceR || GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x && !faceR)
            {
                faceR = !faceR;
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            }
       


            if (timeBtwShots <= 0)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBtwShots = startBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }

        }
    }


    private void FixedUpdate()
    {
        if (gameObject.tag == "Enemy")
        {
            if (transform.position.y > dirY + CreationScript.step)
                up = false;

            else if (transform.position.y < dirY - CreationScript.step)
                up = true;

            if (up)
                transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
            else
                transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);




            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Enemy1/Fly" + (mPos).ToString());
            if (hg < 5)
                hg++;
            else if (hg == 5 && mPos < moveC)
            {
                hg = 0;
                mPos++;
            }
            else if (hg == 5 && mPos == moveC)
            {
                hg = 0;
                mPos = 1;
            }
        }
        else
        {
            
            if (timeBtwShots <= 2)
            {
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Enemy2/Fire" + (mPos).ToString());
                if (hg < 5)
                    hg++;
                else if (hg == 5 && mPos < fireC)
                {
                    hg = 0;
                    mPos++;
                }
                else if (hg == 5 && mPos == fireC)
                {
                    hg = 0;
                    mPos = 1;
                }
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Enemy2/eFire" + (ePos).ToString());
                if (hg < 5)
                    hg++;
                else if (hg == 5 && ePos < efireC)
                {
                    hg = 0;
                    ePos++;
                }
                else if (hg == 5 && ePos == efireC)
                {
                    hg = 0;
                    ePos = 1;
                }
            }

        }

    }


}
