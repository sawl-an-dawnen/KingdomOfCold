using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public enum Type { Damage, Time }

    public Type type;

    public float speed = 50f;
    public float timePenalty = 5f;
    public AudioClip audioClip;

    private PlayerManager playerManager;
    private AudioSource playerAudio;
    private Transform playerTarget;

    // Start is called before the first frame update
    void Start()
    {

        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 direction = (playerTarget.position - gameObject.transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        playerAudio = GameObject.FindGameObjectWithTag("Player").transform.Find("HitSfx").GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collided with player");
            playerAudio.PlayOneShot(audioClip);
            //if player is hit by a DamageBullet, decrement player health.
            if (type == Type.Damage)
            {
                playerManager.TakeDamage(); // Call TakeDamage method from PlayerManager
                playerManager.GetSlowed(30f); // Call GetSlowed method from PlayerManager
                Debug.Log("RED BULLET");
                Destroy(gameObject); // Destroy bullet gameObject
            }

            //if player is hit by a TimeBullet, increment player remaining time
            if (type == Type.Time)
            {
                //TODO: increment player's remaining time, slow movement speed = 0.5s, destroy gameObject
                playerManager.GetSlowed(20f); // Call GetSlowed method from PlayerManager
                Debug.Log("BLUE BULLET");
                Destroy(gameObject); // Destroy bullet gameObject
            }
        }
        if (other.CompareTag("Wall")) 
        {
            Destroy(gameObject); // Destroy bullet gameObject
        }
    }
    
}
