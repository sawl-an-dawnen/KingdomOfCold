using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    private CustomInputs input = null;
    private Vector2 moveVector = Vector2.zero;
    private InputAction osbFire;
    private InputAction dodge;
    private Rigidbody2D rb = null;
    public float moveSpeed = 10f;


    [Header("Dodge Settings")]
    [SerializeField] float dodgePower = 200f;
    [SerializeField] float dodgeDuration = 1f;
    [SerializeField] float dodgeCooldown = 1f;
    bool isDodging;

    private void Awake()
    {
        input = new CustomInputs();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCancelled;
        
        osbFire = input.Player.OSB;
        osbFire.Enable();
        osbFire.performed += OSButton;

        dodge = input.Player.Dodging;
        dodge.Enable();
        dodge.performed += DodgePerformed;



        

    }
    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCancelled;

        osbFire.Disable();
        dodge.Disable();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVector*moveSpeed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();

    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;

    }

    private void DodgePerformed(InputAction.CallbackContext value)
    {
        if (value.performed)
        {

            isDodging = true;
            Debug.Log("Player used dodge");
            Vector2 dodgeDirection = moveVector.normalized;
            rb.velocity = dodgeDirection * dodgePower;

           

            isDodging = false;
        }
    }

    IEnumerator DodgeCoroutine()
    {
        isDodging = true;
        Debug.Log("Player used dodge");
        Vector2 dodgeDirection = moveVector.normalized; 
        rb.velocity = dodgeDirection * dodgePower;

        yield return new WaitForSeconds(dodgeDuration);

        isDodging = false;
    }

    private void OSButton(InputAction.CallbackContext value)
    {
        Debug.Log("Player used OSB");
    }
}
