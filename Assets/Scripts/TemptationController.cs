using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemptationController : MonoBehaviour
{
    //Made by chase stump. Rushed work is still worth crediting.
    //modified by John cavatelli
    public string targetType;
    public float maxDistance;
    public AIController target;
    public RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        switch (targetType)
        {
            case "Rogue":
                target = PartyReferences.currentParty.Rogue.GetComponent<AIController>();
                break;
            case "Bard":
                target = PartyReferences.currentParty.Bard.GetComponent<AIController>();
                break;
            case "Warrior":
                target = PartyReferences.currentParty.Warrior.GetComponent<AIController>();
                break;
        }
        Debug.Log(target.name);
    }

    private void FixedUpdate()
    {
       // Debug.DrawRay(transform.position, target.transform.position - transform.position, Color.green);
        if (Physics.Raycast(transform.position, target.transform.position - transform.position, out hit, maxDistance))
        {
            if (hit.collider.CompareTag(targetType)) //hit something else before the camera
            {                
                Debug.Log("hit player");
                target.temptationTarget = transform;
                target.tempted = true;
            }
            if(hit.distance < 0.1f)//if it gets too close, destroy the gameObject so it doensn't get hung up
            {
                Destroy(this.gameObject);
            }
        }
    }
}
