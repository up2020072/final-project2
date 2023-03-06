//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using System.IO;

//public class ImprovedInventory : MonoBehaviour
//{
//    public int EquipedMeleeWeapon = 0;
//    public int EquipedRangedWeapon = 0;
//    public int CurrentMoney;
//    public int WeaponNum;

//    [Space]
//    [Header("Settings")]

//    public Button Button;
//    public GameObject ButtonFrame;
//    public GameObject ButtonText;
//    public GameObject CurrentSelectedMeleeFrame;
//    public GameObject CurrentSelectedRangedFrame;
//    public GameObject StartingMeleeWeapon;
//    public GameObject StartingRangedWeapon;
//    public bool SlotPurchased = false;

//    [Space]
//    [Header("RangedSettings")]
//    public int Filler;

//    [Space]
//    [Header("OtherSettings")]
//    public Image MeleeWeapon;
//    public Image RangedWeapon;

//    public Sprite InactiveFrame;
//    public Sprite FrameNotUnlocked;
//    public Sprite PurchasedFrame;
//    public Sprite SelectedFrame;

//    public Sprite LongswordImage;
//    public Sprite BroadSwordImage;
//    public Sprite WarhammerImage;
//    public Sprite AxeImage;
//    public Sprite LongbowImage;
//    public Sprite FireSpellBookImage;
//    public Sprite CrossBowImage;
//    public Sprite LightningSpellBookImage;

//    public Image ItemImage;

//    public void Start()
//    {
//        if (StartingMeleeWeapon != null)
//        {
//            GameData.Data.CurrentSelectedMeleeFrame = StartingMeleeWeapon;
//            GameData.Data.CurrentSelectedRangedFrame = StartingRangedWeapon;
//        }
//    }
//    public void Update()
//    {
//        CurrentMoney = GameData.Data.CurrentMoney;
//        ItemImage.GetComponent<Image>().sprite = GameData.Data.ItemImage;
//        WeaponNum = int.Parse(Button.name.Substring(Button.name.Length - 1));
//        List<int> WeaponSelect = new List<int>() {0, 10, 50, 1000 ,0 ,75, 25, 100};
//        CurrentSelectedMeleeFrame = GameData.Data.CurrentSelectedMeleeFrame;
//        CurrentSelectedRangedFrame = GameData.Data.CurrentSelectedRangedFrame;
//        GameData.Data.MeleeWeaponType = int.Parse(CurrentSelectedMeleeFrame.name.Substring(CurrentSelectedMeleeFrame.name.Length - 1))-1;
//        GameData.Data.RangedWeaponType = int.Parse(CurrentSelectedRangedFrame.name.Substring(CurrentSelectedRangedFrame.name.Length - 1))-5;
//        if (CurrentMoney >= WeaponSelect[WeaponNum - 1] & SlotPurchased == false)
//        {
//            Button.interactable = true;
//            ButtonFrame.GetComponent<Image>().sprite = FrameNotUnlocked;
//        }
//        if (CurrentMoney < WeaponSelect[WeaponNum - 1] & SlotPurchased == false)
//        {
//            Button.interactable = false;
//            ButtonFrame.GetComponent<Image>().sprite = InactiveFrame;
//        }
//        if (WeaponNum<=4)
//        {
//            if (ButtonFrame != CurrentSelectedMeleeFrame & SlotPurchased == true)
//            {
//                ButtonFrame.GetComponent<Image>().sprite = PurchasedFrame;
//            }
//            if (ButtonFrame != CurrentSelectedMeleeFrame & SlotPurchased == true)
//            {
//                ButtonFrame.GetComponent<Image>().sprite = PurchasedFrame;
//            }
//        }
//        if (WeaponNum > 4)
//        {
//            if (ButtonFrame != CurrentSelectedRangedFrame & SlotPurchased == true)
//            {
//                ButtonFrame.GetComponent<Image>().sprite = PurchasedFrame;
//            }
//            if (ButtonFrame != CurrentSelectedRangedFrame & SlotPurchased == true)
//            {
//                ButtonFrame.GetComponent<Image>().sprite = PurchasedFrame;
//            }
//        }
//    }
//    public void InventorySlot()
//    {
//        Debug.Log(gameObject.name);
//    }
//    public void MeleeInventorySlot()
//    {
//        if (SlotPurchased == true)
//        {
//            MeleeWeapon.GetComponent<Image>().sprite = Button.GetComponent<Image>().sprite;
//            ButtonFrame.GetComponent<Image>().sprite = SelectedFrame;
//            GameData.Data.CurrentSelectedMeleeFrame = ButtonFrame;
//        }
//        else
//        {
//            List<int> MeleeWeaponSelect = new List<int>() { 25, 50, 1000 };
//            if (CurrentMoney >= MeleeWeaponSelect[WeaponNum - 2])
//            {
//                GameData.Data.CurrentMoney -= MeleeWeaponSelect[WeaponNum - 2];
//                SlotPurchased = true;
//                ButtonFrame.GetComponent<Image>().sprite = PurchasedFrame;
//                ButtonText.SetActive(false);
//            }
//        }
//    }

//    public void RangedInventorySlot()
//    {
//        if (SlotPurchased == true)
//        {
//            RangedWeapon.GetComponent<Image>().sprite = Button.GetComponent<Image>().sprite;
//            ButtonFrame.GetComponent<Image>().sprite = SelectedFrame;
//            GameData.Data.CurrentSelectedRangedFrame = ButtonFrame;
//        }
//        else
//        {
//            List<int> RangedWeaponSelect = new List<int>() { 75, 25, 100 };
//            if (CurrentMoney >= RangedWeaponSelect[WeaponNum-6])
//            {
//                GameData.Data.CurrentMoney -= RangedWeaponSelect[WeaponNum-6];
//                SlotPurchased = true;
//                ButtonFrame.GetComponent<Image>().sprite = PurchasedFrame;
//                ButtonText.SetActive(false);
//            }
//        }
//    }
//    public void TestHover()
//    {
//        var source = new StreamReader(Application.dataPath + "/Item" + WeaponNum + ".txt");
//        var fileContents = source.ReadToEnd();
//        if (fileContents.Length==0)
//        {
//            Debug.Log("item description couldnt be found");
//        }

//        else
//        {
//            GameData.Data.ItemDescription = fileContents;
//            source.Close();
//        }
//        GameData.Data.WeaponNum = WeaponNum-1;
//    }
//}

