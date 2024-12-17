using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Transform teleportDestination; // Reference to the destination

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player fell into the teleport zone!");

            // Get the CharacterController component
            CharacterController controller = other.GetComponent<CharacterController>();

            if (controller != null)
            {
                // Disable the controller to avoid interference, then teleport
                controller.enabled = false;
                other.transform.position = teleportDestination.position;
                controller.enabled = true;
            }
            else
            {
                // Fallback for objects without CharacterController
                other.transform.position = teleportDestination.position;
            }
        }
    }
}
