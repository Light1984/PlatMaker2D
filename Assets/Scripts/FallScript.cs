using UnityEngine;

public class FallScript : MonoBehaviour
{


    public Transform respawn;

    private void Start()
    {
        respawn = GameObject.FindGameObjectWithTag("Respawn").transform;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
            other.transform.position = respawn.transform.position;
    }

}
