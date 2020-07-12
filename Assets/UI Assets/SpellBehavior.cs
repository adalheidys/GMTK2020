using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBehavior : MonoBehaviour
{

    public GameObject prefab;
    CoolDownTimer coolDownTimer;

    public bool canBeCast=true;
    // Start is called before the first frame update
    void Start()
    {
        coolDownTimer = transform.parent.GetComponentInChildren<CoolDownTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDownTimer.cooling)
            canBeCast=false;
        else 
            canBeCast=true;   
    }

    public void Cast()
    {
        coolDownTimer.StartCoolDown();
    }
    
}
