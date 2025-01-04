using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";  // Tag to identify the player
    [SerializeField] Transform platform;          // Reference to the platform

    private Vector3 lastPlatformPosition;         // Last position of the platform
    private Transform player;                     // Reference to the player
    private Vector3 platformVelocity;             // Platform velocity

    private void Start() {
        // Initialize the last platform position
        lastPlatformPosition = platform.position;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals(playerTag)) {
            player = other.transform;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag.Equals(playerTag)) {
            player = null;
        }
    }

    private void FixedUpdate() {
        // Calculate platform velocity
        Vector3 currentPlatformPosition = platform.position;
        platformVelocity = (currentPlatformPosition - lastPlatformPosition) / Time.fixedDeltaTime;

        // Update the last platform position
        lastPlatformPosition = currentPlatformPosition;

        // Synchronize the player's position with the platform in FixedUpdate
        if (player != null) {
            // Directly adjust the player's position based on the platform's velocity
            player.position += platformVelocity * Time.fixedDeltaTime;
        }
    }

    private void LateUpdate() {
        // Fine-tune player's position for smoother visuals
        if (player != null) {
            player.position += platformVelocity * (Time.deltaTime - Time.fixedDeltaTime);
        }
    }
}
