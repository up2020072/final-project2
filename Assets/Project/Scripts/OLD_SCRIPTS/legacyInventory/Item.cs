using UnityEngine;

//need to reference
public class Item : ScriptableObject
{
    [Space]
    [Header("TooltipInfo")]
    public string ItemName;
    public string Description;
    public Sprite Sprite;
    public int Damage;
    public enum Speed { None, Sluggish, Slow, Average, Fast, Supersonic }
    public Speed speed = Speed.None;
    [Space]
    [Header("BackEndInfo")]
    public float WeaponUseTime;
    public enum Type { Default, Consumable, Weapon, Tool }
    public Type type = Type.Default;
    public enum WeaponType { None, Melee, Ranged, Spell }
    public WeaponType weapontype = WeaponType.None;
    public enum DamageType { Default, Magic, Piercing }
    public DamageType damagetype = DamageType.Default;
    public enum Rarity { Common, Uncommon, Rare, UltraRare, Legendary, Mythic }
    public Rarity rarity = Rarity.Common;
    public enum Knockback { None, Low, Average, High }
    public Knockback knockback = Knockback.None;
    public enum Animation { Sword, BigSword, Axe, Hammer, Bow }
    public Animation animation = Animation.Sword;
    public int StackSize = 1;
    public int SellPrice;
    public float Range;
}