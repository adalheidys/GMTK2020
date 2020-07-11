using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerTargetObject : MonoBehaviour
{
    CharacterControllerBehavior characterController;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterControllerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        characterController.target = transform.position;
    }
}
