using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    [HideInInspector]
    public Vector2 moveVector;

    private CustomInputs input;
    private Rigidbody2D rb;

    private Animator animator;
    
    private void Awake()
    {
        input = new CustomInputs();
        //Debug.Log("input PM: " + input);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        rb.velocity = moveVector * moveSpeed;

        if (moveVector.magnitude > 0f)
        {
            if (Mathf.Abs(moveVector.x) > Mathf.Abs(moveVector.y))
            {
                if (moveVector.x > 0f)
                {
                    animator.SetBool("Right", true);
                    IsolateAnimation("Right");
                }
                else
                {
                    animator.SetBool("Left", true);
                    IsolateAnimation("Left");
                }
            }
            else
            {
                if (moveVector.y > 0f)
                {
                    animator.SetBool("Up", true);
                    IsolateAnimation("Up");
                }
                else
                {
                    animator.SetBool("Down", true);
                    IsolateAnimation("Down");
                }
            }
        }
        else 
        {
            animator.SetBool("Idle", true);
            IsolateAnimation("Idle");
        }
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
        //Debug.Log("Move Vector: " + moveVector);
    }

    public void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }

    
    public void OSButton(InputAction.CallbackContext context)
    {
        //Debug.Log("Player used OSB");

        // Find all objects tagged as "bullet"
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        // Destroy all bullets
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
    }

    private void IsolateAnimation(string exclude) 
    {
        if (exclude != "Right") animator.SetBool("Right", false);
        if (exclude != "Left") animator.SetBool("Left", false);
        if (exclude != "Up") animator.SetBool("Up", false);
        if (exclude != "Down") animator.SetBool("Down", false);
        if (exclude != "Idle") animator.SetBool("Idle", false);
    }

}
