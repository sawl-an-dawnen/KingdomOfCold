using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Invincibility Settings")]
    [SerializeField] float invincibleCounter = 2.0f; // Length of Invincibility
    [SerializeField] float damageCooldown = 2.0f; // Duration of damageCooldown

    [HideInInspector]
    public Vector2 moveVector;

    private bool isDamaged;
    //private bool canHurt = true;
    private float defaultSpeed;
    private float timer = 0f;
    private Rigidbody2D rb;
    private playerMovement movement;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRender;

    public bool debugMode = false;
    public int playerHealth = 3;
    public int overDriveShieldBurts = 3;
    //public float speedPenalty = 30f;
    public float penaltyDuration = 2f;
    public float moveSpeed = 10.0f;



    void Start() 
    {
        movement = gameObject.GetComponent<playerMovement>();
        defaultSpeed = movement.moveSpeed;
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRender = GetComponent<SpriteRenderer>();
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

    /*public void Invincibility()
    {
        Debug.Log("Player is invincible.");
        // If (Player is not invincible)
        if (!isDamaged && canHurt)
        {
            isDamaged = true;
            StartCoroutine(InitiateInvincibility());
            canHurt = false; // Disable damage until cooldown is over
            StartCoroutine(DamageCooldown()); // Start the damage cooldown timer
            StartCoroutine(InvincibleCounter());
        }
    }
    */

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

     // Start Invincibility frames
        Debug.Log("Player is invincible.");
        //Invincibility();
        boxCollider.enabled = false;
        spriteRender.color = new Color(0.0f,0.0f, 0.0f, 0.5f);
        StartCoroutine(InvincibleCounter());


    /*
        // If (Player is not invincible)
        if (!isDamaged && canHurt)
        {
            isDamaged = true;
            StartCoroutine(InitiateInvincibility());
            canHurt = false; // Disable damage until cooldown is over
            StartCoroutine(DamageCooldown()); // Start the damage cooldown timer
            StartCoroutine(InvincibleCounter());
        }
    */

    }

    /*private IEnumerator InitiateInvincibility()
    {
        // Disable Player BoxCollider
        boxCollider.enabled = false;

        while (!canHurt)
        {
            yield return null;
        }
    }*/

    /*private IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(damageCooldown);
        canHurt = true; // Enable damage after cooldown is over
    }
    */

    private IEnumerator InvincibleCounter()
    {
        // invincibilityAudio.Play();
        yield return new WaitForSeconds(invincibleCounter);
        spriteRender.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        boxCollider.enabled = true;
        
    }

    public void GetSlowed(float speedPenalty)
    {
        Debug.Log("GetSlowed() Called");
        movement.moveSpeed = defaultSpeed * speedPenalty * .01f;
        timer = penaltyDuration;
    }

    void Die()
    {
        Debug.Log("Player died!");
        gameObject.GetComponent<SceneLoader>().LoadScene();
    }
}
