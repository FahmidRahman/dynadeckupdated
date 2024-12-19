using UnityEngine;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    // Serialized fields for easy customization in Unity Inspector
    [SerializeField] private string[] npcDialogueParagraphs;
    [SerializeField] private float interactionDistance = 3.0f;
    [SerializeField] private GameObject speechBubble;
    [SerializeField] private TextMeshProUGUI bubbleText;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject keyPanel;  // Optional: Key UI panel for rewards

    // Private variables
    private int currentDialogueIndex;
    private bool isInteracting;
    private bool hasKey;  // Example flag for a player reward check
    private PlayerInput playerInput;
    private CharacterMovement characterMovement;

    // Custom events
    public delegate void DialogueEvent();
    public event DialogueEvent OnDialogueStart;
    public event DialogueEvent OnDialogueEnd;

    private void Start()
    {
        InitializeDialogue();
        InitializePlayerComponents();
    }

    private void Update()
    {
        HandleInteraction();
    }

    private void InitializeDialogue()
    {
        bubbleText.text = "";  // Clear any text initially
        speechBubble.SetActive(false);  // Hide bubble initially
        keyPanel?.SetActive(false);  // Hide key panel if it exists
        currentDialogueIndex = 0;
    }

    private void InitializePlayerComponents()
    {
        characterMovement = player.GetComponent<CharacterMovement>();
        playerInput = player.GetComponent<PlayerInput>();
        if (characterMovement == null || playerInput == null)
            Debug.LogError("Player movement or input component is missing.");
    }

    private void HandleInteraction()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= interactionDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isInteracting)
                    StartDialogue();
                else
                    ShowNextDialogue();
            }
        }
        else if (isInteracting)
        {
            EndDialogue();
        }
    }

    private void StartDialogue()
    {
        isInteracting = true;
        currentDialogueIndex = 0;
        OnDialogueStart?.Invoke();
        TogglePlayerMovement(false);
        ShowNextDialogue();
    }

    private void ShowNextDialogue()
    {
        if (currentDialogueIndex < npcDialogueParagraphs.Length)
        {
            bubbleText.text = npcDialogueParagraphs[currentDialogueIndex];
            speechBubble.SetActive(true);
            currentDialogueIndex++;
        }
        else
        {
            EndDialogue();
            GiveReward();  // Optional: Give reward if applicable
        }
    }

    private void EndDialogue()
    {
        isInteracting = false;
        OnDialogueEnd?.Invoke();
        bubbleText.text = "";  // Clear text
        speechBubble.SetActive(false);
        TogglePlayerMovement(true);

        if (hasKey)
        {
            bubbleText.text = "Safe Travels...";
        }
    }

    private void TogglePlayerMovement(bool enable)
    {
        if (characterMovement != null) characterMovement.enabled = enable;
        if (playerInput != null) playerInput.enabled = enable;
    }

    private void GiveReward()
    {
        // Check if a reward (like a key) should be given to the player
        if (!hasKey)
        {
            hasKey = true;
            if (keyPanel != null) keyPanel.SetActive(true);  // Show key panel
            Invoke(nameof(HideKeyPanel), 3f);  // Hide key panel after 3 seconds
        }
    }

    private void HideKeyPanel()
    {
        if (keyPanel != null) keyPanel.SetActive(false);
    }

    public bool HasKey()
    {
        return hasKey;
    }

}
