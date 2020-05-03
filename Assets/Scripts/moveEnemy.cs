using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveEnemy : MonoBehaviour
{
    private Vector2 startP;
    private Vector2 endP;
    private Vector2 begP;
    public float speed = 3f;
    public bool up = true;
    private float progress;

    private void Start()
    {
        startP = transform.position;
        endP = new Vector2(0,transform.position.y +50f);
        begP.y = transform.position.y -50f;
    }

    void Update()
    {
        if (transform.position.y >= endP.y)
            up = false;
        else if (transform.position.y <= startP.y)
            up = true;


        if (up)
            transform.Translate(endP * Time.deltaTime);
        else
            transform.Translate(begP * Time.deltaTime);
      



       

    }   
    
}
