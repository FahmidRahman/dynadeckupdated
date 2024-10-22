using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private HashSet<string> items = new HashSet<string>(); // Using HashSet to avoid duplicates

    public void AddItem(string itemName)
    {
        items.Add(itemName);
        Debug.Log(itemName + " added to inventory!");
    }

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }
}
