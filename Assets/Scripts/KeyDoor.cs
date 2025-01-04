using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private NPCInteraction npcInteraction; // Reference to NPCInteraction
    [SerializeField] private GameObject wallToDisappear; // Assign the wall to disappear in the Inspector
    private bool isPlayerNearby = false; // Tracks if the player is near the door

    private void Update()
    {
        // Check if the player is nearby and presses E
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed near door.");

            if (npcInteraction != null && npcInteraction.HasKey())
            {
                OpenDoor();
            }
            else
            {
                Debug.Log("You need the key to open this door!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detect if the player enters the trigger zone
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Player entered door trigger zone.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Detect if the player exits the trigger zone
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("Player exited door trigger zone.");
        }
    }

    private void OpenDoor()
    {
        Debug.Log("The door has been opened!");
        
        // Make the wall disappear
        if (wallToDisappear != null)
        {
            wallToDisappear.SetActive(false);
            Debug.Log("Wall has been removed.");
        }

        Destroy(gameObject); // Remove the door
    }
}
