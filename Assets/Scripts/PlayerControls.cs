using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    Rigidbody body;
    Animator animator;
        
    void Start()
    {
        body = transform.GetComponentInChildren<Rigidbody>();
        animator = transform.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float horz = 0;
        float vert = 0;
        float speed = 100;
        animator.SetBool("inMotion", false);
        
        if ( (Input.GetKey("a") || Input.GetKey("left"))  )
        {
            horz = (-1) * speed * Time.fixedDeltaTime;
            animator.SetBool("inMotion", true);
        }
        else if ( (Input.GetKey("d") || Input.GetKey("right"))  )
        {
            horz = (1) * speed * Time.fixedDeltaTime;
            animator.SetBool("inMotion", true);
        }
        
        if ( (Input.GetKey("w") || Input.GetKey("up")) )
        {
            vert = (1) * speed * Time.fixedDeltaTime;
            animator.SetBool("inMotion", true);
        }
        else if ( (Input.GetKey("s") || Input.GetKey("down"))  )
        {
            vert = (-1) * speed * Time.fixedDeltaTime;
            animator.SetBool("inMotion", true);
        }

        body.velocity = new Vector3(horz, 0,vert) * speed * Time.fixedDeltaTime;
        
    }


   
}
