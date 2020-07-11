using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float Restraint;
    public CharacterControllerBehavior characterController;
    
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterControllerBehavior>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 target = ray.direction;
        if ((player.position - gameObject.transform.position).magnitude > Restraint )
        {
            target = (player.position - gameObject.transform.position).normalized * Restraint;
        }
        characterController.target = target;
    }
}
