//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerCombat : MonoBehaviour {

//    public Animator animator;
//    public Vector2 EnemyDirection;
//    public Vector2 PlayerPosition;
//    public Vector2 EnemyPosition;
//    public Vector2 MovementDirection;
//    public Vector2 ArrowDirection;
//    public Vector2 MousePos;

//    public bool Firing;
//    public bool Attacking;
//    public bool FiringLong;
//    public bool MeleeCooldown = false;
//    public bool RangedCooldown = false;

//    public float ProjectileSpeed = 5.0f;
//    public float CrosshairRange = 1.0f;
//    public float AttackRange = 0.5f;
//    public float MeleeWeaponUseTime;
//    public float MeleeCooldownTime;
//    public float LightningCharge;

//    public float Timer;
//    public GameObject ProjectilePrefab;
//    public GameObject crosshair;

//    public Transform AttackPoint;

//    public LayerMask EnemyLayers;

//    public Rigidbody2D EnemyRB;

//    public float Hypotenuse;
//    public Vector2 ArrowPos;
//    public float Angle;
//    public float Adjacent;
//    public float Opposite;
//    public float ArrowPosX;
//    public float ArrowPosY;
//    public float OriginalAngle;

//    [Space]
//    [Header("Positions")]
//    public float MeleeWeaponDamage;
//    public int MeleeDamageType;
//    public int MeleeWeaponType;
//    public int RangedWeaponType;
//    public int MaxAmmo;
//    public int CurrentAmmo;
//    public float MaxMana;
//    public float CurrentMana;
//    public float Knockback;
//    public float KnockbackReduction;

//    void Start()
//    {
//        CurrentAmmo = MaxAmmo;
//        CurrentMana = MaxMana;
//    }

//    void Update() {
//        {
//            Timer += Time.deltaTime;
//            PlayerInput();
//            Aim();
//            if (MeleeCooldown == false)
//            {
//                MeleeAttack();
//            }
//            if (RangedCooldown == false)
//            {
//                RangedAttack();
//            }
//            OriginalAngle = (Mathf.Atan2(ArrowDirection.y, ArrowDirection.x));

//        }
//    }
//    public void ArrowPickup(int ammo)
//    {
//        CurrentAmmo += ammo;
//    }
//    void PlayerInput()
//    {
//        MeleeWeaponType = GameData.Data.MeleeWeaponType;
//        MeleeWeaponDamage = GameData.Data.MeleeDamage;
//        MeleeWeaponUseTime = GameData.Data.MeleeWeaponUseTime;
//        MeleeCooldownTime = GameData.Data.MeleeCooldownTime;
//        MeleeDamageType = GameData.Data.MeleeDamageType;

//        Attacking = Input.GetKeyUp(KeyCode.Mouse0);
//        Firing = Input.GetKeyDown(KeyCode.Mouse1);
//        FiringLong = Input.GetKey(KeyCode.Mouse1);
//        MovementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
//        MovementDirection.Normalize();
//    }
//    void CoolDown()
//    {
//        MeleeCooldown = false;
//        RangedCooldown = false;
//    }
//    void Aim()
//    {
//        //finds the location of the mouse on screen
//        PlayerPosition = transform.position;
//        MousePos = GameData.Data.MousePos;
//        ArrowDirection = new Vector2(MousePos.x-PlayerPosition.x, MousePos.y-PlayerPosition.y);
//        animator.SetFloat("MouseDirectionX",ArrowDirection.x);
//        animator.SetFloat("MouseDirectionY", ArrowDirection.y);
//        animator.SetFloat("Horizontal", MovementDirection.x);
//        animator.SetFloat("Vertical", MovementDirection.y);
//    }
    
//    void MeleeAttack()
//    {
//        if (Attacking & !Firing)
//        {
//            GameData.Data.WeaponNum = GameData.Data.MeleeWeaponType;
//            animator.SetTrigger("Attack");
//            Knockback = 0.75f;
//            KnockbackReduction = 0.5f;
//            animator.SetFloat("WeaponType", MeleeWeaponType);
//            Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);
//            foreach (Collider2D enemy in HitEnemies)
//            {
//                Knockback += KnockbackReduction;
//                KnockbackReduction = KnockbackReduction / 2;
//                enemy.GetComponent<Health>().DamageEnemy(MeleeWeaponDamage, MeleeWeaponUseTime, MeleeDamageType, Knockback);
//                MeleeCooldown = true;
//                Invoke("CameraShake",MeleeWeaponUseTime);
//                Invoke("CoolDown", MeleeCooldownTime);
//            }
//        }
//    }

//    void RangedAttack()
//    {
//        if (Firing & !Attacking)
//        {
//            GameData.Data.RegenerateMana = false;
//            animator.SetTrigger("Attack");
//            RangedWeaponType = GameData.Data.RangedWeaponType;
//            GameData.Data.WeaponNum = GameData.Data.RangedWeaponType + 4;
//            if (GameData.Data.RangedWeaponType == 0)
//            {
//                if (CurrentAmmo > 0)
//                {
//                    animator.SetFloat("WeaponType", GameData.Data.WeaponNum);
//                    ArrowDirection.x += Random.Range(-0.2f, 0.2f);
//                    ArrowDirection.y += Random.Range(-0.2f, 0.2f);
//                    GameObject Arrow = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
//                    Arrow.GetComponent<Rigidbody2D>().velocity = ArrowDirection.normalized * ProjectileSpeed * 2;
//                    Arrow.transform.Rotate(0, 0, Mathf.Atan2(ArrowDirection.y, ArrowDirection.x) * Mathf.Rad2Deg);
//                    Destroy(Arrow, 10.0f);
//                    RangedCooldown = true;
//                    Invoke("CoolDown", 0.5f);
//                    CurrentAmmo -= 1;
//                }       
//            }
//            if (GameData.Data.RangedWeaponType == 2)
//            {
//                if (CurrentAmmo - 3 >= 0)
//                {
//                    animator.SetFloat("WeaponType", GameData.Data.WeaponNum);
//                    for (int i = 0; i < 3; i++)
//                    {
//                        ArrowDirection.Normalize();
//                        if (i == 1)
//                        {
//                            ArrowDirection.x += Random.Range(0.1f, 0.3f);
//                            ArrowDirection.y += Random.Range(0.1f, 0.3f);
//                        }
//                        if (i == 2)
//                        {
//                            ArrowDirection.x += Random.Range(-0.3f, -0.1f);
//                            ArrowDirection.y += Random.Range(-0.3f, -0.1f);
//                        }
//                        GameObject Arrow = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
//                        Arrow.GetComponent<Rigidbody2D>().velocity = ArrowDirection.normalized * ProjectileSpeed;
//                        Arrow.transform.Rotate(0, 0, Mathf.Atan2(ArrowDirection.y, ArrowDirection.x) * Mathf.Rad2Deg);
//                        ArrowDirection = new Vector2(MousePos.x - PlayerPosition.x, MousePos.y - PlayerPosition.y);
//                        Destroy(Arrow, 10.0f);

//                    }
//                    RangedCooldown = true;
//                    Invoke("CoolDown", 0.5f);
//                    CurrentAmmo -= 3;
//                }
//            }
//        }
//        if (FiringLong & !Attacking)
//        {
//            RangedWeaponType = GameData.Data.RangedWeaponType;
//            GameData.Data.WeaponNum = GameData.Data.RangedWeaponType + 4;

//            if (GameData.Data.RangedWeaponType == 1)
//            {
//                float FireSpellCost = 2;
//                if (CurrentMana - FireSpellCost > 0)
//                {
//                    bool FireActive = GameData.Data.FireActive;
//                    if (FireActive == false)
//                    {
//                        GameData.Data.FireActive = true;
//                        animator.SetFloat("WeaponType", GameData.Data.WeaponNum);
//                        Invoke("FireSpell", 0.9f);
//                        CurrentMana -= FireSpellCost;
//                    }
//                }
//                if (CurrentMana - FireSpellCost < 0)
//                {
//                    CurrentMana = 0;
//                }
//            }
//            if (GameData.Data.RangedWeaponType == 3)
//            {
//                float LightningSpellCost = 0.05f;
//                if (CurrentMana - LightningSpellCost > 0)
//                {
//                    bool LightningActive = GameData.Data.LightningActive;
//                    if (LightningActive==false)
//                    {
//                        GameData.Data.LightningActive = true;
//                        animator.SetFloat("WeaponType", GameData.Data.WeaponNum);
//                        Invoke("LightningSpell",0.75f);
//                    }
//                    animator.SetFloat("LightningSpellActive", 1f);
//                    GameData.Data.LightningCharge += Time.deltaTime;
//                    LightningCharge = GameData.Data.LightningCharge;
//                    CurrentMana -= LightningSpellCost;
//                }
//                if (CurrentMana - LightningSpellCost < 0)
//                {
//                    CurrentMana = 0;
//                }
//            }
//        }
//        if (FiringLong==false)
//        {
//            animator.SetFloat("LightningSpellActive", 0f);
//            GameData.Data.FireActive = false;
//            }
//        if (Firing==false)
//        {
//            GameData.Data.RegenerateMana = true;
        
//        }
//    }

//    public void FireSpell()
//    {
//        GameObject Fireball = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
//        Fireball.GetComponent<Rigidbody2D>().velocity = ArrowDirection.normalized * ProjectileSpeed / 2;
//        Fireball.transform.Rotate(0, 0, Mathf.Atan2(ArrowDirection.y, ArrowDirection.x) * Mathf.Rad2Deg);
//        Fireball.transform.localScale = new Vector2(0.5f, 0.5f);
//        Destroy(Fireball, 10.0f);
//    }

//    public void LightningSpell()
//    {
//        GameObject Lightning = Instantiate(ProjectilePrefab, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
//        Lightning.GetComponent<Rigidbody2D>().velocity = ArrowDirection.normalized * ProjectileSpeed * 0.5f;

//    }

//    public void CameraShake()
//    {
//        GameData.Data.EnemyHit = true;
//    }

//    private void OnDrawGizmosSelected()
//    {
//        if (AttackPoint == null)
//            return;
//        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
//    }
//}
