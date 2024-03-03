using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public enum Type { Damage, Time }

    public Type type;

    public float bulletLife = 1f;
    public float rotation = 0f;
    public float speed = 1f;

    public float timePenalty = 5f;
    [Range(0.0f, 100f)]
    public float speedPenalty = 30f;

    private Vector2 spawnPoint;
    private float timer = 0f;

    private PlayerManager playerManager;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > bulletLife) Destroy(this.gameObject);
        timer += Time.deltaTime;
        transform.position = Movement(timer);
        
        
    }

    private Vector2 Movement(float timer)
    {
        float x = timer * speed * transform.right.x;
        float y = timer * speed * transform.right.y;
        return new Vector2(x+spawnPoint.x, y+spawnPoint.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collided with player");
            //if player is hit by a DamageBullet, decrement player health.
            if (type == Type.Damage)
            {
                playerManager.TakeDamage(); // Call TakeDamage method from PlayerManager
                playerManager.GetSlowed(speedPenalty); // Call GetSlowed method from PlayerManager
                Debug.Log("RED BULLET");
                Destroy(gameObject); // Destroy bullet gameObject
            }

            //if player is hit by a TimeBullet, increment player remaining time
            if (type == Type.Time)
            {
                gameManager.AddTime(timePenalty);
                //TODO: increment player's remaining time, slow movement speed = 0.5s, destroy gameObject
                playerManager.GetSlowed(speedPenalty); // Call GetSlowed method from PlayerManager
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
