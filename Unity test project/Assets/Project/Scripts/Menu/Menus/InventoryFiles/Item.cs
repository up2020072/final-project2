using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Space]
    [Header("DisplayInfo")]
    public string ItemName;
    public int Damage;
    public string Description;
    public enum Speed { None, Sluggish, Slow, Average, Fast, Supersonic }
    public Speed speed = Speed.None;

    [Space]
    [Header("BackEndInfo")]

    public int ItemID;
    public int StackSize;
    public int Rarity;
    public int SellPrice;
    public enum Type {Default, Weapon, Consumable, Ammo}
    public Type type = Type.Default;
    public enum WeaponType {Sword, MagicSword, Bow, Spell}
    public WeaponType weapontype = WeaponType.Sword;
    public Sprite Sprite;
    public int DamageType; 
    public float Knockback;
    public float Range;
    public float WeaponUseTime;
    public float CooldownTime;
    public float Shake;
}
