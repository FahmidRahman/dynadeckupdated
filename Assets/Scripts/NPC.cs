using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    // Array or list of dialogue paragraphs
    public string[] npcDialogueParagraphs;
    public string postKeyDialogue = "Safe travels..."; // Dialogue after receiving the key
    private int currentDialogueIndex = 0; // Tracks the current paragraph index

    public float interactionDistance = 3.0f;
    private bool isPlayerInRange = false;

    public Transform player;

    // Reference to the Speech Bubble Panel and Bubble Text (TextMeshPro)
    public GameObject speechBubble; // The speech bubble panel
    public TextMeshProUGUI bubbleText; // The text inside the speech bubble

    // Reference to the Key Panel
    public GameObject keyPanel; // The key panel to show after dialogue

    private bool isInteracting = false;

    private CharacterMovement characterMovement;  // Reference to the CharacterMovement script
    private PlayerInput playerInput; // Reference to the PlayerInput script
    private Inventory playerInventory; // Reference to the player's inventory

    void Start()
    {
        bubbleText.text = "";  // Initially clear the dialogue UI
        speechBubble.SetActive(false);  // Initially hide the speech bubble
        keyPanel.SetActive(false); // Initially hide the key panel

        characterMovement = player.GetComponent<CharacterMovement>(); // Get the CharacterMovement component from the player
        playerInput = player.GetComponent<PlayerInput>(); // Get the PlayerInput component from the player
        playerInventory = player.GetComponent<Inventory>(); // Get the Inventory component from the player
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= interactionDistance)
        {
            isPlayerInRange = true;

            // Check if the player presses 'E' to interact
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isInteracting)
                {
                    StartInteraction();
                }
                else
                {
                    ShowNextDialogue();
                }
            }
        }
        else
        {
            isPlayerInRange = false;
            if (!isInteracting)
            {
                bubbleText.text = "";  // Clear the dialogue if player is out of range and not interacting
                speechBubble.SetActive(false); // Hide the speech bubble
            }
        }

        // Lock player movement when interacting
        if (isInteracting)
        {
            characterMovement.enabled = false; // Disable character movement
            playerInput.enabled = false; // Disable player input
            Vector3 bubblePosition = transform.position + new Vector3(0, 2, 0); // Adjust Y to your liking
            speechBubble.transform.position = Camera.main.WorldToScreenPoint(bubblePosition);
        }
        else
        {
            characterMovement.enabled = true; // Re-enable character movement
            playerInput.enabled = true; // Re-enable player input
        }
    }

    void StartInteraction()
    {
        // Start the dialogue interaction
        isInteracting = true;
        currentDialogueIndex = 0;  // Reset to the first paragraph

        // Check if player already has the key
        if (!playerInventory.HasItem("Key"))
        {
            ShowNextDialogue();
        }
        else
        {
            // If the key has been handed over, display "Safe Travels..."
            bubbleText.text = postKeyDialogue; // Set the text to "Safe travels..."
            speechBubble.SetActive(true); // Show the speech bubble
            EndInteraction(); // Immediately end interaction since there's no further dialogue
        }
    }

    void ShowNextDialogue()
    {
        // Display the current paragraph
        if (currentDialogueIndex < npcDialogueParagraphs.Length)
        {
            bubbleText.text = npcDialogueParagraphs[currentDialogueIndex];
            speechBubble.SetActive(true); // Show the speech bubble
            currentDialogueIndex++;
        }
        else
        {
            EndInteraction();  // End interaction when all paragraphs are shown
        }
    }

    void EndInteraction()
    {
        // End the dialogue
        isInteracting = false;
        bubbleText.text = "";  // Clear the bubble text
        speechBubble.SetActive(false); // Hide the speech bubble

        // Show the key panel after dialogue ends
        if (!playerInventory.HasItem("Key"))
        {
            keyPanel.SetActive(true); // Show the key panel
            playerInventory.AddItem("Key"); // Add the key to the inventory
            StartCoroutine(DisableKeyPanelAfterDelay(3f)); // Hide after 3 seconds
        }
        else
        {
            // If the key has been handed over, display "Safe Travels..."
            bubbleText.text = postKeyDialogue; // Set the text to "Safe travels..."
            speechBubble.SetActive(true); // Show the speech bubble
        }
    }

    private IEnumerator DisableKeyPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        keyPanel.SetActive(false); // Hide the key panel after the delay
    }
}
