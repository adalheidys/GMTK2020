using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    //Made by chase stump. Rushed work is still worth crediting.
    public string weakness;
    public string ability;
    public int timerBase;
    public int timer;
    public float goalSensitivity;
    public NavMeshAgent nav;
    public Transform target;
    public Transform temptationTarget;
    public bool tempted;
    public bool directed;
    public RaycastHit hit;
    //public LockController lockcon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //Debug.DrawRay(transform.position, temptationTarget.transform.position - transform.position, Color.green);
        if (tempted)
        {
            if (temptationTarget != null)//make sure that it has a targed
            {
                Debug.DrawRay(transform.position, temptationTarget.transform.position - transform.position, Color.green);
                nav.SetDestination(temptationTarget.position);
                if (Physics.Raycast(transform.position, temptationTarget.transform.position - transform.position, out hit))
                {
                    if (!hit.collider.CompareTag(weakness)) //hit something else before the camera
                    {
                        tempted = false;
                    }
                }
            }
            else
            {
                tempted = false;//stop being tempted bc there is no target
            }
        }
        else if (directed)
        {
            //Debug.Log(Vector3.Distance(target.position, transform.position));
            nav.SetDestination(target.position);
            if (Vector3.Distance(target.position, transform.position) < goalSensitivity)
            {
                directed = false;
            }
        }
        else
        {
            if (timer == 0)
            {
                timer = timerBase;
                if (Random.value < .5)
                {
                    if (Random.value < .5)
                        nav.SetDestination(new Vector3(transform.position.x + 1, transform.position.y, transform.position.z));
                    else
                        nav.SetDestination(new Vector3(transform.position.x - 1, transform.position.y, transform.position.z));
                }
                else
                {
                    if (Random.value < .5)
                        nav.SetDestination(new Vector3(transform.position.x, transform.position.y, transform.position.z+1));
                    else
                        nav.SetDestination(new Vector3(transform.position.x, transform.position.y, transform.position.z-1));
                }                

            }
            else
            {
                timer--;
            }
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ability)//if what it collided with is what it can unlock
        {          
            LockController l = other.GetComponent<LockController>();
            if (l.activatable)
            {
                l.Interact(); directed = true;
                target = other.transform;
            }
        }
    }
}
