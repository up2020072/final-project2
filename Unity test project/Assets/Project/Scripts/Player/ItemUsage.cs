using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUsage : MonoBehaviour
{
    public Animator animator;
    public Vector2 MovementDirection;
    public Vector2 ArrowDirection;
    public Vector2 MousePos;
    public Vector2 PlayerPosition;
    public float Scrolling;
    public float selectedhotbarslot;
    public bool LeftClick;
    public bool LeftClickLong;
    public float LeftClickTimer;
    public bool RightClick;
    public bool RightClickLong;
    public float RightClickTimer;
    public bool Cooldown;
    public int MaxAmmo;
    public int CurrentAmmo;
    public float MaxMana;
    public float CurrentMana;
    public LayerMask EnemyLayers;
    private void Start() 
    {
        Cooldown = false;
        MenuData.Data.SelectedHotbarSlot = 0;
    }
    void Update()
    {
        PlayerInput();
        Aim();
        CheckLongInput();
        if(Cooldown == false)
        {
            itemUsage();
        }
    }

    public void PlayerInput()
    {
        selectedhotbarslot = MenuData.Data.SelectedHotbarSlot;
        Scrolling = -Input.mouseScrollDelta.y;
        if(Scrolling != 0)
        {
            if(Scrolling == 1 & selectedhotbarslot == 9)
            {
                MenuData.Data.SelectedHotbarSlot = -1;
            }
            if(Scrolling == -1 & selectedhotbarslot == 0)
            {
                MenuData.Data.SelectedHotbarSlot = 9;
            }
            else
            {
                MenuData.Data.SelectedHotbarSlot += Scrolling;
            }
        }
        LeftClick = Input.GetKeyDown(KeyCode.Mouse0);
        LeftClickLong = Input.GetKey(KeyCode.Mouse0);
        RightClick = Input.GetKeyDown(KeyCode.Mouse1);
        RightClickLong = Input.GetKey(KeyCode.Mouse1);  
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            MenuData.Data.SelectedHotbarSlot = 0;
        }  
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            MenuData.Data.SelectedHotbarSlot = 1;
        } 
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            MenuData.Data.SelectedHotbarSlot = 2;
        } 
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            MenuData.Data.SelectedHotbarSlot = 3;
        } 
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            MenuData.Data.SelectedHotbarSlot = 4;
        } 
        if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            MenuData.Data.SelectedHotbarSlot = 5;
        } 
        if(Input.GetKeyDown(KeyCode.Alpha7))
        {
            MenuData.Data.SelectedHotbarSlot = 6;
        } 
        if(Input.GetKeyDown(KeyCode.Alpha8))
        {
            MenuData.Data.SelectedHotbarSlot = 7;
        } 
        if(Input.GetKeyDown(KeyCode.Alpha9))
        {
            MenuData.Data.SelectedHotbarSlot = 8;
        } 
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            MenuData.Data.SelectedHotbarSlot = 9;
        }   
        MovementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        MovementDirection.Normalize();
        MenuData.Data.UIManager.GetComponent<InventoryManager>().UpdateItemSlot();
    }
    public void CheckLongInput()
    {
        if(LeftClickLong)
        {
            LeftClickTimer += Time.deltaTime;
        }
        if(LeftClickLong == false)
        {
            LeftClickTimer = 0;
        }
        if(RightClickLong)
        {
            RightClickTimer += Time.deltaTime;
        }
        if(RightClickLong == false)
        {
            RightClickTimer = 0;
        }
    }
    public void ArrowPickup(int ammo)
    {
        CurrentAmmo += ammo;
    }

    public void ItemCooldown()
    {
        Cooldown = false; 
    }
    public void Aim()
    {
        PlayerPosition = transform.position;
        MousePos = GameData.Data.MousePos;
        ArrowDirection = new Vector2(MousePos.x-PlayerPosition.x, MousePos.y-PlayerPosition.y);
        animator.SetFloat("MouseDirectionX",ArrowDirection.x);
        animator.SetFloat("MouseDirectionY", ArrowDirection.y);
        animator.SetFloat("Horizontal", MovementDirection.x);
        animator.SetFloat("Vertical", MovementDirection.y);
    }
    public void itemUsage()
    {
        if(MenuData.Data.SelectedItem.type == Item.Type.Default)
        {
            if(LeftClick)
            {
                Debug.Log("item attack");;
            }
        }
        if(MenuData.Data.SelectedItem.type == Item.Type.Weapon)
        {
            if(LeftClick)
            {
                WeaponUsage();
            }
            if(LeftClickTimer > MenuData.Data.SelectedItem.WeaponUseTime)
            {
                WeaponUsage();
                LeftClickTimer = 0;
            }
        }
        if(MenuData.Data.SelectedItem.type == Item.Type.Consumable)
        {
            if(LeftClick)
            {
                return;
            }

            if(RightClick)
            {
                return;
            }
        }
        if(MenuData.Data.SelectedItem.type == Item.Type.Ammo)
        {
            return;
        }
    }
    public void WeaponUsage()
    {
        if(MenuData.Data.SelectedItem.weapontype == Item.WeaponType.Sword)
        {
            animator.SetTrigger("Attack");
            animator.SetFloat("WeaponType", MenuData.Data.SelectedItem.ItemID);
            Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(transform.position, MenuData.Data.SelectedItem.Range, EnemyLayers);
            foreach (Collider2D enemy in HitEnemies)
            {
                enemy.GetComponent<Health>().DamageEnemy(MenuData.Data.SelectedItem.Damage, MenuData.Data.SelectedItem.WeaponUseTime, MenuData.Data.SelectedItem.DamageType, MenuData.Data.SelectedItem.Knockback);
                Cooldown = true;
                Invoke("CameraShake", MenuData.Data.SelectedItem.WeaponUseTime);
                Invoke("ItemCooldown", MenuData.Data.SelectedItem.CooldownTime);
            }
        }
        if(MenuData.Data.SelectedItem.weapontype == Item.WeaponType.MagicSword)
        {
            return;
        }

        if(MenuData.Data.SelectedItem.weapontype == Item.WeaponType.Bow)
        {
            return;
        }

        if(MenuData.Data.SelectedItem.weapontype == Item.WeaponType.Spell)
        {
            return;
        }
    }
    public void CameraShake()
    {
        GameData.Data.EnemyHit = true;
        GameData.Data.MeleeShock = MenuData.Data.SelectedItem.Knockback;
    }
}

