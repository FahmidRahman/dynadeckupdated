using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class BookInteraction : MonoBehaviour
{
    
    public GameObject messagePanel;
    public TextMeshProUGUI messageText;
    public GameObject closeButton;


    public string dialogueMessage = "This is the message that appears when you click the book.";

    // Check for mouse clicks
    private void OnMouseDown()
    {
        ShowMessage();
    }

    // Show the message on the UI panel
    private void ShowMessage()
    {
        // Enable the message panel
        messagePanel.SetActive(true);
        // Set the text component to display the message
        messageText.text = dialogueMessage;
        closeButton.SetActive(true);
    }

    // Hide the message when needed (optional)
    public void HideMessage()
    {
        // Disable the message panel
        messagePanel.SetActive(false);
        closeButton.SetActive(false);
    }
}
