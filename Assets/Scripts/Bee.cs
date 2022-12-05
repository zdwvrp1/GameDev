using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    public Vector3 mousePos3D;
    public Vector3 lastValidPosition;
    public bool isTouchingAWall = false;
    public bool asleep = true;
    // Start is called before the first frame update
    void Start()
    {
        lastValidPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        if( Vector3.Distance(mousePos3D, transform.position) < 1.0f )
        {
            asleep = false;
        }

        if ( !asleep ) { 
            if ( !Physics.Linecast(transform.position, mousePos3D) ) {
                isTouchingAWall = true;
                transform.position = mousePos3D;
            } else
            {
                isTouchingAWall = false;
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if( collision.gameObject.tag == "RedZone" )
        {
            transform.position = GameManager.S.startPos;
            asleep = true;
            GameManager.S.LoseLife();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RedZone")
        {
            transform.position = GameManager.S.startPos;
            asleep = true;
            GameManager.S.LoseLife();
        } else if (other.tag == "Balloon")
        {
            GameManager.S.BalloonPopped(other.name);
        }
    }
}
