using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public string ID;
    public Tile[,] Tiles = new Tile[16, 16];
    //public List<GameObject> Objects = new List<GameObject>();
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
    // have a function to parse the tile array into something saveable
    //could have tile id work like the animation lookup to resolve issues with vector3's not working correctly
}
