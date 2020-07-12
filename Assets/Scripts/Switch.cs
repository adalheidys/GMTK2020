using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Sprite activatedSprite;
    public SpriteRenderer spriteRenderer;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Rogue")||other.CompareTag("Bard")||other.CompareTag("Warrior"))
        spriteRenderer.sprite = activatedSprite;
        //send message to door
        //Door.HitSwitch();
        Debug.Log("hit inner Switch");
    }
}
