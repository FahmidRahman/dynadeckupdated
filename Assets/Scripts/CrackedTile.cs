using UnityEngine;
using System.Collections;

public class TileBehavior : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;
    public float breakDelay = 1.0f;
    private bool isBreaking = false;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") && !isBreaking)
        {
            Debug.Log("Player stepped on tile!");
            StartCoroutine(BreakTile());
        }
    }

    private IEnumerator BreakTile()
    {
        isBreaking = true;

        // shake the tile
        float elapsed = 0.0f;
        while (elapsed < shakeDuration)
        {
            float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetZ = Random.Range(-shakeMagnitude, shakeMagnitude);

            transform.position = initialPosition + new Vector3(offsetX, 0, offsetZ);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = initialPosition;

        yield return new WaitForSeconds(breakDelay);

        Destroy(gameObject);
    }
}
