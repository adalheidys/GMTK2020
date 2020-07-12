using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockController : MonoBehaviour
{
    public bool activatable = true;
    bool locked;
    public void Interact()
    {
        if (activatable)
        {
            for(int i = 0; i < transform.childCount-1; i++)
            {
                if (transform.GetChild(i).CompareTag("LockObject"))
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
            }
            activatable = false;
        }

    }
}
