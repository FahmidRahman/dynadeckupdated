using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardBattleManager : MonoBehaviour
{
    // Player
    public int[,] playerCards = new int[5, 2]; // card value, card element
    public GameObject[] playerCardGameObjects = new GameObject[5];
    private int playerHealth = 10;
    public TextMeshProUGUI playerHealthText;
    public GameObject lastPlayedPlayerCard;

    // Boss
    public int[,] bossCards = new int[5, 2];
    public GameObject[] bossCardGameObjects = new GameObject[5];
    private int bossHealth = 10;
    public TextMeshProUGUI bossHealthText;
    public GameObject lastPlayedBossCard;

    private System.Random rnd = new System.Random();
    public GameObject cards;
    public TextMeshProUGUI FinalText;

    public Button retryButton;
    public Button ascendButton;
    public string nextLevelSceneName;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize cards for player and boss
        playerCardGameObjects = GameObject.FindGameObjectsWithTag("PlayerCard");
        bossCardGameObjects = GameObject.FindGameObjectsWithTag("BossCard");

        retryButton.gameObject.SetActive(false);
        ascendButton.gameObject.SetActive(false);

        for (int i = 0; i < 5; i++)
        {
            playerCardGameObjects[i].GetComponent<PlayerCard>().id = i;
            bossCardGameObjects[i].GetComponent<BossCard>().id = i;
        }

        DrawFiveCards();
        UpdateView();
    }

    // Draw 5 cards for both player and boss
    void DrawFiveCards()
    {
        for (int i = 0; i < 5; i++)
        {
            playerCards[i, 0] = rnd.Next(1, 11); // card value
            playerCards[i, 1] = rnd.Next(1, 4);  // card element
            bossCards[i, 0] = rnd.Next(1, 11);   // card value
            bossCards[i, 1] = rnd.Next(1, 4);    // card element
        }
    }

    // This is called when a player card is selected
    public void SelectCard(int id)
    {
        // Select a random card for the boss and start the battle phase
        int bossCardChosen = rnd.Next(0, 5);
        BattlePhase(id, bossCardChosen);
    }

    // Main Battle Phase
    void BattlePhase(int playerCardChosen, int bossCardChosen)
    {
        int playerCardValue = playerCards[playerCardChosen, 0];
        int playerCardElement = playerCards[playerCardChosen, 1];
        int bossCardValue = bossCards[bossCardChosen, 0];
        int bossCardElement = bossCards[bossCardChosen, 1];

        int damageDealt = 0;
        int damageTaken = 0;

        // Player's element beats boss's element logic
        if (IsPlayerElementStronger(playerCardElement, bossCardElement))
        {
            if (playerCardValue > bossCardValue)
            {
                damageDealt = playerCardValue - bossCardValue;
            }
        }

        // Boss's element beats player's element logic
        if (IsBossElementStronger(bossCardElement, playerCardElement))
        {
            if (bossCardValue > playerCardValue)
            {
                damageTaken = bossCardValue - playerCardValue;
            }
        }

        // Update health
        playerHealth -= damageTaken;
        bossHealth -= damageDealt;

        // Update the UI with the latest health
        UpdateView();

        // Show the last played cards
        DisplayLastPlayedCards(playerCardValue, playerCardElement, bossCardValue, bossCardElement);

        // Replace the used cards with new random cards
        playerCards[playerCardChosen, 0] = rnd.Next(1, 11);
        playerCards[playerCardChosen, 1] = rnd.Next(1, 4);
        bossCards[bossCardChosen, 0] = rnd.Next(1, 11);
        bossCards[bossCardChosen, 1] = rnd.Next(1, 4);

        // Check if the game has ended
        CheckForGameEnd();
    }

    // Helper function to determine if the player's element beats the boss's element
    bool IsPlayerElementStronger(int playerElement, int bossElement)
    {
        return (playerElement == 1 && bossElement == 3) ||  // Ice beats Water
               (playerElement == 2 && bossElement == 1) ||  // Fire beats Ice
               (playerElement == 3 && bossElement == 2);    // Water beats Fire
    }

    // Helper function to determine if the boss's element beats the player's element
    bool IsBossElementStronger(int bossElement, int playerElement)
    {
        return (bossElement == 1 && playerElement == 3) ||  // Ice beats Water
               (bossElement == 2 && playerElement == 1) ||  // Fire beats Ice
               (bossElement == 3 && playerElement == 2);    // Water beats Fire
    }

    // Check if the game is over (either player or boss health reaches 0)
    void CheckForGameEnd()
    {
        if (bossHealth <= 0)
        {
            cards.SetActive(false);
            FinalText.SetText("YOU DEFEATED THE BOSS!");
            ascendButton.gameObject.SetActive(true);
        }
        else if (playerHealth <= 0)
        {
            cards.SetActive(false);
            FinalText.SetText("GAME OVER...");
            retryButton.gameObject.SetActive(true);
        }
    }

    // Update the view with the current health and cards
    void UpdateView()
    {
        // Update card text (value and element)
        for (int i = 0; i < 5; i++)
        {
            TextMeshProUGUI text = playerCardGameObjects[i].transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            text.SetText($"{playerCards[i, 0]} - {GetElement(playerCards[i, 1])}");
        }

        // Update health text
        playerHealthText.SetText($"Player Health: {playerHealth}");
        bossHealthText.SetText($"Boss Health: {bossHealth}");
    }

    // Display the last played cards and their values/elements
    void DisplayLastPlayedCards(int playerCardValue, int playerCardElement, int bossCardValue, int bossCardElement)
    {
        lastPlayedPlayerCard.SetActive(true);
        lastPlayedBossCard.SetActive(true);

        lastPlayedPlayerCard.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(
            $"{playerCardValue} - {GetElement(playerCardElement)}"
        );

        lastPlayedBossCard.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(
            $"{bossCardValue} - {GetElement(bossCardElement)}"
        );
    }

    // Helper function to return element name from its code (1: Ice, 2: Fire, 3: Water)
    string GetElement(int elementCode)
    {
        switch (elementCode)
        {
            case 1: return "Ice";
            case 2: return "Fire";
            case 3: return "Water";
            default: return "Unknown";
        }
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AscendLevel()
    {
        if (!string.IsNullOrEmpty(nextLevelSceneName))
        {
            SceneManager.LoadScene(nextLevelSceneName);
        }
        else
        {
            Debug.LogError("Next Level Scene Name is not set in the Inspector.");
        }
    }
}
