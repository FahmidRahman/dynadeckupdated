using System.Collections;
using UnityEngine;
using TMPro;  // If you use TextMeshPro

public class KeyHandoverUI : MonoBehaviour
{
    public GameObject keyUIPanel; // Reference to the key UI panel
    public float displayDuration = 3f; // How long to show the UI

    void Start()
    {
        // Ensure the UI is hidden at the start
        keyUIPanel.SetActive(false);
    }

    // Call this method to show the key handover UI
    public void ShowKeyUI()
    {
        StartCoroutine(DisplayKeyUI());
    }

    // Coroutine to show and hide the key UI after a delay
    IEnumerator DisplayKeyUI()
    {
        keyUIPanel.SetActive(true); // Show the UI panel
        yield return new WaitForSeconds(displayDuration); // Wait for the duration
        keyUIPanel.SetActive(false); // Hide the UI panel
    }
}
