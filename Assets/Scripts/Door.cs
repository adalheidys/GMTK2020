using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int numLocks=3;
    public SpriteRenderer spriteRenderer;
    public Sprite twoLocks;
    public Sprite oneLock;
    public Sprite noLock;   
    public static Door currentDoor;
    private void Awake()
    {
        currentDoor = this;
    }

    public void looseLock()
    {
        numLocks--;
        if(numLocks == 2)
        {
            spriteRenderer.sprite = twoLocks;
        }
        else if(numLocks == 1)
        {
            spriteRenderer.sprite = oneLock;
        }
        else if(numLocks <= 0)
        {
            spriteRenderer.sprite = noLock;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (numLocks <= 0)
            {
                Debug.Log("nextScene");
                //nextScene();
            }
        }
    }

    public void setPos(Vector3 pos)
    {
        transform.position = pos;
    }
}
