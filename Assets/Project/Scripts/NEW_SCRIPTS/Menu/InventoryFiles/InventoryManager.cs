using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<InventorySlot> InventorySlots = new List<InventorySlot>();
    public GameObject DragSprite;
    public GameObject ItemPrefab;
    public GameObject InventorySlotPrefab;
    public Transform InventoryPos;
    public int ItemPos;
    public int StartSlotPos;
    public int EndSlotPos;
    public int StartSlotStackSize;
    public int EndSlotStackSize;
    public int InventorySlotAmount;
    //try go through all scripts and make values private if possible

    
    public void Awake()
    {
        for (int i = 0; i < InventorySlotAmount; i++)
        {
            //generate slots
            GameObject InventorySlot = Instantiate(InventorySlotPrefab, gameObject.transform.position, Quaternion.identity, InventoryPos);
            InventorySlot.name = "InventorySlot" + (i+10);
            InventorySlots.Add(InventorySlot.GetComponent<InventorySlot>());
            InventorySlots[i+10].SlotNumber = i+10;
        }
    }
    public bool PickupItem(Items item, int stackSize)
    {
        for (int i = 0; i < InventorySlotAmount + 10; i++)
        {
            if (InventorySlots[i].item == item && InventorySlots[i].ItemStackSize + stackSize <= item.StackSize)
            {
                InventorySlots[i].ItemStackSize+=stackSize;
                UpdateItemSlot(i);
                return true;
            }
            if (InventorySlots[i].item == item && InventorySlots[i].ItemStackSize + stackSize > item.StackSize && InventorySlots[i].ItemStackSize != item.StackSize)
            {
                int tempstack = InventorySlots[i].ItemStackSize + stackSize - item.StackSize;
                InventorySlots[i].ItemStackSize = item.StackSize;
                PickupItem(item, tempstack);
                UpdateItemSlot(i);
                return true;
            }
            if (InventorySlots[i].item == null)
            {
                if (stackSize > item.StackSize)
                {
                    InventorySlots[i].item = item;
                    InventorySlots[i].ItemStackSize = item.StackSize;
                    PickupItem(item, stackSize - item.StackSize);
                    UpdateItemSlot(i);
                    return true;
                }
                else
                {
                    InventorySlots[i].item = item;
                    InventorySlots[i].ItemStackSize += stackSize;
                    UpdateItemSlot(i);
                    return true;
                }
            }
        }
        return false;
    }
    public bool RemoveItem(Items item, int amount)
    {
        for (int i = 0; i < InventorySlotAmount + 10; i++)
        {
            if (InventorySlots[i].item == item && InventorySlots[i].ItemStackSize - amount >= 1)
            {
                InventorySlots[i].ItemStackSize -= amount;
                UpdateItemSlot(i);
                return true;
            }
            if (InventorySlots[i].item == item && InventorySlots[i].ItemStackSize - amount == 1)
            {
                InventorySlots[i].item = null;
                UpdateItemSlot(i);
                return true;
            }
            if (InventorySlots[i].item == item && InventorySlots[i].ItemStackSize - amount < 1)
            {
                int remainder = amount - InventorySlots[i].ItemStackSize;
                InventorySlots[i].item = null;
                RemoveItem(item, remainder);
                UpdateItemSlot(i);
                return true;
            }
        }
        return false;
    }
    public void ConsumeItem()
    {
        int i = GameData.Data.SelectedSlotNum;
        InventorySlots[i].ItemStackSize -= 1;
        if(InventorySlots[i].ItemStackSize == 0) InventorySlots[i].item = null;
        UpdateItemSlot(i);
        return;
    }
    public void UpdateItemSlot(int slot)
    {
        InventorySlots[slot].UpdateItemSlot();
    }
    public void SwapItem()
    {
        //if swapping with itself
        if (StartSlotPos == EndSlotPos)
        {
            return;
        }
        Items TempItem = InventorySlots[EndSlotPos].item;
        InventorySlots[EndSlotPos].item = InventorySlots[StartSlotPos].item;
        InventorySlots[StartSlotPos].item = TempItem;
        //if same item type
        if (InventorySlots[StartSlotPos].item == InventorySlots[EndSlotPos].item)
        {
            if (EndSlotStackSize < InventorySlots[EndSlotPos].item.StackSize)
            {
                int MaxSlots = InventorySlots[EndSlotPos].item.StackSize;
                if (StartSlotStackSize + EndSlotStackSize <= MaxSlots)
                {
                    InventorySlots[EndSlotPos].ItemStackSize = StartSlotStackSize + EndSlotStackSize;
                    InventorySlots[StartSlotPos].item = null;
                    InventorySlots[StartSlotPos].ItemStackSize = 0;
                }
                if (StartSlotStackSize + EndSlotStackSize > MaxSlots)
                {
                    int Remainer = (StartSlotStackSize + EndSlotStackSize) - MaxSlots;
                    InventorySlots[StartSlotPos].ItemStackSize = Remainer;
                    InventorySlots[EndSlotPos].ItemStackSize = MaxSlots;
                }
            }
        }
        //if different items
        else
        {
            int TempStackSize = EndSlotStackSize;
            InventorySlots[EndSlotPos].ItemStackSize = StartSlotStackSize;
            InventorySlots[StartSlotPos].ItemStackSize = TempStackSize;
        }
        UpdateItemSlot(EndSlotPos);
        UpdateItemSlot(StartSlotPos);
    }
    public void DropIn()
    {
        GameData.Data.HoldingItem = false;
        DragSprite.SetActive(false);
        UpdateItemSlot(StartSlotPos);
    }
    public void DropOut()
    {
        if (GameData.Data.HoldingItem == true)
        {
            GameObject DroppedItem = Instantiate(ItemPrefab, GameData.Data.MousePos, Quaternion.identity, GameData.Data.player.transform);
            DroppedItem.GetComponent<ItemPickup>().item = InventorySlots[StartSlotPos].item;
            DroppedItem.GetComponent<ItemPickup>().stacksize = InventorySlots[StartSlotPos].ItemStackSize;
            DragSprite.SetActive(false);
            InventorySlots[StartSlotPos].item = null;
            InventorySlots[StartSlotPos].ItemStackSize = 0;
            InventorySlots[StartSlotPos].ToolTip.SetActive(false);
            UpdateItemSlot(StartSlotPos);
            GameData.Data.HoldingItem = false;
        }
        else
        {
            return;
        }
    }
}

