using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallerController : MonoBehaviour
{
    public bool CallRogue;
    public AIController Rogue;
    public bool CallBard;
    public AIController Bard;
    public bool CallWarrior;
    public AIController Warrior;
    public int lifetime;
    // Start is called before the first frame update
    void Start()
    {
        if (CallRogue)
        {
            Rogue = GameObject.FindWithTag("Rogue").GetComponent<AIController>();
            Rogue.target = this.transform;
            Rogue.directed = true;
        }
        if (CallBard)
        {
            Bard = GameObject.FindWithTag("Bard").GetComponent<AIController>();
            Bard.target = this.transform;
            Bard.directed = true;
        }
        if (CallWarrior)
        {
            Warrior = GameObject.FindWithTag("Warrior").GetComponent<AIController>();
            Warrior.target = this.transform;
            Warrior.directed = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (lifetime > 0)
        {
            lifetime--;
        }
        else
        {
            this.enabled = false;
            //Destroy(this.gameObject);
        }
    }
}
