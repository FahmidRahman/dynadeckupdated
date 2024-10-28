using UnityEngine;

public class Lever : MonoBehaviour
{
    public LeverControlledDoor controlledDoor; // Reference to the door that will be opened
    public float interactionDistance = 3.0f; // Distance within which the player can interact

    private bool isPlayerInRange = false; // Track if the player is in range

    private void Update()
    {
        // Check if the player is within interaction distance
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            isPlayerInRange = distanceToPlayer <= interactionDistance;

            // Open or close the door when player presses 'E' while in range
            if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E pressed near lever");
                controlledDoor.ToggleDoor();
            }
        }
    }
}
