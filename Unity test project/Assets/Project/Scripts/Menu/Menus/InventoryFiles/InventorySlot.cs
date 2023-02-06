using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item;
    public GameObject sprite;
    public GameObject ToolTip;
    public GameObject UIManager;
    public GameObject DragSprite;
    public int ItemStackSize;
    public Text ItemStack;
    public int SlotNumber;

    private void Start()
    {
        ItemStackSize = 1;
    }
    public void UpdateItemSlot()
    {
        if (item !=null)
        {
            sprite.GetComponent<Image>().sprite = item.Sprite;
            if (ItemStackSize>1)
            {
                ItemStack.text = ItemStackSize.ToString();
            }
            if (ItemStackSize <= 1)
            {
                ItemStack.text = null;
            }
            sprite.SetActive(true);
        }
        else
        {
            sprite.SetActive(false);
            ItemStack.text = null;
        }
    }
    public void OnDrag()
    {
        DragSprite.SetActive(true);
        ToolTip.SetActive(false);
        DragSprite.GetComponent<Image>().sprite = item.Sprite;
        DragSprite.transform.localPosition = GameData.Data.ArrowDirection * 172;
        UIManager.GetComponent<InventoryManager>().StartSlotPos = SlotNumber;
        UIManager.GetComponent<InventoryManager>().StartSlotStackSize = ItemStackSize;
    }
    public void OnDrop()
    {
        DragSprite.SetActive(false);
        UIManager.GetComponent<InventoryManager>().EndSlotPos = SlotNumber;
        UIManager.GetComponent<InventoryManager>().EndSlotStackSize = ItemStackSize;
        UIManager.GetComponent<InventoryManager>().Invoke("SwapItem",0);
        EnableToolTipOnDrop();
    }
    public void Hover()
    {
        if (item != null)
        {
            ToolTip.SetActive(true);
            UIManager.GetComponent<InventoryManager>().ItemPos = SlotNumber;
            ToolTip.GetComponent<ToolTip>().itemname = item.ItemName;
            ToolTip.GetComponent<ToolTip>().itemdescription = item.Description;
            ToolTip.GetComponent<ToolTip>().ItemPrice = item.SellPrice;
            ToolTip.GetComponent<ToolTip>().damagetext = item.Damage + " Damage";
            ToolTip.GetComponent<ToolTip>().speedtext = (item.speed).ToString();
            ToolTip.GetComponent<ToolTip>().ItemRarity = item.Rarity;

            if (item.SellPrice>0)
            {
                ToolTip.GetComponent<ToolTip>().ForSale = true;
            }
            if (item.SellPrice == 0)
            {
                ToolTip.GetComponent<ToolTip>().ForSale = false;
            }
            if (item.type == Item.Type.Consumable)
            {
                ToolTip.GetComponent<ToolTip>().Consumable = true;
            }
            if (item.type != Item.Type.Consumable)
            {
                ToolTip.GetComponent<ToolTip>().Consumable = false;
            }
            ToolTip.transform.position = transform.position;
        }
    }
    public void EnableToolTipOnDrop()
    {
        ToolTip.SetActive(true);
        UIManager.GetComponent<InventoryManager>().ItemPos = SlotNumber;
        int InitalPos = UIManager.GetComponent<InventoryManager>().StartSlotPos;
        int EndSlotPos = UIManager.GetComponent<InventoryManager>().EndSlotPos;
        float ItemSellPrice = UIManager.GetComponent<InventoryManager>().ItemList[InitalPos].SellPrice;
        ToolTip.GetComponent<ToolTip>().itemname = UIManager.GetComponent<InventoryManager>().ItemList[InitalPos].ItemName;
        ToolTip.GetComponent<ToolTip>().itemdescription = UIManager.GetComponent<InventoryManager>().ItemList[InitalPos].Description;
        ToolTip.GetComponent<ToolTip>().ItemPrice = UIManager.GetComponent<InventoryManager>().ItemList[InitalPos].SellPrice;
        ToolTip.GetComponent<ToolTip>().damagetext = UIManager.GetComponent<InventoryManager>().ItemList[InitalPos].Damage + " Damage";
        ToolTip.GetComponent<ToolTip>().speedtext = UIManager.GetComponent<InventoryManager>().ItemList[InitalPos].speed.ToString();
        ToolTip.GetComponent<ToolTip>().ItemRarity = UIManager.GetComponent<InventoryManager>().ItemList[InitalPos].Rarity;
        if (ItemSellPrice > 0)
        {
            ToolTip.GetComponent<ToolTip>().ForSale = true;
        }
        if (ItemSellPrice == 0)
        {
            ToolTip.GetComponent<ToolTip>().ForSale = false;
        }
        if (UIManager.GetComponent<InventoryManager>().ItemList[InitalPos].type == Item.Type.Consumable)
        {
            ToolTip.GetComponent<ToolTip>().Consumable = true;
        }
        if (UIManager.GetComponent<InventoryManager>().ItemList[InitalPos].type != Item.Type.Consumable)
        {
            ToolTip.GetComponent<ToolTip>().Consumable = false;
        }
        ToolTip.transform.position = UIManager.GetComponent<InventoryManager>().InventorySlots[EndSlotPos].transform.position;
    }
    public void SelectHotbarSlot()
    {
        if(MenuData.Data.SelectedHotbarSlot == SlotNumber-30)
        {
            gameObject.GetComponent<Image>().sprite = MenuData.Data.SelectedFrame;
            if(item != null)
            {
                MenuData.Data.SelectedItem = item;
                Debug.Log("current slot selected item is" + item + "with ID " + item.ItemID);
            }
            if(item == null)
            {
                MenuData.Data.SelectedItem = null;
            }
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = MenuData.Data.NormalFrame;
        }
    }
}
