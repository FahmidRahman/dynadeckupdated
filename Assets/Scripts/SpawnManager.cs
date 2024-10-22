using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static Vector3 playerSpawnPosition;

    void Start()
    {

        // Find the spawn point in the current scene
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn");

        if (spawnPoint != null)
        {
            // Set the spawn position to the spawn point's position
            playerSpawnPosition = spawnPoint.transform.position;

            // Check if the player exists, or instantiate it if not
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                // Move the player to the spawn position
                player.transform.position = playerSpawnPosition;
            }
            else
            {
                // If the player is not in the scene, instantiate it
                Debug.LogWarning("Player not found in the scene! Instantiating a new player.");
                player = Instantiate(Resources.Load("PlayerPrefab") as GameObject); // Ensure you have a player prefab in Resources
                player.transform.position = playerSpawnPosition;
            }
        }
        else
        {
            Debug.LogWarning("PlayerSpawnPoint not found in scene, using default position.");
        }
    }
}
