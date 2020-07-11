using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseBehavior : MonoBehaviour
{
    private CharacterControllerBehavior CharControl;
    // Start is called before the first frame update
    void Start()
    {
       CharControl =  GetComponent<CharacterControllerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        Vector3 target = ray.direction;
         CharControl.target = target;
    }
}
