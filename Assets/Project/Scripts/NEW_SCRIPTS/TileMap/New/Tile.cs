using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tile/Base")]
public class Tile : ScriptableObject
{
    public string ID;
    public Sprite tilesprite;
    public Color mapColour;
    public bool HasCollision = false;

    //function that can be overriden if tiles have functionality
    public virtual void UpdateTile() { }
}
