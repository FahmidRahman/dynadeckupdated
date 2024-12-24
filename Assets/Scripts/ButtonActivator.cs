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
        gameManager = FindObjectOfType<GameManager>();
        ResetButton();
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
            buttonRenderer.material = activeMaterial;
            gameManager.ActivateButton(buttonOrder);
        }
        else
        {
            gameManager.ResetButtons();
        }
    }

    public void ResetButton()
    {
        if (buttonRenderer != null && inactiveMaterial != null)
        {
            buttonRenderer.material = inactiveMaterial;
        }
    }
}
