using UnityEngine;

public class GenericDoorScript : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 3.0f;  // Distance for interaction range
    private bool isPlayerNearby = false;  // Track if the player is near the door

    private void Update()
    {
        // Check if the player is within range and presses E
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();  // Open the door when E is pressed
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detect if the player enters the trigger zone
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;  // Set flag to true when player is nearby
            Debug.Log("Player entered door trigger zone.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Detect if the player exits the trigger zone
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;  // Set flag to false when player leaves
            Debug.Log("Player exited door trigger zone.");
        }
    }

    private void OpenDoor()
    {
        // Optional: Play an animation or sound here for door opening (if desired)
        Debug.Log("The door has been opened!");
        Destroy(gameObject);  // Destroy the door to simulate opening (optional)
    }
}
