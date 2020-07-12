using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyReferences : MonoBehaviour
{
    public static PartyReferences currentParty;
    public GameObject Rogue;
    public GameObject Bard;
    public GameObject Warrior;

    private void Awake()
    {
        currentParty = this;
    }
}
