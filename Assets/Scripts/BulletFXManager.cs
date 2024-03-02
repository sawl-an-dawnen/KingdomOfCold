using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFXManager : MonoBehaviour
{

    public GameObject bulletTypeDamage;
    public GameObject bulletTypeTime;

    private bool isBulletDamage;
    private bool isBulletTime;

    public float bulletSlowTime;
    public int bulletDamage;
    public float playerTimerIncrement;

    // Start is called before the first frame update
    void Start()
    {

        isBulletDamage = false;
        isBulletTime = false;
        bulletSlowTime = 0.5f;
        playerTimerIncrement = 0.0f;
        bulletDamage = 1;

    }


    // Update is called once per frame
    void Update()
    {
        /*
         //if player is hit by a DamageBullet, decrement player health.
            if (isBulletDamage == true)
            {
                //TODO: deduct health from player and slow their movement speed for 0.5s

            }


            //if player is hit by a TimeBullet, increment player remaining time
            if (isBulletTime == true)
            {
                //TODO: increment player's remaining time and slow their movement speed for 0.5s
            }

        */
       

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
