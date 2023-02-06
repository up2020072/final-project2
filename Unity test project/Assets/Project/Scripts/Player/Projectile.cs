using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Projectile : MonoBehaviour {
    public Transform AttackPoint;
    private float AttackRange = 0.2f;
    public LayerMask EnemyLayers;
    public LayerMask Wall;
    public LayerMask ArrowEnemyLayers;
    public LayerMask FireballEnemyLayers;
    public LayerMask LightningEnemyLayers;

    public GameObject LightningEmmitter;
    public GameObject FireEmmitter;
    public GameObject ExplosionEmmitter;

    public GameObject ProjectilePrefab;
    public float Damage;
    public Sprite Arrow;
    public Sprite FireBall;
    public Sprite Lightning;
    public float Timer;
    public float WeaponType;
    public float lerp = 0;

    private void Start()
    {
        Damage = GameData.Data.RangedDamage;
        WeaponType = GameData.Data.RangedWeaponType;
        if (WeaponType == 0 | WeaponType == 2)
        {
            EnemyLayers = ArrowEnemyLayers;
            gameObject.GetComponent<SpriteRenderer>().sprite = Arrow;
        }
        if (WeaponType == 1)
        {
            EnemyLayers = FireballEnemyLayers;
            gameObject.GetComponent<SpriteRenderer>().sprite = FireBall;
            FireEmmitter.SetActive(true);
        }
        if (WeaponType == 3)
        {
            EnemyLayers = LightningEnemyLayers;
            gameObject.GetComponent<SpriteRenderer>().sprite = Lightning;
            LightningEmmitter.SetActive(true);
        }
    }


    void Update()
    {
        Timer += Time.deltaTime;
        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);
        if(WeaponType == 3)
        {
            float LightningCharge = GameData.Data.LightningCharge;
            float ScaleConstant = Mathf.Pow(LightningCharge, 1 / 3f) / 2;
            transform.localScale = new Vector2(ScaleConstant, ScaleConstant) ;
            LightningEmmitter.transform.localScale = new Vector2(ScaleConstant, ScaleConstant);
            foreach(Transform Light in LightningEmmitter.transform)
            {
                Light.GetComponent<Light2D>().pointLightOuterRadius = 5f * ScaleConstant;
            }
            Vector3 MousePos = GameData.Data.MousePos;
            lerp += Time.deltaTime/3;
            Vector2 NewPos = Vector2.Lerp(transform.position, MousePos,lerp);
            transform.position = new Vector2(NewPos.x, NewPos.y);
            
        }
        foreach (Collider2D enemy in HitEnemies)
        {
            if (WeaponType == 0 | WeaponType == 2)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Projectile>().enabled = false;
                enemy.GetComponent<Health>().DamageEnemy(Damage, 0, 0, 0);
            }
            if (WeaponType == 1)
            {
                if (Timer>=0.1)
                {
                    enemy.GetComponent<Health>().DamageEnemy(Damage, 0, 1, 0);
                    Timer = 0;
                }
            }
            if (WeaponType == 3)
            {
                enemy.GetComponent<Health>().HealthTotal();
                if (enemy.GetComponent<Health>().CurrentHealthTotal < (GameData.Data.LightningChargeDamage))
                {
                    float ChargeDamage = GameData.Data.LightningChargeDamage;
                    Debug.Log(ChargeDamage);
                    float EnemyHealth = enemy.GetComponent<Health>().CurrentHealthTotal;
                    enemy.GetComponent<Health>().DamageEnemy(enemy.GetComponent<Health>().CurrentHealthTotal, 0, 1, 0);
                    GameData.Data.LightningCharge = (ChargeDamage - EnemyHealth) / Damage;

                }
                if (enemy.GetComponent<Health>().CurrentHealthTotal > Damage * GameData.Data.LightningCharge)
                {
                    GetComponent<BoxCollider2D>().enabled = false;
                    GetComponent<SpriteRenderer>().enabled = false;
                    GetComponent<Projectile>().enabled = false;
                    enemy.GetComponent<Health>().DamageEnemy(Damage * GameData.Data.LightningCharge, 0, 1, 0);
                    GameData.Data.LightningCharge = 0;
                    GameData.Data.LightningActive = false;
                    Destroy(gameObject);
                }
            }
        }
        Collider2D[] WallReflect = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange / 2, Wall);
        foreach (Collider2D wall in WallReflect)
        {
            if (WeaponType == 1)
            {
                GameData.Data.FireActive = false;
                ExplosionEmmitter.SetActive(true);
            }
            if (WeaponType==3)
            {
                GameData.Data.LightningActive = false;
                GameData.Data.LightningCharge = 0;
                LightningEmmitter.SetActive(false);
            }
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmosSelected()
        {
            if (AttackPoint == null)
                return;
            Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
        }
    }
