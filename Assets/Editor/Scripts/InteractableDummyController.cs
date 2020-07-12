using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDummyController : MonoBehaviour
{
    public bool activatable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        if (activatable)
        {
            Debug.Log("YouActivatedMe");
            activatable = false;
        }
        
    }
}
