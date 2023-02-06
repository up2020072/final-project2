using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Items item;
    public GameObject sprite;
    public Sprite normalsprite;
    public Sprite selectedsprite;
    public GameObject ToolTip;
    public GameObject UIManager;
    public GameObject DragSprite;
    public int ItemStackSize;
    public Text ItemStack;
    public int SlotNumber;

    public void Awake()
    {
        UIManager = GameData.Data.UIManager.gameObject;
        ToolTip = GameData.Data.ToolTip;
        DragSprite = UIManager.GetComponent<InventoryManager>().DragSprite;
    }
    public void UpdateItemSlot()
    {
        if (item !=null)
        {
            sprite.GetComponent<Image>().sprite = item.Sprite;
            if (ItemStackSize>1)
            {
                ItemStack.gameObject.SetActive(true);
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
        if (item != null)
        {
            sprite.SetActive(false);
            ItemStack.gameObject.SetActive(false);
            ToolTip.SetActive(false);
            GameData.Data.HoldingItem = true;
            DragSprite.SetActive(true);
            if (ItemStackSize > 1)
            {
                DragSprite.GetComponentInChildren<Text>().text = ItemStackSize.ToString();
            }
            if (ItemStackSize <= 1)
            {
                DragSprite.GetComponentInChildren<Text>().text = null;
            }
            DragSprite.GetComponent<Image>().sprite = item.Sprite;
            Vector2 relativemousepos = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);
            DragSprite.transform.localPosition = relativemousepos * (1920f/Screen.width);
            UIManager.GetComponent<InventoryManager>().StartSlotPos = SlotNumber;
            UIManager.GetComponent<InventoryManager>().StartSlotStackSize = ItemStackSize;
        }
        else
        {
            return;
        }
    }
    public void OnDrop()
    {
        GameData.Data.HoldingItem = false;
        DragSprite.SetActive(false);
        UIManager.GetComponent<InventoryManager>().EndSlotPos = SlotNumber;
        UIManager.GetComponent<InventoryManager>().EndSlotStackSize = ItemStackSize;
        UIManager.GetComponent<InventoryManager>().Invoke("SwapItem",0);
        Invoke("EnableToolTipOnSelect", 0);
        UpdateItemSlot();
    }
    public void EnableToolTipOnSelect()
    {
        if (item != null && SlotNumber > 9)
        {
            ToolTip.SetActive(true);
            ToolTip.transform.position = transform.position;
            ToolTip.GetComponent<ToolTip>().Invoke("EnableToolTip",0);
            ToolTip.GetComponent<ToolTip>().tooltipitem = item;
        }
        if (item != null && SlotNumber <= 9)
        {
            ToolTip.SetActive(true);
            ToolTip.transform.position = new Vector3(transform.position.x, transform.position.y+200);
            ToolTip.GetComponent<ToolTip>().Invoke("EnableToolTip", 0);
            ToolTip.GetComponent<ToolTip>().tooltipitem = item;
        }
    }
    public void DisableToolTip()
    {
        ToolTip.SetActive(false);
    }
    public void OnSelect()
    {
        gameObject.GetComponent<Image>().sprite = selectedsprite;
    }
    public void OnDeselect()
    {
        gameObject.GetComponent<Image>().sprite = normalsprite;
    }
}
