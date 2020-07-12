using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{



    void Start()
    {
        //spellPanel = transform.parent.parent.GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData mousePointerData)
    {
        if (!GetComponentInChildren<SpellBehavior>().canBeCast)
        {
            return;
        }
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData mousePointerData)
    {
        transform.localPosition = Vector3.zero;
    }

    
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
