using System.Collections;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public Transform player;  // Reference to the player
    public float interactionDistance = 3.0f; // How close the player needs to be to open the door
    public float openSpeed = 2.0f; // Speed of the door opening
    private bool isOpen = false; // Track if the door is open
    private bool isPlayerInRange = false; // Check if player is close enough to the door
    private Quaternion originalRotation; // Store the original door rotation
    private Quaternion openRotation; // The target rotation when the door is open

    private Inventory playerInventory; // Reference to the player's inventory

    public string requiredKey = "Key"; // Define the required key name

    void Start()
    {
        originalRotation = transform.localRotation; // Store the original rotation of the door pivot
        openRotation = Quaternion.Euler(0, 90, 0) * originalRotation; // Define the open rotation, adjust for desired angle (90 degrees here)
        playerInventory = player.GetComponent<Inventory>(); // Get the player's inventory
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // Check if player is within range
        if (distanceToPlayer <= interactionDistance)
        {
            isPlayerInRange = true;

            // Check if player presses 'E' and has the key in inventory
            if (Input.GetKeyDown(KeyCode.E) && playerInventory.HasItem(requiredKey))
            {
                ToggleDoor();
            }
        }
        else
        {
            isPlayerInRange = false;
        }
    }

    void ToggleDoor()
    {
        if (!isOpen)
        {
            // Open the door by rotating it
            StartCoroutine(OpenDoor());
        }
        else
        {
            // Close the door (if you want it to be closeable)
            StartCoroutine(CloseDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        isOpen = true;

        // Rotate the door over time to simulate it opening
        while (Quaternion.Angle(transform.localRotation, openRotation) > 0.01f)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, openRotation, Time.deltaTime * openSpeed);
            yield return null;
        }
    }

    IEnumerator CloseDoor()
    {
        isOpen = false;

        // Rotate the door back to the original rotation to close it
        while (Quaternion.Angle(transform.localRotation, originalRotation) > 0.01f)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, originalRotation, Time.deltaTime * openSpeed);
            yield return null;
        }
    }

    // Optional: Visual indication that the player is in range of the door
    void OnGUI()
    {
        if (isPlayerInRange)
        {
            if (!isOpen)
            {
                if (playerInventory.HasItem(requiredKey))
                {
                    GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 25, 150, 50), "Press E to open the door");
                }
                else
                {
                    GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 25, 150, 50), "You need a key to open this door");
                }
            }
            else
            {
                GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 25, 150, 50), "Press E to close the door");
            }
        }
    }
}
