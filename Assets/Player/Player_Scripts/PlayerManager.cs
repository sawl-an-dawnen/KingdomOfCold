using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    [HideInInspector]
    public Vector2 moveVector;

    private Rigidbody2D rb;

    public bool debugMode = false;
    public int playerHealth = 3;
    public int overDriveShieldBurts = 3;

    //public float speedPenalty = 30f;
    public float penaltyDuration = 2f;

    private float defaultSpeed;
    private playerMovement movement;
    private float timer = 0f;

    void Start() 
    {
        movement = gameObject.GetComponent<playerMovement>();
        defaultSpeed = movement.moveSpeed;
    }

    void Update() 
    {
        if (timer >= 0f)
        {
            timer -= Time.deltaTime;
        }
        else 
        { 
            movement.moveSpeed = defaultSpeed; 
        }
    }

    public void TakeDamage()
    {
        
        playerHealth -= 1;
       
        if (playerHealth <= 0)
        {
            if (!debugMode)
            {
                Die();
            }
        }
    }

    public void GetSlowed(float speedPenalty)
    {
        Debug.Log("GetSlowed() Called");
        movement.moveSpeed = defaultSpeed * (1-(speedPenalty * .01f));
        timer = penaltyDuration;
    }

    void Die()
    {
        Debug.Log("Player died!");
        gameObject.GetComponent<SceneLoader>().LoadScene();
    }
}
