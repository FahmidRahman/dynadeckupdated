using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string newSceneName;            // The name of the scene to load
    public Inventory playerInventory; // Reference to the player's inventory

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding with the door is the player
        if (other.CompareTag("Player"))
        {
            // Check if the player has the "Key" in their inventory
            if (playerInventory != null && playerInventory.HasItem("Key"))
            {
                // If the player has the key, load the new scene
                SceneManager.LoadScene(newSceneName);
            }
            else
            {
                Debug.Log("You need a key to open this door.");
            }
        }
    }
}
