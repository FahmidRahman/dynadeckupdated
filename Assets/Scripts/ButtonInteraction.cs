using System.Collections;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    // Reference to the TileManager
    public TileManager tileManager;

    private bool isPlayerInRange = false;

    private void Update()
    {
        // Check if the player is in range and presses "E"
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            tileManager.ResetAllTiles(); // Reset all tiles
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the trigger zone
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // Enable interaction
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exits the trigger zone
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // Disable interaction
        }
    }
}
