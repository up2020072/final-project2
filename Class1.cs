using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileMap : MonoBehaviour
{
    public GameObject tileprefab;
    public TileScriptableObject[] tiletypes;
    public Color[] mapcolors;
    public GameObject[,] chunks;
    public GameObject[,] tiles;
    public List<GameObject> objects;
    public GameObject tree;
    public int ChunkSize = 100;

    private float tileheight;
    public float mapsize = 200;
    private float frequency = 0.008f;
    private int seed;
    private float treecoverage = 0.5f;
    private float forestdropoff = 0.2f;

    public RawImage mapsprite;
    public GameObject mapindicator;
    private Texture2D map;


    public bool MakeIsland;
    public bool GenerateSeed = true;
    public bool NewMap;


    //init 
    //generate tile
    //tile type

    //regenerate map
    //chunk generator 
    //seperate map gen function with constant map texture size
    public void Init()
    {
        if (GenerateSeed) seed = Random.Range(100000, 123456);
        Debug.Log(seed.ToString());
        map = new Texture2D((int)mapsize, (int)mapsize);
        //if (NewMap == false) tiles = new GameObject[(int)mapsize, (int)mapsize];
        InvokeRepeating("RenderMap", 0, 1);
        //for (int y = 0; y < mapsize; y++)
        //{
        //    for (int x = 0; x < mapsize; x++)
        //    {
        //        CreateTile(x, y);
        //    }
        //}
        map.Apply();
        mapsprite.texture = map;
    }
    private void CreateTile(int x, int y)
    {
        GameObject newtile = null;
        Vector2 tilepos = new Vector2((x - y) * 0.5f, ((x + y) * 0.25f) - mapsize / 4);
        if (NewMap == false || tiles[x, y] == null)
        {
            newtile = Instantiate(tileprefab, tilepos, Quaternion.identity, transform);
            tiles[x, y] = newtile;
        }
        else newtile = tiles[x, y];
        float distance = new Vector2(2 * x / mapsize - 1, 2 * y / mapsize - 1).magnitude;
        float noise = Mathf.PerlinNoise((x + seed) * frequency, (y + seed) * frequency)
        + 0.5f * Mathf.PerlinNoise((2 * (x + seed)) * frequency, (2 * (y + seed)) * frequency)
        + 0.25f * Mathf.PerlinNoise((4 * (x + seed)) * frequency, (4 * (y + seed)) * frequency);
        noise = noise / (1 + 0.5f + 0.25f);
        noise = Mathf.Pow(noise * 1.5f, 3f);
        if (MakeIsland) noise = (noise + (1 - (1.3f * distance))) / 2;

        TileScriptableObject tiletype = GetTileType(x, y, noise);
        newtile.GetComponent<SpriteRenderer>().sprite = tiletype.tilesprite;
        newtile.name = tiletype.name + "_tile";

        Color tilecolor = Color.white;
        tilecolor *= 0.6f + (0.4f * tileheight);
        tilecolor.a = 100;
        newtile.GetComponent<SpriteRenderer>().color = tilecolor;
    }
    private TileScriptableObject GetTileType(float x, float y, float noise)
    {

        if (noise < 0.2f)
        {
            tileheight = (noise) / 0.2f;
            DrawMap(x, y, mapcolors[0]);
            return tiletypes[0];
        }
        else if (noise < 0.25f)
        {
            tileheight = (noise - 0.2f) / 0.05f;
            DrawMap(x, y, mapcolors[1]);
            return tiletypes[1];
        }
        else if (noise < 0.6f)
        {
            tileheight = (noise - 0.25f) / 0.35f;
            float treechance = Mathf.Pow(tileheight, 2) * treecoverage;
            if (tileheight > Random.Range(forestdropoff, 1f) && Random.Range(0, 1f) < treechance)
            {
                GameObject newtree = Instantiate(tree, tiles[(int)x, (int)y].transform.position, Quaternion.identity, transform);
                objects.Add(newtree);
                DrawMap(x, y, mapcolors[3]);

            }
            else DrawMap(x, y, mapcolors[2]);
            return tiletypes[2];
        }
        else if (noise < 0.8f)
        {
            tileheight = (noise - 0.6f) / 0.2f;
            DrawMap(x, y, mapcolors[4]);
            return tiletypes[3];
        }
        else if (noise < 0.95f)
        {
            tileheight = (noise - 0.8f) / 0.15f;
            DrawMap(x, y, mapcolors[5]);
            return tiletypes[4];
        }
        else if (noise > 0.95f)
        {
            tileheight = (noise - 0.95f) / 0.1f;
            DrawMap(x, y, mapcolors[6]);
            return tiletypes[5];
        }
        else
        {
            DrawMap(x, y, mapcolors[2]);
            return tiletypes[0];
        }
    }
    private void DrawMap(float x, float y, Color color)
    {
        map.SetPixel((int)x, (int)y, color);
    }
    public void Island(bool b) { MakeIsland = b; }
    public void newMap() { NewMap = true; }
    public void MapSize(string size) { mapsize = int.Parse(size); }
    public void EnterSeed(string customseed) { seed = int.Parse(customseed); GenerateSeed = false; }
    public void TreeCoverage() { treecoverage = GameObject.Find("Slider").GetComponent<Slider>().value; }
    public void Frequency(string freq) { frequency = 0.008f * float.Parse(freq); }
}
