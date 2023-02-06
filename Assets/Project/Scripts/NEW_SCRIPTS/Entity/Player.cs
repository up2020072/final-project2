using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public Animator WeaponAnimator;
    private List<string> animationClips = new List<string>();
    public void Awake()
    {
        Health = gameObject.AddComponent<HealthBar>();
        Health.InitHealthBar(50, 50, HealthBar.HealthType.Normal, 10, gameObject);
        Init();
        for (int i = 0; i < WeaponAnimator.runtimeAnimatorController.animationClips.Length - 7; i++)
        {
            animationClips.Add(WeaponAnimator.runtimeAnimatorController.animationClips[i].name);
        }
    }
    public override void Controller()
    {
        if (GameData.Data.InventoryOpen == false && GameData.Data.SelectedSlot != null && GameData.Data.SelectedSlot.item is Interactable) MouseInputs();
        MoveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")/2) * Speed;
        Move();
        AnimateWeapon();
    }
    private void MouseInputs()
    {
        bool LeftClick = Input.GetKeyDown(KeyCode.Mouse0);
        bool RightClick = Input.GetKeyDown(KeyCode.Mouse1);
        bool LeftClickLong = Input.GetKey(KeyCode.Mouse0);
        bool RightClickLong = Input.GetKey(KeyCode.Mouse1);
        if (LeftClickLong == false && LeftClick == false && RightClickLong == false && RightClick == false) GameData.Data.animator.SetBool("Attack", false);
        if (GameData.Data.ItemCooldown == false && (LeftClick || LeftClickLong))
        {
            WeaponAnimator.SetBool("Attack", true);
            WeaponAnimator.SetFloat("WeaponAnimation", (float)animationClips.FindIndex(x => x == (GameData.Data.SelectedSlot.item as Interactable).AnimationKey));
        }
    }
    private void AnimateWeapon()
    {
        SpriteRenderer renderer = WeaponAnimator.gameObject.GetComponent<SpriteRenderer>();
        WeaponAnimator.SetFloat("Horizontal", MoveDirection.x);
        WeaponAnimator.SetFloat("Vertical", MoveDirection.y);
        WeaponAnimator.SetFloat("Movement", MoveDirection.magnitude);
        if (MoveDirection.x > 0) { renderer.flipX = true; }
        else renderer.flipX = false;
    }
}
