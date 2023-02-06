using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    private int MapRes = 512;
    public RawImage mapSprite;
    public GameObject mapIndicator;
    private Texture2D map;
    public int currentLevel;
    public int seed;
    public Vector2 Pos;

    // Update is called once per frame
    public void Start()
    {
        map = new Texture2D(MapRes, MapRes);
        InvokeRepeating("GenerateMap", 0, 1f);
    }
    public void GenerateMap()
    {
        Pos = World.tileMap.chunkPos * 16;
        float increment = 256f / MapRes;
        for (int x = 0; x < MapRes; x++)
        {
            for (int y = 0; y < MapRes; y++)
            {
                Color pixelcolour = World.tileMap.GetTile(Pos.x + x-(MapRes/2),Pos.y + y - (MapRes / 2)).mapColour;
                map.SetPixel(x, y, pixelcolour);
            }
        }
        map.Apply();
        mapSprite.texture = map;
    }
}
