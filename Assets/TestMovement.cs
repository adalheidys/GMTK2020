using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    public int speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical")*speed*Time.deltaTime));
    }
}
