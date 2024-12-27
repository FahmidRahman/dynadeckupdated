using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private List<GameObject> tiles = new List<GameObject>();

    // Register a tile when it's created
    public void RegisterTile(GameObject tile)
    {
        if (!tiles.Contains(tile))
        {
            tiles.Add(tile);
        }
    }

    // Reset all tiles (set them active again)
    public void ResetAllTiles()
    {
        foreach (var tile in tiles)
        {
            tile.SetActive(true); // Reactivate the tile
            // You can also reset tile position or other properties if needed
        }

        Debug.Log("All tiles have been reset.");
    }
}
