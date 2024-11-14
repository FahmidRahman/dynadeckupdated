using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody rb;
    Vector3 moveVectorInput;
    Vector3 moveDirection;
    Vector3 rotationDirection;
    [SerializeField] float speed = 10f;
    [SerializeField] Camera targetCamera;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float jumpForce = 5f; 
    [SerializeField] LayerMask groundLayer;

    bool isGrounded;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    public void AddMoveVectorInput(Vector3 moveVector) {
        moveVectorInput = moveVector;
    }

    private void Update() {
        HandleMovement();
        HandleRotation();
        HandleJump();
    }

    private void HandleMovement() {
        
        moveDirection = targetCamera.transform.forward * moveVectorInput.z;
        moveDirection += targetCamera.transform.right * moveVectorInput.x;
        moveDirection.y = 0f;
        moveDirection.Normalize();

        
        Vector3 moveVelocity = moveDirection * speed;
        moveVelocity.y = rb.velocity.y; 

        rb.velocity = moveVelocity;
    }

    private void HandleRotation() {
        if (moveDirection.magnitude > 0f) {
            rotationDirection = moveDirection;
        }

        if (rotationDirection != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(rotationDirection);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.rotation = rotation;
        }
    }

    private void HandleJump() {
        
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.5f, groundLayer);

        
        if (Input.GetButtonDown("Jump") && isGrounded) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
