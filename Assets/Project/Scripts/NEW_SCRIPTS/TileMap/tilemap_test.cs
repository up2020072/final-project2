using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tilemap_test : MonoBehaviour
{
    public Tilemap tilemap;
    public UnityEngine.Tilemaps.Tile testTile;
    public int width;
    public int height;

    void Start()
    {
        //tilemap.SetTile(new Vector3Int(0, 0, 0), testTile);
        //for (int x = -width; x < width; x++)
        //{
        //    for (int y = -height; y < height; y++)
        //    {
        //        tilemap.SetTile(new Vector3Int(x, y, 0), testTile);
        //    }
        //}
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            for (int x = -width; x < width; x++)
            {
                for (int y = -height; y < height; y++)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), testTile);
                }
            }
        }
    }
}
