using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Animate : MonoBehaviour
{
    private bool up = false;

    [SerializeField]
    private float _jumpFreq = 3f;

    [SerializeField]
    private float _jumpHeight = 3f;

    private float _modifier = 0f;
    private SpriteRenderer _sprite = null;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _modifier = _jumpFreq;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + (_modifier * Time.deltaTime));

        //Debug.Log(transform.localPosition.z);

        if (transform.localPosition.z > _jumpHeight)
        {
            _modifier = -_jumpFreq;
        }

        if (transform.localPosition.z < 0f)
        {
            _modifier = _jumpFreq;
        }
    }
}
