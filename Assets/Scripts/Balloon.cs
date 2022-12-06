using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Touched by bee");
        if( collision.gameObject.tag == "Bee" )
        {
            Pop();
        }
    }

    void Pop()
    {
        GameManager.S.BalloonPopped(gameObject.name);
    }
}
