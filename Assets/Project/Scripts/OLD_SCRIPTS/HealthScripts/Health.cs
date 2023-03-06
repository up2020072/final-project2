using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb2D;
    public float CurrentHealth;
    public int damageType;
    public float damage;
    public float CurrentHealthTotal;
    public float HealthSpacing;
    public string HealthConstantIndex;
    public int HealthConstantPointer;
    public int NameLength;
    public float knockback = 0f;
    public bool GetHit;

    [Space]
    [Header("Health Settings")]
    public float MaxHealth;
    [Range(1, 9)]
    public int HealthType;
    [Range(1, 3)]
    public int LootRarity;
    public float HealthConstant;


    [Space]
    [Header("HP prefabs")]
    public GameObject HealthBlockPrefab;

    public GameObject Healthblock1;
    public GameObject Healthblock2;
    public GameObject Healthblock3;

    public GameObject ArmourBlock1;
    public GameObject ArmourBlock2;
    public GameObject ArmourBlock3;

    public GameObject ShieldBlock1;
    public GameObject ShieldBlock2;
    public GameObject ShieldBlock3;

    public GameObject HealthBackgroundPrefab;
    public GameObject Enemy;

    [Space]
    [Header("Positions")]
    public Transform InitialHealthPos;
    public Transform Character;
    public Vector2 NewHealthPos;
    public Vector2 PlayerPosition;
    public Vector2 EnemyPosition;
    public Vector2 EnemyDirection;

    public List<float> EnemyHealthList;
    public Rigidbody2D EnemyRB;
    public GameObject LootDrop;
    public Vector2 localscale;
    public float Timer;
    public float StunTimer;

    [Space]
    [SerializeField]
    private Color DamageColour;

    void Start()
    {
        EnemyRB = GetComponent<Rigidbody2D>();
        HealthType -= 1;
        EnemyHealthList = new List<float>();
        List<GameObject> HealthBlockType = new List<GameObject>()
        {Healthblock1, Healthblock2, Healthblock3,
         ArmourBlock1, ArmourBlock2, ArmourBlock3,
         ShieldBlock1, ShieldBlock2, ShieldBlock3,};
        NewHealthPos = InitialHealthPos.position;
        HealthBlockPrefab = HealthBlockType[HealthType] as GameObject;
        NameLength = HealthBlockPrefab.name.Length;
        HealthConstantIndex = char.ToString(HealthBlockPrefab.name[NameLength - 1]);
        HealthConstantPointer = int.Parse(HealthConstantIndex);
        if (HealthConstantPointer == 1)
        {
            HealthConstant = 5;
        }
        if (HealthConstantPointer == 2)
        {
            HealthConstant = 8;
        }
        if (HealthConstantPointer == 3)
        {
            HealthConstant = 15;
        }
        HealthSpacing = (1 / (HealthConstant)) * transform.localScale.x;
        for (int i = 0; i < HealthConstant; i++)
        {
            EnemyHealthList.Add(MaxHealth / HealthConstant);
            GameObject Healthblock = Instantiate(HealthBlockPrefab, NewHealthPos, Quaternion.identity, Character);
            GameObject HealthBlockBackground = Instantiate(HealthBackgroundPrefab, NewHealthPos, Quaternion.identity, Character);
            Healthblock.name = "EnemyHealthBlock" + i;
            NewHealthPos.x += HealthSpacing / 1.5f;
        }

    }
    public void FixedUpdate()
    {
        HealthTotal();
        Timer += Time.deltaTime;
        StunTimer += Time.deltaTime;
        PlayerPosition = GameData.Data.player.transform.position;
        EnemyPosition = transform.position;
        EnemyDirection = new Vector2(PlayerPosition.x - transform.position.x, PlayerPosition.y - transform.position.y);
        if (Timer > 0.2f)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }
    public void DamageEnemy(float Damage, float Time, int DamageType, int Knockback)
    {
        if (Damage > 0)
        {
            damage = Damage;
            GameData.Data.GetHit = true;
            knockback = (int)Knockback / transform.localScale.x;
            StunTimer = 0;
            damageType = (int)DamageType;
            Invoke("EnemyTakeDamage", Time);
        }
        else
        {
            return;
        }
    }
    void EnemyTakeDamage()
    {

        //Normal
        if (HealthType >= 0 & HealthType < 3)
        {
            if (damageType == 0)
            {
                damage -= 0;
            }
            if (damageType == 1)
            {
                damage -= 0;
            }
            if (damageType == 2)
            {
                damage -= 0;
            }
            else
                damage -= 0;

            EnemyRB.AddForce(-EnemyDirection.normalized * knockback / 3, ForceMode2D.Force);
        }
        //Armor (-3 to all physical damage, and x2 magic)
        if (HealthType >= 3 & HealthType < 6)
        {
            if (damageType == 0)
            {
                damage -= 3;
            }
            if (damageType == 1)
            {
                damage *= 2;
            }
            if (damageType == 2)
            {
                damage -= 0;
            }
            else
                damage -= 0;
            EnemyRB.AddForce(-EnemyDirection.normalized * knockback / 10, ForceMode2D.Force);
            Debug.Log("enemy knocked back");
        }
        //Shield (-5 to magic damage, x2 to physical damage, and x5 to piercing)
        if (HealthType >= 6 & HealthType < 9)
        {
            if (damageType == 0)
            {
                damage *= 2;
            }
            if (damageType == 1)
            {
                damage -= 5;
            }
            if (damageType == 2)
            {
                damage *= 5;
            }
            else
                damage -= 0;
            //spectral (immune to physical and piercing, x10 magic damage)
        }
        CurrentHealth = (int)EnemyHealthList[EnemyHealthList.Count - 1];
        Timer = 0;
        gameObject.GetComponent<SpriteRenderer>().color = DamageColour;
        if (CurrentHealth - damage > 0)
        {
            CurrentHealth -= damage;
            EnemyHealthList.RemoveAt(EnemyHealthList.Count - 1);
            EnemyHealthList.Add(CurrentHealth);
        }
        else if (CurrentHealth - damage <= 0 && CurrentHealthTotal - damage > 0)
        {
            float RemainingDamage = damage - CurrentHealth;
            CurrentHealth = 0;
            EnemyHealthList.RemoveAt(EnemyHealthList.Count - 1);
            DamageEnemy(RemainingDamage, 0, damageType, (int)knockback);
        }
        else if (CurrentHealthTotal - damage <= 0)
        {
            //wont remove health bars by doing this
            Die();
        }
        CurrentHealthTotal = 0;
        for (int i = 0; i < EnemyHealthList.Count; i++)
        {

            CurrentHealthTotal += (int)EnemyHealthList[i];

        }
        if (CurrentHealthTotal <= 0)
        {
            Die();
        }
    }
    public void HealthTotal()
    {
        CurrentHealthTotal = 0;
        for (int i = 0; i < EnemyHealthList.Count; i++)
        {

            CurrentHealthTotal += (int)EnemyHealthList[i];

        }
    }
    void HealEnemy()
    {
        Debug.Log("enemy healed");
        CurrentHealth = (int)EnemyHealthList[EnemyHealthList.Count - 1];
        if (CurrentHealth < HealthConstant)
        {
            EnemyHealthList.RemoveAt(EnemyHealthList.Count - 1);
            EnemyHealthList.Add(CurrentHealth + 1);
        }
        if (CurrentHealth == HealthConstant)
        {
            EnemyHealthList.Add(CurrentHealth + 1);

        }
    }

    void Die()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        animator.SetTrigger("Dead");
        Debug.Log(gameObject + " dead");
        //for (int i = 0; i < Random.Range(HealthConstant,HealthConstant*LootRarity*LootRarity); i++)
        //{
        //    List<GameObject> PotentialDrops = new List<GameObject>()
        //    {BronzeCoin, SilverCoin, GoldCoin ,Hearts, Arrow};
        //    LootDrop = PotentialDrops[Random.Range(LootRarity-1,5)] as GameObject;
        //    Vector3 position = new Vector2(transform.position.x + Random.Range(-0.1f, 0.1f), transform.position.y + Random.Range(-0.1f, 0.1f));
        //    GameObject lootdrop = Instantiate(LootDrop, position, Quaternion.identity);
        //}
        Destroy(Enemy, 1.0f);


    }

}
