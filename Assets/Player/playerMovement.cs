using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    public CustomInputs input = null;
    public Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb = null;
    public float moveSpeed = 10f;


    
    private void Update()
    {
        rb.velocity = moveVector * moveSpeed;
    }

    private void Awake()
    {
        input = new CustomInputs();
        Debug.Log("input PM: " + input);
        rb = GetComponent<Rigidbody2D>();
    }
    public void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCancelled;
        input.Player.OSB.performed += OSButton;
        

    }
    public void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCancelled;
        input.Player.OSB.performed -= OSButton;
        



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
