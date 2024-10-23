using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCard : MonoBehaviour
{
    public int id;
    private Renderer rend;
    private Color originalColor;
    private CardBattleManager cardBattleManager;

    void Start() {
        rend = gameObject.GetComponent<Renderer>();
        originalColor = rend.material.color;
        cardBattleManager = GameObject.Find("CardBattleManager").GetComponent<CardBattleManager>();
    }
    void OnMouseEnter()
    {
        rend.material.color = Color.cyan;
    }

    void OnMouseExit()
    {
        rend.material.color = originalColor;
    }

    void OnMouseDown()
    {
        cardBattleManager.SelectCard(id);
    }
}
