using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMovment : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(0, speed);
        }
        else 
        {
            rb.velocity = new Vector2(0, -speed);
        }

       
        if (Vector2.Distance(transform.position, currentPoint.position) < 10f && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 10f && currentPoint == pointA.transform)
        {

            currentPoint = pointB.transform;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(pointA.transform.position, 10f);
        Gizmos.DrawWireSphere(pointB.transform.position, 10f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);

    }
}
