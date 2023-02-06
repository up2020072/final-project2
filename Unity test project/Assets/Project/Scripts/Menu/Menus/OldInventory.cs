using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int EquipedMeleeWeapon = 0;
    public int EquipedRangedWeapon = 0;
    public int CurrentMoney;

    [Space]
    [Header("Melee Settings")]
    public Sprite MeleeSprite1;
    public Sprite MeleeSprite2;
    public Sprite MeleeSprite3;
    public Sprite MeleeSprite4;

    public Button MeleeButton1;
    public Button MeleeButton2;
    public Button MeleeButton3;
    public Button MeleeButton4;

    public GameObject MeleeFrame1;
    public GameObject MeleeFrame2;
    public GameObject MeleeFrame3;
    public GameObject MeleeFrame4;

    public GameObject MeleeText2;
    public GameObject MeleeText3;
    public GameObject MeleeText4;

    public bool MeleeSlot1Purchased = true;
    public bool MeleeSlot2Purchased = false;
    public bool MeleeSlot3Purchased = false;
    public bool MeleeSlot4Purchased = false;

    [Space]
    [Header("RangedSettings")]
    public int Filler;

    [Space]
    [Header("OtherSettings")]
    public Image MeleeWeapon;
    public Image RangedWeapon;

    public Sprite InactiveFrame;
    public Sprite AvailableFrame;
    public Sprite PurchasedFrame;
    public Sprite SelectedFrame;

    void Awake()
    {
        MeleeButton2.interactable = false;
        MeleeButton3.interactable = false;
        MeleeButton4.interactable = false;
        MeleeFrame2.GetComponent<Image>().sprite = InactiveFrame;
        MeleeFrame3.GetComponent<Image>().sprite = InactiveFrame;
        MeleeFrame4.GetComponent<Image>().sprite = InactiveFrame;
    }
    public void Update()
    {
        CurrentMoney = GameData.Data.CurrentMoney;
        if (CurrentMoney>=100 & MeleeSlot2Purchased==false)
        {
            MeleeButton2.interactable = true;
            MeleeFrame2.GetComponent<Image>().sprite = AvailableFrame;
        }
        if (CurrentMoney < 100 & MeleeSlot2Purchased == false)
        {
            MeleeButton2.interactable = false;
            MeleeFrame2.GetComponent<Image>().sprite = InactiveFrame;
        }


        if (CurrentMoney >= 500 & MeleeSlot3Purchased == false)
        {
            MeleeButton3.interactable = true;
            MeleeFrame3.GetComponent<Image>().sprite = AvailableFrame;
        }
        if (CurrentMoney < 500 & MeleeSlot3Purchased == false)
        {
            MeleeButton3.interactable = false;
            MeleeFrame3.GetComponent<Image>().sprite = InactiveFrame;
        }


        if (CurrentMoney >= 800 & MeleeSlot4Purchased == false)
        {
            MeleeButton4.interactable = true;
            MeleeFrame4.GetComponent<Image>().sprite = AvailableFrame;
        }
        if(CurrentMoney < 800 & MeleeSlot4Purchased == false)
        {
            MeleeButton4.interactable = false;
            MeleeFrame4.GetComponent<Image>().sprite = InactiveFrame;
        }
    }
    public void ChangeSlot()
    {
        List<Sprite> WeaponSelect = new List<Sprite>()
        {MeleeSprite1,MeleeSprite2,MeleeSprite3,MeleeSprite4};
        List<GameObject> MeleeFrame = new List<GameObject>()
        {MeleeFrame1,MeleeFrame2,MeleeFrame3,MeleeFrame4};
        MeleeWeapon.GetComponent<Image>().sprite = WeaponSelect[EquipedMeleeWeapon];
        for (int i=0; i<MeleeFrame.Count; i++)
        {
            if (MeleeFrame[i].GetComponent<Image>().sprite == SelectedFrame)
            {
                MeleeFrame[i].GetComponent<Image>().sprite = PurchasedFrame;
            }
        }
        MeleeFrame[EquipedMeleeWeapon].GetComponent<Image>().sprite = SelectedFrame;
        GameData.Data.animator.SetFloat("WeaponType", EquipedMeleeWeapon);

    }
    public void MeleeSlot1()
    {
        if (MeleeSlot1Purchased==true)
        {
            EquipedMeleeWeapon = 0;
            //GameData.Data.Damage = 2;
            ChangeSlot();
        }
    }
    public void MeleeSlot2()
    {
        if (MeleeSlot2Purchased == true)
        {
            EquipedMeleeWeapon = 1;
            //GameData.Data.Damage = 6;
            ChangeSlot();
        }
        else
        {
            if (CurrentMoney>=100)
            {
                GameData.Data.CurrentMoney -= 100;
                MeleeSlot2Purchased = true;
                MeleeFrame2.GetComponent<Image>().sprite = PurchasedFrame;
                MeleeText2.SetActive(false);
            }
        }
    }
    public void MeleeSlot3()
    {
        if (MeleeSlot3Purchased == true)
        {
            EquipedMeleeWeapon = 2;
            //GameData.Data.Damage = 8;
            ChangeSlot();
        }
        else
        {
            if (CurrentMoney >= 500)
            {
                GameData.Data.CurrentMoney -= 500;
                MeleeSlot3Purchased = true;
                MeleeFrame3.GetComponent<Image>().sprite = PurchasedFrame;
                MeleeText3.SetActive(false);
            }
        }
    }
    public void MeleeSlot4()
    {
        EquipedMeleeWeapon = 3;
        ChangeSlot();
    }
}
