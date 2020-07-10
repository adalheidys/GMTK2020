using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBehavior : MonoBehaviour
{
    public float speed;
    public float Accel;
    bool isMoving;
    Vector3 Target;
    public bool AccelerationBody;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(AccelerationBody)
        {
            AcceleratingBody();
        }
        
       if(Target != null)
       {
            MoveToTarget(Target);
       }

    }
    private void MoveToTarget(Vector3 TargetPos)
    {
        var step = speed * Time.deltaTime;

        Vector3.MoveTowards(transform.position, TargetPos, step);
    }
   private void AcceleratingBody()
   {
        speed = speed + Accel * Time.deltaTime;
   }

}
