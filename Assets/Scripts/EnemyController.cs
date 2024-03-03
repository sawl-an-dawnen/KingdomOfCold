using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    public float speed;
    [HideInInspector]
    public Vector3 direction;

    [Range(0.0f, 100f)]
    public float speedPenalty = 10f;
    public AudioClip audioClip;
    private AudioSource playerAudio;
    private Transform playerTarget;
    private PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        playerAudio = playerManager.transform.Find("HitSfx").GetComponent<AudioSource>();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ENEMY COLLISION");
            playerManager.TakeDamage(); // Call TakeDamage method from PlayerManager
            playerManager.GetSlowed(speedPenalty); // Call GetSlowed method from PlayerManager
            playerAudio.PlayOneShot(audioClip);
        }
    }
}
