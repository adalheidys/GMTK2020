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

        }
        else if(numLocks == 1)
        {

        }
        else if(numLocks <= 0)
        {

        }
    }
}
