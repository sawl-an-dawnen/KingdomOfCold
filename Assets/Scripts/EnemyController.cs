using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    public float speed;
    [HideInInspector]
    public Vector3 direction;

    private Transform playerTarget;

    // Start is called before the first frame update
    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        direction = (playerTarget.position - gameObject.transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * speed;

        /*pawnMovement = Random.Range(0, 2);

        if(pawnMovement == 0 )
        {
            Vector3 direction = (playerTarget.position - gameObject.transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
        if(pawnMovement == 1 )
        {
            Vector3 direction = (playerTarget.position - gameObject.transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
        if (pawnMovement == 2 )
        {
            Vector3 direction = (playerTarget.position - gameObject.transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
        */
        
    }

    //oncollide do damage
}
