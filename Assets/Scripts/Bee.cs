using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    public Vector3 mousePos3D;
    public bool asleep = true; // If true, Bee will follow the player's mouse input. If false, Bee will remain stationary.
    // Start is called before the first frame update
    void Start()
    {
        transform.position = GameManager.S.BeeSpawnPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Convert the position of the mouse pointer to a point in the game.
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        // Once the mouse is close enough to the Bee, it will start following the mouse as it moves.
        // This ensures that the Bee won't be placed out of bounds when it spawns.
        if( Vector3.Distance(mousePos3D, transform.position) < 1.0f )
        {
            asleep = false;
        }

        // This if statement blocks the Bee's movement until the player has put the mouse close enough to it.
        if ( !asleep && GameManager.S.isTimerActive) { 
            // Blocks the Bee's movement if there is an obstacle between it and the mouse.
            if ( !Physics.Linecast(transform.position, mousePos3D) ) {
                transform.position = mousePos3D;
                transform.LookAt(mousePos3D);
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RedZone")
        {
            Debug.Log("hit redzone");
            transform.position = GameManager.S.startPos;
            asleep = true;
            GameManager.S.LoseLife();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the player touches a Red Zone or Bomb, reset it's position to spawn and lose a life.
        if (other.gameObject.tag == "RedZone" || other.gameObject.tag == "Bomb")
        {
            transform.position = GameManager.S.BeeSpawnPoint.transform.position;
            asleep = true;
            GameManager.S.LoseLife();
        }
    }
}
