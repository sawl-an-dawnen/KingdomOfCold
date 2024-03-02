using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public enum Type { Damage, Time }

    public Type type;

    private Transform playerTarget;

    public float speed = 50f;
    public float bulletSlowTime;
    public int bulletDamage;
    public float slowAmount ;
    public float playerTimerIncrement;
    public playerManager PlayerManager;

    

    // Start is called before the first frame update
    void Start()
    {

        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 direction = (playerTarget.position - gameObject.transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
        PlayerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<playerManager>();

    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //if player is hit by a DamageBullet, decrement player health.
            if (type == Type.Damage)
            {
                PlayerManager.TakeDamage(bulletDamage); // Call TakeDamage method from PlayerManager
                PlayerManager.GetSlowed(bulletDamage); // Call GetSlowed method from PlayerManager
                Debug.Log("Player has been hit. Decrement Health by " + bulletDamage + ".");
                Debug.Log("Player has been hit. Slowed by by " + slowAmount + ".");
                Destroy(gameObject); // Destroy bullet gameObject
            }


            //if player is hit by a TimeBullet, increment player remaining time
            if (type == Type.Time)
            {
                //TODO: increment player's remaining time, slow movement speed = 0.5s, destroy gameObject
                PlayerManager.GetSlowed(bulletDamage); // Call GetSlowed method from PlayerManager
                Debug.Log("Player has been hit. Slowed by by " + slowAmount + ".");
                Debug.Log("Player has been hit. Increment Time by 5.0f.");
                Destroy(gameObject); // Destroy bullet gameObject
            }
        }
    }
    
}
