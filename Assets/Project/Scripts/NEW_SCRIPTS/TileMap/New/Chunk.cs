using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public float ID;
    public Tile[,] Tiles;
    public List<GameObject> Objects =  new List<GameObject>();
    public GameObject Parent;

    //used to update tiles in the chunk
    //called on chunks within a certain range of the player to increase performance
    public void UpdateTiles()
    {
        foreach (var tile in Tiles)
        {
            tile.UpdateTile();
        }
    }
}
