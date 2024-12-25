using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Timer text on the UI
    public Transform player; // Reference to the player object
    public Transform playerStartPosition; // Starting position for the player
    public GameObject door; // Door GameObject to be destroyed
    public ButtonActivator[] buttons; // Array of buttons in the scene
    public float timeLimit = 90f; // Time limit to complete the puzzle

    private int currentButtonIndex = 0; // Tracks the current button in the sequence
    private float timer = 0f; // Tracks remaining time
    private bool isTimerRunning = false; // Is the timer active

    private void Start()
    {
        ResetButtons();
        timerText.gameObject.SetActive(false); // Hide the timer at the start
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                timer = 0f;
                isTimerRunning = false;
                ResetLevel(); // If time runs out, reset the level
            }

            UpdateTimerText();
        }
    }

    public bool IsCorrectButton(int buttonOrder)
    {
        return buttonOrder == currentButtonIndex + 1;
    }

    public void ActivateButton(int buttonOrder)
    {
        if (buttonOrder == currentButtonIndex + 1)
        {
            currentButtonIndex++;

            if (currentButtonIndex == 1)
            {
                StartTimer(); // Start the timer when the first button is pressed
            }

            if (currentButtonIndex == buttons.Length)
            {
                UnlockDoor(); // Unlock the door if all buttons are pressed in the correct order
            }
        }
        else
        {
            ResetLevel(); // Reset the level if the wrong button is pressed
        }
    }

    private void UnlockDoor()
    {
        isTimerRunning = false;

        if (door != null)
        {
            Destroy(door); // Destroy the door to proceed
        }

        timerText.gameObject.SetActive(false); // Hide the timer once the door is unlocked
    }

    public void ResetLevel()
    {
        ResetButtons();

        // Teleport the player back to the starting position
        if (player != null && playerStartPosition != null)
        {
            player.position = playerStartPosition.position;
            player.rotation = playerStartPosition.rotation;
        }

        timerText.gameObject.SetActive(false); // Hide the timer when resetting
        ResetTimer();
    }

    public void ResetButtons()
    {
        currentButtonIndex = 0;

        foreach (ButtonActivator button in buttons)
        {
            button.ResetButton(); // Reset each button to its original state
        }
    }

    private void ResetTimer()
    {
        timer = timeLimit;
        isTimerRunning = false;
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = $"Time Left: {Mathf.CeilToInt(timer)}s";
        }
    }

    public void StartTimer()
    {
        if (!isTimerRunning)
        {
            isTimerRunning = true;
            timer = timeLimit;
            timerText.gameObject.SetActive(true); // Show the timer when starting
        }
    }
}
