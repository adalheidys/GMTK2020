using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell :MonoBehaviour
{
    string SpellName;
    int Mana;
    // Start is called before the first frame update
    public Spell()
    {     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    virtual public void Cast()
    {
        Debug.Log("SpellCast");
    }
}
