using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownTimer : MonoBehaviour
{

    Transform timerChildTrans;
    public bool cooling=false;
    public float speed= 0.1f; 

    // Start is called before the first frame update
    void Start()
    {
        cooling=false;
        timerChildTrans = transform.GetChild(0).GetComponent<RectTransform>();
        timerChildTrans.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (cooling)
        {
            
            timerChildTrans.localPosition = Vector2.MoveTowards(timerChildTrans.localPosition, new Vector2(0,-60), speed);
            if ( Vector2.Distance(timerChildTrans.localPosition, new Vector2(0,-60)) < 0.01f )
            {
                cooling=false;
                timerChildTrans.gameObject.SetActive(false);
            }
        }
    }

    public void StartCoolDown()
    {
        
        timerChildTrans.localPosition =  Vector2.zero;
        timerChildTrans.gameObject.SetActive(true);
        cooling=true;

    }
}
