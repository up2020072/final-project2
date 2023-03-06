using UnityEngine;


[CreateAssetMenu(menuName = "Biome/Default")]
public class Biome : ScriptableObject
{
    public Tile[] Tiles;
    public string ID;
    public Biome()
    {
        ID = "default_biome";
    }

}

