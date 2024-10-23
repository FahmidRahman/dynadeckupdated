using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBattle : MonoBehaviour
{
    public int[][] playerCards; // card value then the card element
    private int playerHealth = 10;
    public int[][] bossCards;
    private int bossHealth = 10;
    System.Random rnd = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        DrawFiveCards();
        while (CheckForGameEnd() == 0) {
            
        }
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
            playerCards[i][0] = rnd.Next(1, 10); // card value
            playerCards[i][1] = rnd.Next(1, 3);  // card element
            bossCards[i][0] = rnd.Next(1, 10); // card value
            bossCards[i][1] = rnd.Next(1, 3);  // card element 
        }


    }

    void BattlePhase(int playerCardChosen, int bossCardChosen) 
    {
        int playerCardValue = playerCards[playerCardChosen][0];
        int playerCardElement = playerCards[playerCardChosen][1];
        int bossCardValue = bossCards[bossCardChosen][0];
        int bossCardElement = bossCards[bossCardChosen][1];
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
        playerCards[playerCardChosen][0] = rnd.Next(1, 10); // card value
        playerCards[playerCardChosen][1] = rnd.Next(1, 3); // card element
        bossCards[bossCardChosen][0] = rnd.Next(1, 10); // card value
        bossCards[bossCardChosen][1] = rnd.Next(1, 3); // card element
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
