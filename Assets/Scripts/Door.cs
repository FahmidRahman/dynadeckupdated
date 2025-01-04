using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string newSceneName;            // The name of the scene to load

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding with the door is the player
        if (other.CompareTag("Player"))
        {  
            SceneManager.LoadScene(newSceneName);
        }
    }
}
