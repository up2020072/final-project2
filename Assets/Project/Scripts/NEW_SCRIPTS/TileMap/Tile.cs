using UnityEngine;

[CreateAssetMenu(menuName = "Tile/Base")]
public class Tile : ScriptableObject
{
    public string ID;
    public string tileSprite;
    public TilePosition tilePos;
    public int tileState;
    public string mapColour;
    public bool HasCollision = false;

    //function that can be overriden if tiles have functionality
    public virtual void UpdateTile() { }
}
