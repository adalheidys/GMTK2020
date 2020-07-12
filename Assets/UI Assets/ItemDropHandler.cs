using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler  
{

    RectTransform rectTransform;
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        CastSpell(droppedObject);
        
    }

    void CastSpell( GameObject spell )
    {
        SpellBehavior spellBehavior = spell.GetComponent<SpellBehavior>();
        if ( !spellBehavior.canBeCast )
            return;

        

        if (!RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition))
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000))
            {
                spellBehavior.Cast();
                GameObject prefab = spell.GetComponent<SpellBehavior>().prefab;
                Instantiate(prefab, hit.point, Quaternion.identity);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = transform.GetChild(0).GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown("1") )
        {
            GameObject spell = GameObject.Find("Spell 1");
            CastSpell(spell);
        }
        if ( Input.GetKeyDown("2") )
        {
            GameObject spell = GameObject.Find("Spell 2");
            CastSpell(spell);
        }
        if ( Input.GetKeyDown("3") )
        {
            GameObject spell = GameObject.Find("Spell 3");
            CastSpell(spell);
        }
        if ( Input.GetKeyDown("4") )
        {
            GameObject spell = GameObject.Find("Spell 4");
            CastSpell(spell);
        }
        if ( Input.GetKeyDown("5") )
        {
            GameObject spell = GameObject.Find("Spell 5");
            CastSpell(spell);
        }
        if ( Input.GetKeyDown("6") )
        {
            GameObject spell = GameObject.Find("Spell 6");
            CastSpell(spell);
        }
    }
}
