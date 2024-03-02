using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public enum Type { Damage, Time }

    public Type type;

    private Transform playerTarget;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 direction = (playerTarget.position - gameObject.transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //if player is hit by a DamageBullet, decrement player health.
            if (isBulletDamage == true)
            {
                //TODO: deduct health from player, slow their movement speed = 0.5s, destroy gameObject

            }


            //if player is hit by a TimeBullet, increment player remaining time
            if (isBulletTime == true)
            {
                //TODO: increment player's remaining time, slow movement speed = 0.5s, destroy gameObject
            }
        }
    }
}
