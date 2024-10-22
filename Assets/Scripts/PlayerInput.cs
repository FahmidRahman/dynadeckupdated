using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    CharacterMovement characterMovement;
    Vector3 moveVector;

    private void Awake() {
        characterMovement = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if character movement is enabled
        if (characterMovement.enabled) {
            moveVector.x = Input.GetAxisRaw("Horizontal");
            moveVector.z = Input.GetAxisRaw("Vertical");

            characterMovement.AddMoveVectorInput(moveVector);
        }
        else {
            moveVector = Vector3.zero; // Stop input when not allowed to move
        }
    }
}
