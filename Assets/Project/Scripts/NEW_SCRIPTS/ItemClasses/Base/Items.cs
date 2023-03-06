using UnityEngine;

[CreateAssetMenu(menuName = "Item/Default")]
public class Items : ScriptableObject
{
    public string ID;
    public int StackSize = 1;
    public string ItemName;
    public string Description;
    public Sprite Sprite;
    public enum Rarity { Common, Uncommon, Rare, UltraRare, Legendary, Mythic }
    public Rarity rarity = Rarity.Common;
    public Items()
    {
        this.ID = "item";
        this.ItemName = "Item";
        this.Description = "Blank Description";
        this.Sprite = null;
        this.StackSize = 1;
    }
    public void Init(string id, string itemname, string description, int stacksize)
    {
        this.ID = id;
        this.ItemName = itemname;
        this.Description = description;
        this.StackSize = stacksize;
    }
    public static Items CreateInstance(string ID, string Name, string Description, int Stacksize)
    {
        var iteminstance = ScriptableObject.CreateInstance<Items>();
        iteminstance.Init(ID, Name, Description, Stacksize);
        return iteminstance;
    }
    public virtual void UseItem(GameObject origin, LayerMask targets) { }
}

public class Interactable : Items
{
    public string AnimationKey;
    public Interactable()
    {
        this.AnimationKey = "player_attack_default";
    }
}

public class Weapon : Interactable
{
    public enum Speed { None = -1, Sluggish, Slow, Average, Fast, Supersonic }
    public Speed speed = Speed.None;
    public enum DamageType { Default, Piercing, Magic }
    public DamageType damagetype = DamageType.Default;
    public float damage;

    public Weapon()
    {
        this.damage = 1;
    }
}
public class Consumable : Interactable
{
    public float Cooldown;
    public Consumable()
    {
        this.Cooldown = 1;
    }
}

public class Tool : Interactable
{

}
