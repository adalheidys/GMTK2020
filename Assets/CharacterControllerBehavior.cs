using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    public float Acceleration;
    public float CurrentVel;
    public float MaxVel;
    public bool SpeedCorrection;
    public Vector3 target;
    [HideInInspector]
    
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        { 
            CurrentVel = rb.velocity.magnitude;
            moveToTarget();
            VelocityCorrection();
        }
      
    }

    void VelocityCorrection()
    {
        float speed = Vector3.Magnitude(rb.velocity);  // test current object speed
        if (speed > MaxVel)
        {
            float brakeSpeed = speed - MaxVel;


            Vector3 normalisedVelocity = rb.velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;
            rb.AddForce(-brakeVelocity);
        }        
             
    }
    private void moveToTarget()
    {
       Vector3 dir = (target - transform.position).normalized;
        rb.velocity = dir * Acceleration * Time.deltaTime;
    }
    
}
