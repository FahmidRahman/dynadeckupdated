using UnityEngine;

public class LeverControlledDoor : MonoBehaviour
{
    public bool isOpen = false; // Track if the door is open
    public float doorOpenAngle = 90f; // Angle to open the door
    public float openSpeed = 2f; // Speed at which the door opens

    private Quaternion closedRotation; // Rotation when door is closed
    private Quaternion openRotation; // Rotation when door is open

    void Start()
    {
        // Store the initial rotation as the closed state
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + doorOpenAngle, transform.eulerAngles.z);
    }

    public void ToggleDoor()
    {
        if (isOpen)
        {
            CloseDoor(); // Call close if it's currently open
        }
        else
        {
            OpenDoor(); // Call open if it's currently closed
        }
    }

    private void OpenDoor()
    {
        if (!isOpen) // Only open if it's not already open
        {
            isOpen = true; // Set the state to open
            StartCoroutine(OpenDoorCoroutine()); // Start the opening coroutine
        }
    }

    private void CloseDoor()
    {
        if (isOpen) // Only close if it's currently open
        {
            isOpen = false; // Set the state to closed
            StartCoroutine(CloseDoorCoroutine()); // Start the closing coroutine
        }
    }

    private System.Collections.IEnumerator OpenDoorCoroutine()
    {
        float elapsed = 0;

        while (elapsed < 1)
        {
            transform.rotation = Quaternion.Slerp(closedRotation, openRotation, elapsed); // Smoothly transition to open
            elapsed += Time.deltaTime * openSpeed; // Increment elapsed time
            yield return null; // Wait for the next frame
        }

        transform.rotation = openRotation; // Ensure the door is fully open
        Debug.Log("Door is now open!");
    }

    private System.Collections.IEnumerator CloseDoorCoroutine()
    {
        float elapsed = 0;

        while (elapsed < 1)
        {
            transform.rotation = Quaternion.Slerp(openRotation, closedRotation, elapsed); // Smoothly transition to closed
            elapsed += Time.deltaTime * openSpeed; // Increment elapsed time
            yield return null; // Wait for the next frame
        }

        transform.rotation = closedRotation; // Ensure the door is fully closed
        Debug.Log("Door is now closed!");
    }
}
