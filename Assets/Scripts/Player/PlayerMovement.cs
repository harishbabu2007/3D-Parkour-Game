using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController controller;
    public float speed = 12, gravity = -19.6f, jumpHeight = 3f, sprintSpeed;
    private Vector3 velocity;
    public Transform feet;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.collisionFlags == CollisionFlags.None)
        {
            gravity = -19.6f;
        }

        isGrounded = Physics.CheckSphere(feet.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
		{
            velocity.y = -2f;
		}

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
		{
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
		}

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed += sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed -= sprintSpeed;
        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            transform.localScale = new Vector3(1, 0.5f, 1);
        } else if (Input.GetKeyUp(KeyCode.C))
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.CompareTag("WallJump"))
        {
            gravity = -1f;
            isGrounded = true;
        }
        
        else
        {
            gravity = -19.6f;
        }
    }
}
