using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Item[] ItemList = new Item[40];
    public List<InventorySlot> InventorySlots = new List<InventorySlot>();
    public GameObject DragSprite;
    public int ItemPos;
    public int StartSlotPos;
    public int EndSlotPos;
    public int StartSlotStackSize;
    public int EndSlotStackSize;
    
    public void Awake()
    {
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            InventorySlots[i].SlotNumber = i;

            
        }
    }
    private bool PickupItem(Item item)
    {
        for (int i = 0; i < ItemList.Length; i++)
        {
            if (ItemList[i] == item & item.StackSize == 0)
            {
                return false;
            }
            if (ItemList[i] == item & item.StackSize > 1 & InventorySlots[i].ItemStackSize < item.StackSize)
            {
                InventorySlots[i].ItemStackSize++;
                return true;
            }
            if (ItemList[i] == item & item.StackSize > 1 & InventorySlots[i].ItemStackSize > item.StackSize)
            {
                for (int a = 0; a < ItemList.Length; a++)
                {
                    if (ItemList[i] == null)
                    {
                        ItemList[i] = item;
                        InventorySlots[i].item = item;
                        return true;
                    }
                }
            }
            if (ItemList[i] == null)
            {
                ItemList[i] = item;
                InventorySlots[i].item = item;
                return true;
            }
        }
        return false;
    }
    public void UpdateItemSlot()
    {
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            InventorySlots[i].UpdateItemSlot();
            InventorySlots[i].SelectHotbarSlot();
        }
    }
    public void AddItem(Item item)
    {
        bool HasAdded = PickupItem(item);
        if (HasAdded)
        {
            UpdateItemSlot();
        }
    }

    public void RemoveItem()
    {
        if (InventorySlots[ItemPos].ItemStackSize>1)
        {
            InventorySlots[ItemPos].ItemStackSize -= 1;
            GameData.Data.CurrentMoney += ItemList[ItemPos].SellPrice;
        }
        else
        {
            GameData.Data.CurrentMoney += ItemList[ItemPos].SellPrice;
            ItemList[ItemPos] = null;
            InventorySlots[ItemPos].item = null;
            InventorySlots[ItemPos].ToolTip.SetActive(false);
        }
        UpdateItemSlot();
    }
    public void SwapItem()
    {

        if (StartSlotPos == EndSlotPos)
        {
            DragSprite.SetActive(false);
        }
        
        Item TempItem =  ItemList[EndSlotPos];
        ItemList[EndSlotPos] = ItemList[StartSlotPos];
        InventorySlots[EndSlotPos].item = ItemList[EndSlotPos];
        ItemList[StartSlotPos] = TempItem;
        InventorySlots[StartSlotPos].item = ItemList[StartSlotPos];

        if (InventorySlots[StartSlotPos].item == InventorySlots[EndSlotPos].item)
        {
            if (EndSlotStackSize < InventorySlots[EndSlotPos].item.StackSize)
            {
                int MaxSlots = InventorySlots[EndSlotPos].item.StackSize;
                if (StartSlotStackSize + EndSlotStackSize <= MaxSlots)
                {
                    InventorySlots[EndSlotPos].ItemStackSize = StartSlotStackSize + EndSlotStackSize;
                    InventorySlots[StartSlotPos].ItemStackSize = 0;
                    ItemList[StartSlotPos] = null;
                    InventorySlots[StartSlotPos].item = null;
                }
                if (StartSlotStackSize + EndSlotStackSize > MaxSlots)
                {
                    int Remainer = (StartSlotStackSize + EndSlotStackSize) - MaxSlots;
                    InventorySlots[StartSlotPos].ItemStackSize = Remainer;
                    InventorySlots[EndSlotPos].ItemStackSize = MaxSlots;
                }
            }
        }
        else
        {
            int TempStackSize = EndSlotStackSize;
            InventorySlots[EndSlotPos].ItemStackSize = StartSlotStackSize;
            InventorySlots[StartSlotPos].ItemStackSize = TempStackSize;
        }
        UpdateItemSlot();
    }

    public void DisableDragSprite()
    {
        DragSprite.SetActive(false);
    }
}
