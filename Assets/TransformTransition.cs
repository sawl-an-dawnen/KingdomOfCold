using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TransformTransition : MonoBehaviour
{
    public GameObject position1;
    public GameObject position2;

    public float speed;
    [HideInInspector]
    public Vector3 direction;
    public bool state = true;

    // Update is called once per frame
    void Update()
    {
        if (state)
        {
            if (gameObject.transform.position != position1.transform.position)
            {
                direction = (position1.transform.position - gameObject.transform.position).normalized;
                GetComponent<Rigidbody2D>().velocity = direction * speed;
            }
        }
        else 
        {
            if (gameObject.transform.position != position2.transform.position)
            {
                direction = (position2.transform.position - gameObject.transform.position).normalized;
                GetComponent<Rigidbody2D>().velocity = direction * speed;
            }
        }

    }
}
