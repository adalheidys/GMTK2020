using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickThrowSpell : Spell
{
    public GameObject Brick;
    CharacterControllerBehavior brickController;
    Rigidbody Brickrb;
    // Start is called before the first frame update
    void Start()
    {
        brickController = Brick.GetComponent<CharacterControllerBehavior>();
        Brickrb = Brick.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Cast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 target = ray.direction;
       
        Brick.transform.position = transform.position;
        
        brickController.target =  Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        
    }
}
