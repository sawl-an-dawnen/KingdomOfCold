using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerDodge : MonoBehaviour
{
    public CustomInputs input = null;
    public Rigidbody2D rb = null;
    public playerMovement movementScript = null;

    [Header("Dodge Settings")]
    [SerializeField] float dodgePower = 200f;
    [SerializeField] float dodgeDistance = 50f;
    [SerializeField] float dodgeCooldown = 1f;
    private bool isDodging;
    private bool canDodge = true;


    private void Awake()
    {
        input = new CustomInputs();
        
        rb = GetComponent<Rigidbody2D>();
        movementScript = GetComponent<playerMovement>();
    }
    public void OnEnable()
    {
        input.Enable();
        input.Player.Dodging.performed += Dodge;
        Debug.Log("Player  Dodge Enabled");
    }

    public void OnDisable()
    {
        input.Disable();
        input.Player.Dodging.performed -= Dodge;
    }


    public void Dodge(InputAction.CallbackContext context)
    {
        Debug.Log("Player pressed Dodge");
        //If (Dodge button is pressed) (Player is not dodging) (Player is moving)
        if (context.performed && !isDodging && movementScript.moveVector.magnitude > 0 && canDodge)
        {
            isDodging = true;
            StartCoroutine(PerformDodge());
            canDodge = false; // Disable dodge until cooldown is over
            StartCoroutine(DodgeCooldown()); // Start the dodge cooldown timer
        }
    }

    private IEnumerator PerformDodge()
    {
        // Calculate dodge direction
        Vector2 dodgeDirection = movementScript.moveVector.normalized; // Dodge in the direction of movement
        Debug.Log("Dodge Direction: " + dodgeDirection);

        // Disable player input during dodge
        input.Disable();
        Debug.Log("Input Disabled");
        // Store current position
        Vector2 startPosition = rb.position;
        Debug.Log("Start Position: " + startPosition);

        // Calculate dodge velocity
        Vector2 dodgeVelocity = dodgeDirection * dodgePower;
        Debug.Log("Dodge Velocity: " + dodgeVelocity);

        // Calculate target position
        Vector2 targetPosition = startPosition + dodgeDirection * dodgeDistance;
        Debug.Log("Target Position: " + targetPosition);

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
        Debug.Log("Input Enabled");
    }

    private IEnumerator DodgeCooldown()
    {
        yield return new WaitForSeconds(dodgeCooldown);
        canDodge = true; // Enable dodge after cooldown is over
    }
}
