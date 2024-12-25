using UnityEngine;

public class ButtonActivator : MonoBehaviour
{
    public int buttonOrder; // The order of this button in the sequence
    public Material activeMaterial; // Material for active button
    public Material inactiveMaterial; // Material for inactive button
    private bool isPlayerInRange = false; // Tracks if the player is in range
    private Renderer buttonRenderer;
    private GameManager gameManager;

    private void Start()
    {
        buttonRenderer = GetComponent<Renderer>();
        gameManager = Object.FindFirstObjectByType<GameManager>();
        ResetButton(); // Ensure the button starts in its reset state
    }

    private void Update()
    {
        // Check for player input when in range
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ActivateButton();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // Enable interaction
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // Disable interaction
        }
    }

    public void ActivateButton()
    {
        if (gameManager != null && gameManager.IsCorrectButton(buttonOrder))
        {
            buttonRenderer.material = activeMaterial; // Change to the active material
            gameManager.ActivateButton(buttonOrder); // Notify GameManager
        }
        else
        {
            gameManager.ResetLevel(); // Reset level on incorrect button press
        }
    }

    public void ResetButton()
    {
        if (buttonRenderer != null && inactiveMaterial != null)
        {
            buttonRenderer.material = inactiveMaterial; // Reset material to inactive
        }
        isPlayerInRange = false; // Ensure interaction is reset
    }
}
