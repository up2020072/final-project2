using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Data;
    public Animator animator;
    public GameObject player;
    public Vector2 ArrowDirection;
    public float CurrentHealth;
    public bool ItemCooldown;
    public bool RegenerateMana;
    public float CurrentMana;
    public float MaxMana;

    public float Timer;

    public GameObject UIManager;
    public GameObject ToolTip;
    public GameObject EntitySpawn;
    public int SelectedSlotNum;
    public InventorySlot SelectedSlot;
    public List<Color> RarityColours;
    public bool HoldingItem;
    public bool InventoryOpen;
    public bool PauseOpen;
    public bool TextOpen;
    public bool GetHit;
    public Vector2 MousePos;
    public List<Items> ItemList = new List<Items>();
    public List<Entity> EntityList = new List<Entity>();

    void Awake()
    {
        Data = this; SelectedSlot = UIManager.GetComponent<InventoryManager>().InventorySlots[0];
        ChangeHotbarSlot(0);
        Object[] AllItems = Resources.LoadAll("Items", typeof(Items));
        foreach (Items item in AllItems)
        {
            ItemList.Add(item);
        }
        Object[] AllEntities = Resources.LoadAll("Entities", typeof(Entity));
        foreach (Entity entity in AllEntities)
        {
            EntityList.Add(entity);
        }
    }
    public void ChangeHotbarSlot(int slot)
    {
        animator.SetTrigger("ChangeWeapon");
        SelectedSlot.OnDeselect();
        SelectedSlotNum = slot;
        SelectedSlot = UIManager.GetComponent<InventoryManager>().InventorySlots[SelectedSlotNum];
        SelectedSlot.OnSelect();
    }
}
