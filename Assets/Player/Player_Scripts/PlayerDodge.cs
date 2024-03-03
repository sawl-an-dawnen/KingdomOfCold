using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerDodge : MonoBehaviour
{
    [Header("Dodge Settings")]
    [SerializeField] float dodgePower = 200f;
    [SerializeField] float dodgeDistance = 50f;
    [SerializeField] float dodgeCooldown = 1f;
    [SerializeField] float invisibleCounter = .5f;

    private bool isDodging;
    private bool canDodge = true;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private playerMovement movementScript;
    private CustomInputs input;
    private AudioSource dodgeAudio;

    private Animator animator;


    private void Awake()
    {
        input = new CustomInputs();
        rb = GetComponent<Rigidbody2D>();
        movementScript = GetComponent<playerMovement>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        dodgeAudio = transform.Find("DodgeSfx").GetComponent<AudioSource>();
    }
    public void OnEnable()
    {
        input.Enable();
        input.Player.Dodging.performed += Dodge;
        //Debug.Log("Player  Dodge Enabled");
    }

    public void OnDisable()
    {
        input.Disable();
        input.Player.Dodging.performed -= Dodge;
    }


    public void Dodge(InputAction.CallbackContext context)
    {
        //Debug.Log("Player pressed Dodge");
        //If (Dodge button is pressed) (Player is not dodging) (Player is moving)
        if (context.performed && !isDodging && movementScript.moveVector.magnitude > 0 && canDodge)
        {
            isDodging = true;
            StartCoroutine(PerformDodge());
            canDodge = false; // Disable dodge until cooldown is over
            StartCoroutine(DodgeCooldown()); // Start the dodge cooldown timer
            StartCoroutine(InvinsibleCounter());
            animator.StopPlayback();
            dodgeAudio.Play();
            if (movementScript.moveVector.x >= 0f)
            {
                animator.Play("dodge_right");
            }
            else
            {
                animator.Play("dodge_left");
            }
            animator.StopPlayback();
        }

    }

    private IEnumerator PerformDodge()
    {
        boxCollider.enabled = false;
        // Calculate dodge direction
        Vector2 dodgeDirection = movementScript.moveVector.normalized; // Dodge in the direction of movement
        //Debug.Log("Dodge Direction: " + dodgeDirection);

        // Disable player input during dodge
        input.Disable();
        //Debug.Log("Input Disabled");
        // Store current position
        Vector2 startPosition = rb.position;
        //Debug.Log("Start Position: " + startPosition);

        // Calculate dodge velocity
        Vector2 dodgeVelocity = dodgeDirection * dodgePower;
        //Debug.Log("Dodge Velocity: " + dodgeVelocity);

        // Calculate target position
        Vector2 targetPosition = startPosition + dodgeDirection * dodgeDistance;
        //Debug.Log("Target Position: " + targetPosition);

        // Move the player until reaching the target position
        while (Vector2.Distance(rb.position, startPosition) < dodgeDistance)
        {
            // Apply dodge velocity
            rb.velocity = dodgeVelocity;

            // Wait for the next frame
            yield return null;
        }

        // Reset velocity and enable player input
        rb.velocity = Vector2.zero; // Stop the player
        isDodging = false;
        input.Enable();
        //Debug.Log("Input Enabled");
    }

    private IEnumerator DodgeCooldown()
    {
        yield return new WaitForSeconds(dodgeCooldown);
        canDodge = true; // Enable dodge after cooldown is over
        boxCollider.enabled = true;
    }
    private IEnumerator InvinsibleCounter()
    {
        yield return new WaitForSeconds(invisibleCounter);
        boxCollider.enabled = true;
    }
}
