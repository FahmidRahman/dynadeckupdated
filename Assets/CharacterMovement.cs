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


    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    public void AddMoveVectorInput(Vector3 moveVector) {
        moveVectorInput = moveVector;
    }

    private void Update() {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement() {
        moveDirection = targetCamera.transform.forward * moveVectorInput.z;
        moveDirection += targetCamera.transform.right * moveVectorInput.x;
        moveDirection.y = 0f;
        moveDirection.Normalize();


        Vector3 moveVelocity = moveDirection * speed;
        moveVelocity += Physics.gravity;

        rb.velocity = moveVelocity;

    }

    private void HandleRotation() {
        if(moveDirection.magnitude > 0f) {
            rotationDirection = moveDirection;
        }

        if(rotationDirection != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(rotationDirection);
            Quaternion rotation = Quaternion.Slerp(targetRotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.rotation = rotation;
        }
    }


    
}

