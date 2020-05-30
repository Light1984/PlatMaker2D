using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCol : MonoBehaviour
{
    public Movement moveP;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (moveP.hel == 0)
        {
            moveP.enabled = false;
            FindObjectOfType<PauseMenu>().EndGame();

        }
    }
}
