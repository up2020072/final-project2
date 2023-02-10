using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public static World tileMap;
    //levels contains all chunks that have ever been created
    //loaded chunks contains the ids of currently loaded chunks
    public Dictionary<Vector2Int, Chunk>[] Levels = new Dictionary<Vector2Int, Chunk>[9];
    private List<Vector2Int> loadedChunks = new List<Vector2Int>();
    public Biome[] level0_biomes;


    private int chunkSize = 16;
    private float tileheight;
    private int seed;
    public int renderDistance = 1;
    private int temprenderDistance;
    public int CurrentLevel = 0;
    private int templevel = 0;
    public int RenderedObjects;

    public Vector2 chunkPos;
    private Vector2 tempchunkPos;


    //temp
    public GameObject tree;
    public GameObject tileprefab;

    public void SetLevel(int level) 
    { 
        //sets level but currently there is no terrain gen for other levels
        CurrentLevel = level;
    }
    public void RenderDistance(string dist)
    {
        renderDistance = int.Parse(dist);
        ChunkUnloader();
    }
    public void InitWorld()
    {
        tileMap = this;
        seed = Random.Range(100000, 999999);
        gameObject.GetComponent<Map>().seed = seed;
        Debug.Log(seed.ToString());
        InvokeRepeating("LoadWorld", 0, 0.1f);
        for(int i = 0; i < 9; i++)
        {
            Levels[i] = new Dictionary<Vector2Int, Chunk>();
        }
    }
    private void LoadWorld()
    {
        Vector2 pos = GameData.Data.player.transform.position / chunkSize;
        chunkPos = new Vector2((pos.x + pos.y * 2), -(pos.x - pos.y * 2));
        chunkPos = new Vector2(Mathf.CeilToInt(chunkPos.x) - 1, Mathf.CeilToInt(chunkPos.y) - 1);
        if (chunkPos.magnitude != tempchunkPos.magnitude | renderDistance != temprenderDistance)
        {
            tempchunkPos = chunkPos;
            temprenderDistance = renderDistance;
            ChunkLoader();
        }
    }
    private void ChunkLoader() 
    {
        for (int cx = (int)chunkPos.x - renderDistance; cx <= chunkPos.x + renderDistance; cx++)
        {
            for (int cy = (int)chunkPos.y - renderDistance; cy <= chunkPos.y + renderDistance; cy++)
            {
                Vector2Int pos = new Vector2Int(cx, cy);
                //skip if chunk already loaded in
                //load chunk data if chunk has previously been generated
                //created new chunk if neither conditions are met
                if (Levels[CurrentLevel].ContainsKey(pos)) continue;
                loadedChunks.Add(new Vector2Int(cx, cy));
                CreateNewChunk(CurrentLevel, cx, cy);
                ChunkUnloader();

                //currently loads all new chunks in one function call which causes lag at render distances
                //rework to load one by one
            }
        }
    }
    public void ChunkUnloader() 
    {
        List<Vector2Int> unloadQueue = new List<Vector2Int>();
        foreach (Vector2Int pos in loadedChunks)
        {
            if ((pos - chunkPos).magnitude > renderDistance * 1.42f)
            {
                Destroy(Levels[CurrentLevel][pos].Parent);
                Levels[CurrentLevel].Remove(pos);
                unloadQueue.Add(pos);
            }
        }
        foreach (Vector2Int pos in unloadQueue) loadedChunks.Remove(pos);
    }
    public void UnloadLevel()
    {
        List<Vector2Int> unloadQueue = new List<Vector2Int>();
        foreach (Vector2Int pos in loadedChunks)
        {
            Destroy(Levels[templevel][pos].Parent);
            Levels[templevel].Remove(pos);
            unloadQueue.Add(pos);
        }
        templevel = CurrentLevel;
        foreach (Vector2Int pos in unloadQueue) loadedChunks.Remove(pos);
        ChunkLoader();
    }
    private void CreateNewChunk(int level, int cx, int cy)
    {
        Vector2Int pos = new Vector2Int(cx, cy);
        GameObject chunkparent = new GameObject("chunk: " + cx + ", " + cy);
        Chunk newchunk = new Chunk();
        chunkparent.transform.parent = transform;
        newchunk.Parent = chunkparent;
        Levels[CurrentLevel].Add(pos, newchunk);
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                //position
                GameObject newtile;
                float xpos = x + (cx * chunkSize);
                float ypos = y + (cy * chunkSize);
                Vector2 tilepos = new Vector2((xpos - ypos) * 0.5f, ((xpos + ypos) * 0.25f));

                //tile gameobject creation
                newtile = Instantiate(tileprefab, tilepos, Quaternion.identity, chunkparent.transform);
                Tile tiletype = GetTile(xpos, ypos);
                newtile.GetComponent<SpriteRenderer>().sprite = tiletype.tilesprite;
                newtile.name = tiletype.name + "_tile_chunk_" + cx + "_" + cy;
                //newchunk.Tiles.SetValue(tiletype, x, y);
                //newchunk.Tiles[x,y].tile = newtile;
                if (tiletype.HasCollision)
                {
                    newtile.AddComponent<BoxCollider2D>();
                }

                //height gradient
                Color tilecolor = Color.white;
                //tilecolor *= 0.6f + (0.4f * tileheight);

                //temp colour change to denote level difference
                tilecolor.a = 100;
                newtile.GetComponent<SpriteRenderer>().color = tilecolor;
            }
        }
    }
    public void GenerateObject()
    {
        //, Vector2Int pos, Transform tile, Transform chunk


        //tileheight = (noise - 0.25f) / 0.35f;
        //float treechance = Mathf.PerlinNoise((100 * (x + seed)) * frequency, (100 * (y + seed)) * frequency);
        //if (tileheight > treechance + 0.4f)
        //{
        //    //objects need to be in their own chunk dictionary so they can be removed when chunk is unloaded
        //    GameObject newtree = Instantiate(tree, tile.transform.position, Quaternion.identity, chunk);
        //    Levels[CurrentLevel][pos].Objects.Add(newtree);

        //}
    }
    //public Tile GetTileType(int level, float x, float y)
    //{
    //    switch (level)
    //    {
    //        case 0: return GetTile(CurrentLevel, x, y, seed);
    //        //case 1: return TerrainGeneration_Level1.GetTile(x, y, seed);
    //    }
    //    return null;
    //}



    //ideally there should be varying noise gen based on biome or at least level
    //function could take inputs such as varying frequency in biome settings or noise/amplitude
    public float GetNoise(float x, float y)
    {
        float frequency = 0.008f;
        float noise = Mathf.PerlinNoise((x + seed) * frequency, (y + seed) * frequency)
        + 0.5f * Mathf.PerlinNoise((2 * (x + seed)) * frequency, (2 * (y + seed)) * frequency)
        + 0.25f * Mathf.PerlinNoise((4 * (x + seed)) * frequency, (4 * (y + seed)) * frequency);
        noise = noise / (1 + 0.5f + 0.25f);
        noise = Mathf.Pow(noise * 1.5f, 3f);
        return noise;
    }
    public Biome GetBiome(float x, float y)
    {
        //biomes should be stored in a 2d array [,] so different levels can be accessed
        float frequency = 0.002f;
        float m = Mathf.PerlinNoise((x + seed) * frequency, (y + seed) * frequency)
        + 0.5f * Mathf.PerlinNoise((2 * (x + seed)) * frequency, (2 * (y + seed)) * frequency);
        if (m < 0.4) return level0_biomes[2];
        if (m < 0.6) return level0_biomes[1];
        if (m > 0.8) return level0_biomes[0];
        else return level0_biomes[0];
    }
    public Tile GetTile(float X, float Y)
    {
        //restructure code to find biome then noise
        float e = GetNoise(X, Y);
        if (e < 0.2f) return GetBiome(X, Y).Tiles[0];
        else if (e < 0.25f) if (GetBiome(X, Y).Tiles.Length >= 1) return GetBiome(X, Y).Tiles[1]; else return GetBiome(X, Y).Tiles[0];
        else if (e < 0.6f) if (GetBiome(X, Y).Tiles.Length >= 2) return GetBiome(X, Y).Tiles[2]; else return GetBiome(X, Y).Tiles[0];
        else if (e < 0.8f) if (GetBiome(X, Y).Tiles.Length >= 3) return GetBiome(X, Y).Tiles[3]; else return GetBiome(X, Y).Tiles[0];
        else if (e < 0.95f) if (GetBiome(X, Y).Tiles.Length >= 4) return GetBiome(X, Y).Tiles[4]; else return GetBiome(X, Y).Tiles[0];
        else if (e > 0.95f) if (GetBiome(X, Y).Tiles.Length >= 5) return GetBiome(X, Y).Tiles[5]; else return GetBiome(X, Y).Tiles[0];
        else return GetBiome(X, Y).Tiles[0];

    }
}