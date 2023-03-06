////public class TerrainGeneration_Level0
//{
//    public Biome[] level0_biomes;
//    public static float GetNoise(float x, float y, float seed) 
//    {
//        float frequency = 0.008f;
//        float noise = Mathf.PerlinNoise((x + seed) * frequency, (y + seed) * frequency)
//        + 0.5f * Mathf.PerlinNoise((2 * (x + seed)) * frequency, (2 * (y + seed)) * frequency)
//        + 0.25f * Mathf.PerlinNoise((4 * (x + seed)) * frequency, (4 * (y + seed)) * frequency);
//        noise = noise / (1 + 0.5f + 0.25f);
//        noise = Mathf.Pow(noise * 1.5f, 3f);
//        return noise;
//    }
//    public Biome GetBiome(float x, float y, float seed)
//    {
//        float frequency = 0.008f;
//        float m = Mathf.PerlinNoise((x + seed) * frequency, (y + seed) * frequency);
//        if (m < 0.1) return level0_biomes[0];
//        if (m < 0.4) return level0_biomes[1];
//        if (m < 0.7) return level0_biomes[2];
//        else return level0_biomes[0];
//    }
//    public Tile GetTile(float X, float Y, float Seed) 
//    {
//        float e = GetNoise(X, Y, Seed);
//        if (e < 0.2f) return GetBiome(X, Y, Seed).Tiles[0];
//        else if (e < 0.25f)return GetBiome(X, Y, Seed).Tiles[1];
//        else if (e < 0.6f) return GetBiome(X, Y, Seed).Tiles[2];
//        else if (e < 0.8f) return GetBiome(X, Y, Seed).Tiles[3];
//        else if (e < 0.95f) return GetBiome(X, Y, Seed).Tiles[4];
//        else if (e > 0.95f) return GetBiome(X, Y, Seed).Tiles[5];
//        else return GetBiome(X, Y, Seed).Tiles[0];

//    }
//}

public static class TerrainGeneration_Level1
{

}
public static class TerrainGeneration_Level2 { }
public static class TerrainGeneration_Level3 { }
public static class TerrainGeneration_Level4 { }
public static class TerrainGeneration_Level5 { }
public static class TerrainGeneration_Level6 { }
public static class TerrainGeneration_Level7 { }
public static class TerrainGeneration_Level8 { }
