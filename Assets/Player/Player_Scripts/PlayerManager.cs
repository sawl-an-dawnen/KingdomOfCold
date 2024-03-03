using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    [HideInInspector]
    public Vector2 moveVector;

    private float defaultSpeed;
    private float timer = 0f;
    private playerMovement movement;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRender;

    public bool debugMode = false;
    public int playerHealth = 3;
    public int overDriveShieldBurts = 3;
    public GameObject life01, life02, life03;
    public GameObject osb01, osb02, osb03;
    public float penaltyDuration = 2f;
    public float invincibleCounter = 2.0f;
    [Range(0.0f, 100f)]
    public float fadeIntensity = 50f;
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

        if (playerHealth <= 2) {
            Destroy(life03);
        }
        if (playerHealth <= 1)
        {
            Destroy(life02);
        }
        if (overDriveShieldBurts <= 2)
        {
            Destroy(osb03);
        }
        if (overDriveShieldBurts <= 1)
        {
            Destroy(osb02);
        }
        if (overDriveShieldBurts <= 0)
        {
            Destroy(osb01);
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
        boxCollider.enabled = false;
        spriteRender.color = new Color(1.0f, 1.0f, 1.0f, fadeIntensity * 0.01f);
        StartCoroutine(InvincibleCounter());
    }

    private IEnumerator InvincibleCounter()
    {
        //invincibilityAudio.Play();
        yield return new WaitForSeconds(invincibleCounter);
        spriteRender.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        boxCollider.enabled = true;
        
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
