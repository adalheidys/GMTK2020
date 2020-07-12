using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    Rigidbody body;
    public float speed;
    void Start()
    {
        body = transform.GetComponentInChildren<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        float horz = 0;
        float vert = 0;
        
        if ( (Input.GetKey("a") || Input.GetKey("left"))  )
        {
            horz = (-1) * speed * Time.deltaTime;
        }
        else if ( (Input.GetKey("d") || Input.GetKey("right"))  )
        {
            horz = (1) * speed * Time.deltaTime;
        }
        
        if ( (Input.GetKey("w") || Input.GetKey("up")) )
        {
            vert = (1) * speed * Time.deltaTime;
        }
        else if ( (Input.GetKey("s") || Input.GetKey("down"))  )
        {
            vert = (-1) * speed * Time.deltaTime;
        }

        body.velocity = new Vector3(horz, 0,vert) * speed * Time.deltaTime;
        
    }


   
}
