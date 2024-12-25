using System.Collections;
using UnityEngine;

public class CrackedTile : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;
    public float breakDelay = 1.0f;
    
    private bool isBreaking = false;
    private Vector3 initialPosition;

    // Reference to the TileManager to register the tile
    private TileManager tileManager;

    private void Start()
    {
        initialPosition = transform.position;
        // Find the TileManager in the scene and register this tile
        tileManager = FindObjectOfType<TileManager>();
        tileManager.RegisterTile(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player stepped on the tile
        if (other.CompareTag("Player") && !isBreaking)
        {
            StartCoroutine(BreakTile());
        }
    }

    private IEnumerator BreakTile()
    {
        isBreaking = true;

        // Shake the tile before breaking
        float elapsed = 0.0f;
        while (elapsed < shakeDuration)
        {
            float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetZ = Random.Range(-shakeMagnitude, shakeMagnitude);

            transform.position = initialPosition + new Vector3(offsetX, 0, offsetZ);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Return to original position
        transform.position = initialPosition;

        // Wait before destroying the tile
        yield return new WaitForSeconds(breakDelay);

        // Disable the tile (it's broken)
        gameObject.SetActive(false);
    }
}
