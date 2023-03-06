
//public class ItemUsage : MonoBehaviour
//{
//    public Animator animator;
//    public Vector2 MovementDirection;
//    public Vector2 ArrowDirection;
//    public Vector2 MousePos;
//    public Vector2 PlayerPosition;
//    public int Scrolling;
//    public float selectedhotbarslot;
//    public bool LeftClick;
//    public bool LeftClickLong;
//    public bool RightClick;
//    public bool RightClickLong;
//    public LayerMask EnemyLayers;
//    public InventorySlot SelectedItemSlot;
//    public GameObject PlayerSprite;
//    public Collider2D[] HitEnemies;
//    private void Start()
//    {
//        EnemyLayers = LayerMask.GetMask("Enemy", "EnemyGhost");
//    }
//    void Update()
//    {
//        SelectedItemSlot = GameData.Data.UIManager.GetComponent<InventoryManager>().InventorySlots[GameData.Data.SelectedSlotNum];
//        if(SelectedItemSlot.item != null)
//        {
//            HitEnemies = Physics2D.OverlapCircleAll(transform.position, SelectedItemSlot.item.Range, EnemyLayers);
//        }
//        if (GameData.Data.InventoryOpen == false && SelectedItemSlot.item != null)
//        {
//            PlayerInput();
//            CheckLongInput();
//            itemUsage();
//        }
//        Aim();
//    }

//    public void PlayerInput()
//    {
//        LeftClick = Input.GetKeyDown(KeyCode.Mouse0);
//        LeftClickLong = Input.GetKey(KeyCode.Mouse0);
//        RightClick = Input.GetKeyDown(KeyCode.Mouse1);
//        RightClickLong = Input.GetKey(KeyCode.Mouse1);
//        MovementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
//        MovementDirection.Normalize();
//    }
//    public void CheckLongInput()
//    {
//        if (LeftClickLong == false && LeftClick == false && RightClickLong == false && RightClick == false)
//        {
//            animator.SetBool("Attack", false);
//        }
//    }
//    //needs to be part of the ranged weapon class
//    public void Aim()
//    {
//        PlayerPosition = transform.position;
//        MousePos = GameData.Data.MousePos;
//        ArrowDirection = new Vector2(MousePos.x - PlayerPosition.x, MousePos.y - PlayerPosition.y);
//        if (ArrowDirection.x > 0)
//        {
//            PlayerSprite.transform.localScale = new Vector2(-1,1);
//        }
//        else
//        {
//            PlayerSprite.transform.localScale = new Vector2(1, 1);
//        }
//        animator.SetFloat("MouseDirectionX", ArrowDirection.x);
//        animator.SetFloat("MouseDirectionX", ArrowDirection.x);
//        animator.SetFloat("MouseDirectionY", ArrowDirection.y);
//        animator.SetFloat("Horizontal", MovementDirection.x);
//        animator.SetFloat("Vertical", MovementDirection.y);
//        animator.SetFloat("Movement", MovementDirection.magnitude);
//    }
//    public void itemUsage()
//    {
//        if(SelectedItemSlot.item != null && GameData.Data.ItemCooldown == false)
//        {
//            if(SelectedItemSlot.item.type == Item.Type.Default)
//            {
//                if (LeftClick)
//                {
//                    return;
//                }
//            }
//            if (SelectedItemSlot.item.type == Item.Type.Weapon)
//            {

//                animator.SetFloat("WeaponType", (int)SelectedItemSlot.item.animation);
//                if (LeftClick || LeftClickLong)
//                {
//                    animator.SetBool("Attack", true);
//                    Invoke("WeaponUsage", SelectedItemSlot.item.WeaponUseTime);
//                }
//            }
//            if (SelectedItemSlot.item.type == Item.Type.Consumable)
//            {
//                if (LeftClick)
//                {
//                    return;
//                }

//                if (RightClick)
//                {
//                    return;
//                }
//            }
//        }
//    }
//    public void WeaponUsage()
//    {
//        if (SelectedItemSlot.item.weapontype == Item.WeaponType.Melee)
//        {
//            foreach (Collider2D enemy in HitEnemies)
//            {
//                enemy.GetComponent<Health>().DamageEnemy(SelectedItemSlot.item.Damage, 0, (int)SelectedItemSlot.item.damagetype, (int)SelectedItemSlot.item.knockback);
//            }
//        }
//        if (SelectedItemSlot.item.weapontype == Item.WeaponType.Ranged)
//        {
//            return;
//        }

//        if (SelectedItemSlot.item.weapontype == Item.WeaponType.Spell)
//        {
//            return;
//        }
//    }
//    public void CameraShake()
//    {
//    }
//}

