using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardBattleManager : MonoBehaviour
{
    public int[,] playerCards = new int[5,2]; // card value then the card element
    public GameObject[] playerCardGameObjects = new GameObject[5];
    private int playerHealth = 10;
    public TextMeshProUGUI playerHealthText;
    public int[,] bossCards = new int[5,2];
    public GameObject[] bossCardGameObjects = new GameObject[5];
    private int bossHealth = 10;
    public TextMeshProUGUI bossHealthText;
    System.Random rnd;

    // Start is called before the first frame update
    void Start()
    {
        rnd = new();
        playerCardGameObjects = GameObject.FindGameObjectsWithTag("PlayerCard");
        bossCardGameObjects = GameObject.FindGameObjectsWithTag("BossCard");
        DrawFiveCards();
        UpdateView();
        // draw phase 5 cards are generated
        // choose phase, wait for player choice
        // calculation phase, calculate health changes and change used card to new card,
        // if health drops to zero end game whether win or loss
        // otherwise choose phase again
    }

    // card value 1-10
    // card element 1-ice, 2-fire, 3-water
    void DrawFiveCards() {
        for (int i = 0; i < 5; i++) {
            playerCards[i, 0] = rnd.Next(1, 10); // card value
            playerCards[i, 1] = rnd.Next(1, 3);  // card element
            bossCards[i, 0] = rnd.Next(1, 10); // card value
            bossCards[i, 1] = rnd.Next(1, 3);  // card element 
        }
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
            (playerCardElement == 2 && bossCardElement == 1))
        {
            // only apply damage if player card is higher value
            if (playerCardValue > bossCardValue) { 
                damageDealt = playerCardValue - bossCardValue;
            }
        }

        // Boss element beats player element
        if ((bossCardElement == 1 && playerCardElement == 3) || 
            (bossCardElement == 2 && playerCardElement == 1) || 
            (bossCardElement == 2 && playerCardElement == 1))
        {
            // only apply damage if boss card is higher value
            if (bossCardValue > playerCardValue) { 
                damageTaken = bossCardValue - playerCardValue;
            }
        }

        // update player and boss health
        playerHealth -= damageTaken;
        bossHealth -= damageDealt;

        // replace the two used cards with new cards
        playerCards[playerCardChosen,0] = rnd.Next(1, 10); // card value
        playerCards[playerCardChosen, 1] = rnd.Next(1, 3); // card element
        bossCards[bossCardChosen, 0] = rnd.Next(1, 10); // card value
        bossCards[bossCardChosen, 1] = rnd.Next(1, 3); // card element
    }

    // 0 game not over, 1 player won, 2 player lost
    int CheckForGameEnd() {
        if (bossHealth <= 0) {
            return 1;
        } else if (playerHealth <= 0) {
            return 2;
        } else {
            return 0;
        }
    }

    void UpdateView() {
        // Update card text
        for (int i = 0; i < 5; i++) {
            string cardElement = "NONE";
            if (playerCards[i, 1] == 1) cardElement = "Ice";
            if (playerCards[i, 1] == 2) cardElement = "Fire";
            if (playerCards[i, 1] == 3) cardElement = "Water";
            
            TextMeshProUGUI text = playerCardGameObjects[i].transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            text.SetText($"{playerCards[i, 0]} - {cardElement}");
        }

        // Update health text

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
