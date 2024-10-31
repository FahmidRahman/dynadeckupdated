using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookInteraction : MonoBehaviour
{
    public GameObject messagePanel;
    public TextMeshProUGUI messageText;
    public string dialogueMessage = "This is the message that appears when you click the book.";
    
    private bool isPlayerInRange = false;
    private bool isMessageActive = false;

    private void Update()
    {
        // Check if the player is in range and presses "E"
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (isMessageActive)
            {
                HideMessage();
            }
            else
            {
                ShowMessage();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the trigger zone
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exits the trigger zone
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            HideMessage(); // Hide the message if the player leaves the range
        }
    }

    // Show the message on the UI panel
    private void ShowMessage()
    {
        messagePanel.SetActive(true);
        messageText.text = dialogueMessage;
        
        isMessageActive = true;
    }

    // Hide the message when needed
    public void HideMessage()
    {
        messagePanel.SetActive(false);
        
        isMessageActive = false;
    }
}
