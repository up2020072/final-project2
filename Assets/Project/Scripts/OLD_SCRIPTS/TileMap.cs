using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileMap : MonoBehaviour
{
    public GameObject tileprefab;
    public TileScriptableObject[] tiletypes;
    public Color[] mapcolors;
    //public List<GameObject[,]> chunks;
    public List<GameObject> objects;
    public GameObject tree;
    private int MapRes = 512;
    private int ChunkSize = 16;
    public Vector2 playerpos;
    public Vector2 chunkpos;
    private Vector2 tempchunkpos;

    private float tileheight;
    public int mapsize = 256;
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

    //dictionary chunk lookup
    //list initialization should be done in init function
    public Dictionary<Vector2Int, GameObject[,]> chunks = new Dictionary<Vector2Int, GameObject[,]>();
    public Dictionary<Vector2Int, GameObject> chunkparents = new Dictionary<Vector2Int, GameObject>();
    private List<Vector2Int> loadedchunks = new List<Vector2Int>();
    public int renderdistance = 1;
    public int temprenderdistance = 1;

    public void Init()
    {
        map = new Texture2D(MapRes, MapRes);
        GenerateMap();
        tempchunkpos = new Vector2();
        temprenderdistance = 1 + (int)Camera.main.orthographicSize / 7;
        int chunkamount = mapsize / ChunkSize;

        InvokeRepeating("LoadMap", 0, 0.1f);
        //chunk gen speed should correlate so the player cannot move through chunks faster than they load
    }
    public void LoadMap()
    {
        //renderdistance = 1 + (int)Camera.main.orthographicSize / 7;
        Vector2 pos = GameData.Data.player.transform.position / ChunkSize;
        chunkpos = new Vector2((pos.x + pos.y * 2), -(pos.x - pos.y * 2));
        chunkpos = new Vector2(Mathf.CeilToInt(chunkpos.x) - 1, Mathf.CeilToInt(chunkpos.y) - 1);
        if (chunkpos.magnitude != tempchunkpos.magnitude | renderdistance != temprenderdistance)
        {
            tempchunkpos = chunkpos;
            temprenderdistance = renderdistance;
            ChunkLoader();
        }
    }
    //whole system here needs reworking
    private void ChunkLoader()
    {
        for (int cx = (int)chunkpos.x - renderdistance; cx <= chunkpos.x + renderdistance; cx++)
        {
            for (int cy = (int)chunkpos.y - renderdistance; cy <= chunkpos.y + renderdistance; cy++)
            {
                if (chunks.ContainsKey(new Vector2Int(cx, cy))) continue;
                loadedchunks.Add(new Vector2Int(cx, cy));
                CreateChunk(cx, cy);
                ChunkUnloader();
            }
        }
    }
    private void ChunkUnloader()
    {
        List<Vector2Int> unloadqueue = new List<Vector2Int>();
        foreach (Vector2Int pos in loadedchunks)
        {
            if ((pos - chunkpos).magnitude > renderdistance * 1.42f)
            {
                Destroy(chunkparents[pos]);
                chunks.Remove(pos);
                chunkparents.Remove(pos);
                unloadqueue.Add(pos);
            }
        }
        foreach (Vector2Int pos in unloadqueue) loadedchunks.Remove(pos);
    }
    public float GetNoise(float x, float y)
    {
        //frequency should scale with size to be consistent
        //eg these values give same results just larger maps
        //100 7
        //1000 0.7 
        //10,000 0.07
        //reduce expensive maths functions such as pow
        float distance = (new Vector2(x, y).magnitude / mapsize);
        float noise = Mathf.PerlinNoise((x + seed) * frequency, (y + seed) * frequency)
        + 0.5f * Mathf.PerlinNoise((2 * (x + seed)) * frequency, (2 * (y + seed)) * frequency)
        + 0.25f * Mathf.PerlinNoise((4 * (x + seed)) * frequency, (4 * (y + seed)) * frequency);
        noise = noise / (1 + 0.5f + 0.25f);
        noise = Mathf.Pow(noise * 1.5f, 3f);
        if (MakeIsland) noise = (noise + (1 - (3 * distance))) / 2;
        return noise;
    }
    public void CreateChunk(int cx, int cy)
    {
        //might be worth looking at unitys built in tilemap as the render performance is much quicker and its currently the only area where the performance is struggling
        GameObject chunkparent = new GameObject(cx + "_" + cy);
        chunkparent.transform.parent = transform;
        chunkparents.Add(new Vector2Int(cx, cy), chunkparent);
        GameObject[,] chunk = new GameObject[ChunkSize, ChunkSize];
        for (int x = 0; x < ChunkSize; x++)
        {
            for (int y = 0; y < ChunkSize; y++)
            {
                GameObject newtile;
                float xpos = x + (cx * ChunkSize);
                float ypos = y + (cy * ChunkSize);
                Vector2 tilepos = new Vector2((xpos - ypos) * 0.5f, ((xpos + ypos) * 0.25f));
                newtile = Instantiate(tileprefab, tilepos, Quaternion.identity, chunkparent.transform);
                chunk[x, y] = newtile;
                TileScriptableObject tiletype = GetTileType(x, y, GetNoise(xpos, ypos), newtile.transform, chunkparent.transform);

                newtile.GetComponent<SpriteRenderer>().sprite = tiletype.tilesprite;
                newtile.name = tiletype.name + "_tile_chunk_" + cx + "_" + cy;
                //test implementation to give tile hitboxes - will need fleshing out to allow tiles to have properties
                if (tiletype.HasCollision)
                {
                    newtile.AddComponent<BoxCollider2D>();
                }
                Color tilecolor = Color.white;
                tilecolor *= 0.6f + (0.4f * tileheight);
                tilecolor.a = 100;
                newtile.GetComponent<SpriteRenderer>().color = tilecolor;
            }
        }
        Vector2Int chunkpos = new Vector2Int(cx, cy);
        chunks.Add(chunkpos, chunk);

    }
    //this whole function isnt very efficient
    private TileScriptableObject GetTileType(float x, float y, float noise, Transform tile, Transform chunk)
    {

        if (noise < 0.2f)
        {
            tileheight = (noise) / 0.2f;
            return tiletypes[0];
        }
        else if (noise < 0.25f)
        {
            tileheight = (noise - 0.2f) / 0.05f;
            return tiletypes[1];
        }
        else if (noise < 0.6f)
        {
            tileheight = (noise - 0.25f) / 0.35f;
            float treechance = Mathf.PerlinNoise((100 * (x + seed)) * frequency, (100 * (y + seed)) * frequency);
            if (tileheight > treechance + 0.4f)
            {
                GameObject newtree = Instantiate(tree, tile.transform.position, Quaternion.identity, chunk);
                objects.Add(newtree);

            }
            return tiletypes[2];
        }
        else if (noise < 0.8f)
        {
            tileheight = (noise - 0.6f) / 0.2f;
            return tiletypes[3];
        }
        else if (noise < 0.95f)
        {
            tileheight = (noise - 0.8f) / 0.15f;
            return tiletypes[4];
        }
        else if (noise > 0.95f)
        {
            tileheight = (noise - 0.95f) / 0.1f;
            return tiletypes[5];
        }
        else
        {
            return tiletypes[0];
        }
    }
    public void GenerateMap()
    {
        seed = Random.Range(100000, 999999);
        Debug.Log(seed.ToString());
        float increment = (float)mapsize / MapRes;
        for (int x = 0; x < MapRes; x++)
        {
            for (int y = 0; y < MapRes; y++)
            {
                map.SetPixel(x, y, GetColor(GetNoise((x * increment) - (mapsize / 2), (y * increment) - (mapsize / 2))));
            }
        }
        map.Apply();
        mapsprite.texture = map;
    }
    public Color GetColor(float noise)
    {
        if (noise < 0.2f) return mapcolors[0];
        if (noise < 0.25f) return mapcolors[1];
        if (noise < 0.6f) return mapcolors[2];
        if (noise < 0.8f) return mapcolors[3];
        if (noise < 0.95f) return mapcolors[4];
        if (noise > 0.95f) return mapcolors[5];
        else return mapcolors[0];
    }
    public void Island(bool b) { MakeIsland = b; }
    public void newMap() { NewMap = true; }
    public void MapSize(string size) { mapsize = int.Parse(size); }
    public void EnterSeed(string customseed) { seed = int.Parse(customseed); GenerateSeed = false; }
    public void TreeCoverage() { treecoverage = GameObject.Find("Slider").GetComponent<Slider>().value; }
    public void Frequency(string freq) { frequency = 0.008f * float.Parse(freq); }
    public void RenderDistance(string dist)
    {
        renderdistance = int.Parse(dist);
        ChunkUnloader();
    }
}
