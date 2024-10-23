using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCard : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;
    void Start() {
        rend = gameObject.GetComponent<Renderer>();
        originalColor = rend.material.color;
    }
    void OnMouseEnter()
    {
        rend.material.color = Color.cyan;
    }

    void OnMouseExit()
    {
        rend.material.color = originalColor;
    }
}
