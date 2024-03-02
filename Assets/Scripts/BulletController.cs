using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public enum Type { Damage, Time }

    public Type type;

    private Transform playerTarget;

    public float speed;
    public float bulletSlowTime;
    public int bulletDamage;
    public float playerTimerIncrement;

    // Start is called before the first frame update
    void Start()
    {

        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 direction = (playerTarget.position - gameObject.transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * speed;


    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //if player is hit by a DamageBullet, decrement player health.
            if (type == Type.Damage)
            {
                //TODO: deduct health from player, slow their movement speed <= 0.5s, destroy gameObject
                GameObject.FindGameObjectWithTag("Player");
                Debug.Log("Player has been hit. Decrement Health by 1.");

            }


            //if player is hit by a TimeBullet, increment player remaining time
            if (type == Type.Time)
            {
                //TODO: increment player's remaining time, slow movement speed = 0.5s, destroy gameObject
                GameObject.FindGameObjectWithTag("Player");
                Debug.Log("Player has been hit. Increment Time by 5.0f.");
            }
        }
    }
    
}
