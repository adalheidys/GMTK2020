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
            if (Rogue)
            {
                Rogue.target = this.transform;
                Rogue.directed = true;
            }
        }
        if (CallBard)
        {
            Bard = GameObject.FindWithTag("Bard").GetComponent<AIController>();
            if (Bard)
            {
                Bard.target = this.transform;
                Bard.directed = true;
            }
        }
        if (CallWarrior)
        {
            Warrior = GameObject.FindWithTag("Warrior").GetComponent<AIController>();
            if (Warrior)
            {
                Warrior.target = this.transform;
                Warrior.directed = true;
            }
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
            if (Rogue)
            {
                Rogue.target = null;
                Rogue.directed = false;
            }
            if (Bard)
            {
                Bard.target = null;
                Bard.directed = false;
            }
            if (Warrior)
            {
                Warrior.target = null;
                Warrior.directed = false;
            }
            Destroy(this.gameObject);
        }
    }
}
