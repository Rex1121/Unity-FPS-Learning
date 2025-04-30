using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    public float speed = 12f;
    public float sprintSpeed = 18f;
    public float gravity = -19.62f; 
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 2.0f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;

    public Transform Camera;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        GroundCheck();
        Move();
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            // Reset velocity when grounded
            velocity.y = -2f; 
        }
    }

    void Move()
    {
        // Getting the inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = Camera.transform.right * x + Camera.transform.forward * z;

        // Check if the Shift key is pressed to sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * sprintSpeed * Time.deltaTime);
        }
        else
        {
            // Creating the moving vector
            controller.Move(move * speed * Time.deltaTime);
        }

        // Check if player can jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // Actually jumping
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity - falling down
        velocity.y += gravity * Time.deltaTime;

        // Exectuting the jump
        controller.Move(velocity * Time.deltaTime);
        
    }
}
    