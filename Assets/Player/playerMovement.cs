using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    private CustomInputs input = null;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb = null;
    public float moveSpeed = 10f;


    [Header("Dodge Settings")]
    [SerializeField] float dodgePower = 200f;
    [SerializeField] float dodgeDistance = 50f;
    [SerializeField] float dodgeCooldown = 1f;
    private bool isDodging;
    private bool canDodge = true;
    private void Update()
    {
        rb.velocity = moveVector * moveSpeed;
    }

    private void Awake()
    {
        input = new CustomInputs();
        rb = GetComponent<Rigidbody2D>();
    }
    public void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCancelled;
        input.Player.OSB.performed += OSButton;
        input.Player.Dodging.performed += Dodge;

    }
    public void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCancelled;
        input.Player.OSB.performed -= OSButton;
        input.Player.Dodging.performed -= Dodge;



    }

    public void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
        Debug.Log("Move Vector: " + moveVector);
    }

    public void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;

    }

    public void Dodge(InputAction.CallbackContext context)
    {
        //If (Dodge button is pressed) (Player is not dodging) (Player is moving)
        if (context.performed && !isDodging && moveVector.magnitude > 0 && canDodge)
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
        Vector2 dodgeDirection = moveVector.normalized; // Dodge in the direction of movement
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

    public void OSButton(InputAction.CallbackContext context)
    {
        Debug.Log("Player used OSB");

        // Find all objects tagged as "bullet"
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("bullet");

        // Destroy all bullets
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
    }
}
