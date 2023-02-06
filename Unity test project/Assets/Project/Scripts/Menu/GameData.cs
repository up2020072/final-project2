using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public Animator animator;
    public Animator LegAnimator;
    public static GameData Data;
    public GameObject player;
    public Vector2 ArrowDirection;
    public float CurrentHealth;
    public float CurrentAmmo;
    public int CurrentMoney;
    public GameObject InventoryManager;

    public bool RegenerateMana;
    public float CurrentMana;
    public float MaxMana;
    public float PlayerSpeed;

    public float Timer;
    public Vector2 MousePos;

    public float LightningCharge;
    public float LightningChargeDamage;
    public bool LightningActive;
    public bool FireActive;

    public int MoneyIncrease;
    public float lerp = 0;
    public float MoneyToAdd=0;
    public float StartMoney;
    public int StartingMoney;

    [Space]
    [Header("Experience")]
    public float CurrentExperience;
    public float ExperienceRequired;
    public int Level;

    [Space]
    [Header("Melee Weapon Data")]
    public int MeleeWeaponType;

    public float MeleeDamage;
    public float MeleeWeaponUseTime;
    public float MeleeCooldownTime;
    public float MeleeShock;
    public int MeleeDamageType;
    public bool EnemyHit;

    [Space]
    [Header("Ranged Weapon Data")]
    public int RangedWeaponType;

    public float RangedDamage;
    public float RangedWeaponUseTime;
    public int RangedDamageType;

    public GameObject CurrentSelectedMeleeFrame;
    public GameObject CurrentSelectedRangedFrame;
    public string ItemDescription;
    public Sprite ItemImage;
    public int WeaponNum;

    public Sprite LongswordImage;
    public Sprite BroadSwordImage;
    public Sprite WarhammerImage;
    public Sprite AxeImage;
    public Sprite LongbowImage;
    public Sprite FireSpellBookImage;
    public Sprite CrossBowImage;
    public Sprite LightningSpellBookImage;


    void Awake()
    {
        Data = this;
        DontDestroyOnLoad(gameObject);
        InvokeRepeating("RegenMana",0f,0.05f);
        RegenerateMana = true;
        PlayerSpeed = 3;
        AddMoney(StartingMoney);
        CurrentExperience = 0;
    }
    public void RegenMana()
    {
        if (RegenerateMana == true)
        {
            if (CurrentMana <= MaxMana)
            {
                player.GetComponent<PlayerCombat>().CurrentMana += 0.05f;
            }
        }
    }
    public void AddMoney(int Money)
    {
        lerp = 0;
        MoneyToAdd += Money;
        StartMoney = CurrentMoney;
    }
    public void Update()
    {
        lerp += Time.deltaTime;
        if (CurrentMoney < StartMoney + MoneyToAdd)
        {
            CurrentMoney = (int)Mathf.Lerp(StartMoney, (StartMoney + MoneyToAdd), lerp);
        }
        if (CurrentMoney == StartMoney + MoneyToAdd)
        {
            MoneyToAdd = 0;
        }
        Timer += Time.deltaTime;
        CurrentHealth = player.GetComponent<PlayerHealth>().CurrentHealthTotal;
        CurrentAmmo = player.GetComponent<PlayerCombat>().CurrentAmmo;
        CurrentMana = player.GetComponent<PlayerCombat>().CurrentMana;
        MaxMana= player.GetComponent<PlayerCombat>().MaxMana;
        ArrowDirection = player.GetComponent<PlayerCombat>().ArrowDirection;
        if (player.GetComponent<PlayerCombat>().Attacking == true)
        {
            Timer = 0;
            float x = MeleeWeaponType + 1;
            PlayerSpeed = 2.5f / x;
            LegAnimator.speed = 1 / x;
        }
        if (Timer > MeleeCooldownTime*0.8f  & player.GetComponent<PlayerCombat>().Firing == true)
        {
            PlayerSpeed = 3;
            LegAnimator.speed = 1;
        }
        if (Timer > RangedWeaponUseTime)
        {
            PlayerSpeed = 3;
            LegAnimator.speed = 1;
        }

        List<float> MeleeDamageList = new List<float>() { 3, 8, 15, 1 };
        List<float> MeleeUseTimeList = new List<float>() { 0.5f, 1f, 0.5f, 0.1f };
        List<float> MeleeCooldownList = new List<float>() { 0.66f, 1.0f, 1.55f, 0.1f };
        List<float> MeleeShockList = new List<float>() { 0.1f, 0.3f, 3f, 0.3f };
        List<int> MeleeDamageTypeList = new List<int>() { 0, 0, 0, 1 };

        List<float> RangedDamageList = new List<float>() { 5, 5, 2, 5 };
        List<float> RangedUseTimeList = new List<float>() { 1f, 1f, 1f, 0.8f };
        List<int> RangedDamageTypeList = new List<int>() { 0, 1, 0, 1 };

        List<Sprite> Images = new List<Sprite>()
        { LongswordImage,BroadSwordImage,WarhammerImage,AxeImage,
        LongbowImage, FireSpellBookImage, CrossBowImage, LightningSpellBookImage};

        MeleeDamage = MeleeDamageList[MeleeWeaponType];
        MeleeWeaponUseTime = MeleeUseTimeList[MeleeWeaponType];
        MeleeCooldownTime = MeleeCooldownList[MeleeWeaponType];
        MeleeDamageType = MeleeDamageTypeList[MeleeWeaponType];

        RangedDamage = RangedDamageList[RangedWeaponType];
        RangedWeaponUseTime = RangedUseTimeList[RangedWeaponType];
        RangedDamageType = RangedDamageTypeList[RangedWeaponType];

        LightningChargeDamage = LightningCharge * RangedDamage;
        ExperienceRequired = 100 + (100 * Level);
        ItemImage = Images[WeaponNum];
    }
}
