using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

// draw phase 5 cards are generated
// choose phase, wait for player choice
// calculation phase, calculate health changes and change used card to new card,
// if health drops to zero end game whether win or loss
// otherwise choosing phase again

public class CardBattleManager : MonoBehaviour
{
    // Player
    public int[,] playerCards = new int[5,2]; // card value then the card element
    public GameObject[] playerCardGameObjects = new GameObject[5];
    private int playerHealth = 10;
    public TextMeshProUGUI playerHealthText;
    public GameObject lastPlayedPlayerCard;

    // Boss
    public int[,] bossCards = new int[5,2];
    public GameObject[] bossCardGameObjects = new GameObject[5];
    private int bossHealth = 10;
    public TextMeshProUGUI bossHealthText;
    public GameObject lastPlayedBossCard;

    System.Random rnd = new();
    public GameObject cards;
    public TextMeshProUGUI FinalText;

    // Start is called before the first frame update
    void Start()
    {
        // assign ids to every card gameobject
        playerCardGameObjects = GameObject.FindGameObjectsWithTag("PlayerCard");
        bossCardGameObjects = GameObject.FindGameObjectsWithTag("BossCard");
        for (int i = 0; i < 5; i++) {
            playerCardGameObjects[i].GetComponent<PlayerCard>().id = i;
            bossCardGameObjects[i].GetComponent<BossCard>().id = i;
        }

        DrawFiveCards();
        UpdateView();
    }

    // card value 1-10
    // card element 1-ice, 2-fire, 3-water
    void DrawFiveCards() {
        for (int i = 0; i < 5; i++) {
            playerCards[i, 0] = rnd.Next(1, 11); // card value
            playerCards[i, 1] = rnd.Next(1, 4);  // card element
            bossCards[i, 0] = rnd.Next(1, 11); // card value
            bossCards[i, 1] = rnd.Next(1, 4);  // card element 
        }
    }

    // This is called by the Player Card script which is attached to the player's cards, when a card is clicked
    public void SelectCard(int id) {
        BattlePhase(id, rnd.Next(0, 5));
    }

    void BattlePhase(int playerCardChosen, int bossCardChosen) 
    {
        int playerCardValue = playerCards[playerCardChosen, 0];
        int playerCardElement = playerCards[playerCardChosen, 1];
        int bossCardValue = bossCards[bossCardChosen, 0];
        int bossCardElement = bossCards[bossCardChosen, 1];
        int damageDealt = 0;
        int damageTaken = 0;

        // Player element beats boss element
        if ((playerCardElement == 1 && bossCardElement == 3) || 
            (playerCardElement == 2 && bossCardElement == 1) || 
            (playerCardElement == 3 && bossCardElement == 2))
        {
            // only apply damage if player card is higher value
            if (playerCardValue > bossCardValue) { 
                damageDealt = playerCardValue - bossCardValue;
            }
        }

        // Boss element beats player element
        if ((bossCardElement == 1 && playerCardElement == 3) || 
            (bossCardElement == 2 && playerCardElement == 1) || 
            (bossCardElement == 3 && playerCardElement == 2))
        {
            // only apply damage if boss card is higher value
            if (bossCardValue > playerCardValue) { 
                damageTaken = bossCardValue - playerCardValue;
            }
        }

        // update player and boss health
        playerHealth -= damageTaken;
        bossHealth -= damageDealt;

        // set last played player and boss card text before resetting the card
        lastPlayedBossCard.SetActive(true);
        lastPlayedPlayerCard.SetActive(true);
        lastPlayedPlayerCard.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(
            $"{playerCardValue} - {GetElement(playerCardElement)}"
        );
        lastPlayedBossCard.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(
            $"{bossCardValue} - {GetElement(bossCardElement)}"
        );

        // replace the two used cards with new cards
        playerCards[playerCardChosen,0] = rnd.Next(1, 11); // card value
        playerCards[playerCardChosen, 1] = rnd.Next(1, 4); // card element
        bossCards[bossCardChosen, 0] = rnd.Next(1, 11); // card value
        bossCards[bossCardChosen, 1] = rnd.Next(1, 4); // card element

        UpdateView();
    }

    // 0 game not over, 1 player won, 2 player lost
    void CheckForGameEnd() {
        if (bossHealth <= 0) {
            cards.SetActive(false);
            FinalText.SetText("YOU DEFEATED THE DUNGEON!");
        } else if (playerHealth <= 0) {
            cards.SetActive(false);
            FinalText.SetText("GAME OVER...");
        }
    }

    void UpdateView() {

        // Update card text
        for (int i = 0; i < 5; i++) {
            TextMeshProUGUI text = playerCardGameObjects[i].transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            text.SetText($"{playerCards[i, 0]} - {GetElement(playerCards[i, 1])}");
        }

        // Update health text
        playerHealthText.SetText($"Player Health: {playerHealth}");
        bossHealthText.SetText($"Boss Health: {bossHealth}");

        CheckForGameEnd();
    }

    string GetElement(int val) {
        if (val == 1) return "Ice";
        if (val == 2) return "Fire";
        if (val == 3) return "Water";
        else return null;
    }
}
