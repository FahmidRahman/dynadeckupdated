using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    public float rotationSpeed = 200f; // Speed of rotation
    public float rotationDelay = 1f; // Delay between rotations

    private bool isRotating = false; // Track if rotation is in progress
    private int rotationStep = 0; // Tracks the current step in the rotation sequence

    void Update()
    {
        // Start rotation if not already rotating
        if (!isRotating)
        {
            StartCoroutine(RotateCube());
        }
    }

    System.Collections.IEnumerator RotateCube()
    {
        isRotating = true;

        // Define the rotation based on the step
        Vector3 rotationAmount;
        switch (rotationStep)
        {
            case 0: // Rotate 90 degrees on X-axis (left)
                rotationAmount = Vector3.right * -90f;
                break;
            case 1: // Rotate 90 degrees on Y-axis (right)
                rotationAmount = Vector3.up * 90f;
                break;
            case 2: // Rotate 90 degrees on X-axis (right)
                rotationAmount = Vector3.right * 90f;
                break;
            case 3: // Rotate 90 degrees on Y-axis (left)
                rotationAmount = Vector3.up * -90f;
                break;
            default:
                rotationAmount = Vector3.zero;
                break;
        }

        // Perform the rotation
        yield return RotateToAngle(rotationAmount);

        // Update the rotation step (loop through 0 to 3)
        rotationStep = (rotationStep + 1) % 4;

        // Wait before allowing the next rotation
        yield return new WaitForSeconds(rotationDelay);

        isRotating = false;
    }

    System.Collections.IEnumerator RotateToAngle(Vector3 rotationAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(rotationAmount);
        float elapsed = 0f;

        while (elapsed < 1f)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed);
            elapsed += Time.deltaTime * (rotationSpeed / 90f); // Normalize rotation speed
            yield return null;
        }

        transform.rotation = endRotation;
    }
}
