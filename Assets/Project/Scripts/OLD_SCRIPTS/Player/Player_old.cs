using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_old : MonoBehaviour
{
    public Animator bodyanimator;
    public Animator leganimator;
    public Vector2 movement;
    private Rigidbody2D rb2D;
    public bool LeftClick;
    public bool LeftClickLong;
    public bool RightClick;
    public bool RightClickLong;
    public Vector2 FacingDirection;
    public GameObject PlayerSprite;
    private List<string> animationClips = new List<string>();

    private HealthBar Health;

    public void Start()
    {
        //healthbar testing
        Health = gameObject.AddComponent<HealthBar>();
        //Health.InitHealthBar(30, 30, HealthBar.HealthType.Normal, gameObject);
        //InvokeRepeating("HealTest", 0, 1);

        rb2D = GetComponent<Rigidbody2D>();
        for (int i = 0; i < bodyanimator.runtimeAnimatorController.animationClips.Length - 7; i++)
        {
            animationClips.Add(bodyanimator.runtimeAnimatorController.animationClips[i].name);
        }
    }
    private void Update()
    {
        if (GameData.Data.InventoryOpen == false && GameData.Data.SelectedSlot != null && GameData.Data.SelectedSlot.item is Interactable)
        {
            PlayerInputs();
            ItemUsage();
        }
    }
    private void HealTest()
    {
        Health.Heal(1);
    }
    private void FixedUpdate()
    {
        Movement();
        Aim();
    }
    private void Movement()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        leganimator.SetFloat("Horizontal", movement.x);
        leganimator.SetFloat("Vertical", movement.y);
        leganimator.SetFloat("Movement", movement.magnitude);

        bodyanimator.SetFloat("Horizontal", movement.x);
        bodyanimator.SetFloat("Vertical", movement.y);
        bodyanimator.SetFloat("Movement", movement.magnitude);
        rb2D.MovePosition(rb2D.position + movement * 2.5f * Time.deltaTime);
    }
    public void Aim()
    {
        FacingDirection = new Vector2(GameData.Data.MousePos.x - GameData.Data.player.transform.position.x, GameData.Data.MousePos.y - GameData.Data.player.transform.position.y);
        if (movement.x > 0)
        {
            PlayerSprite.transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            PlayerSprite.transform.localScale = new Vector2(1, 1);
        }
    }
    private void PlayerInputs()
    {
        LeftClick = Input.GetKeyDown(KeyCode.Mouse0);
        RightClick = Input.GetKeyDown(KeyCode.Mouse1);
        LeftClickLong = Input.GetKey(KeyCode.Mouse0);
        RightClickLong = Input.GetKey(KeyCode.Mouse1);
        if (LeftClickLong == false && LeftClick == false && RightClickLong == false && RightClick == false)
        {
            GameData.Data.animator.SetBool("Attack", false);
        }
    }
    private void ItemUsage()
    {
        if(GameData.Data.ItemCooldown == false && (LeftClick || LeftClickLong))
        {

            bodyanimator.SetBool("Attack", true);
            //Debug.Log((GameData.Data.SelectedSlot.item as Interactable).AnimationKey);
            //Debug.Log(animationClips.FindIndex(x => x == ((GameData.Data.SelectedSlot.item as Interactable).AnimationKey)));
            bodyanimator.SetFloat("WeaponAnimation",(float)animationClips.FindIndex(x => x == (GameData.Data.SelectedSlot.item as Interactable).AnimationKey));
        }
    }
}