using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private List<HealthBlock> BlockList = new List<HealthBlock>();
    public enum HealthType { Normal, Armour, Shield }
    public HealthType Healthtype;
    private GameObject HealthBlockPrefab;
    private Vector2 InitialBarPos;
    private Vector2 BarPos;
    public float TotalHealth;
    public float CurrentHealth;
    private int BlockAmount;
    private float BlockConstant;
    public void InitHealthBar(float totalhealth, float currenthealth, HealthType healthtype, int blockamount, GameObject entity)
    {
        BlockAmount = blockamount;
        HealthBlockPrefab = Resources.Load<GameObject>("Prefabs/HealthBlock");
        InitialBarPos = entity.transform.position + new Vector3(-0.325f, 0.3f, 0);
        BarPos = InitialBarPos;
        TotalHealth = totalhealth;
        CurrentHealth = currenthealth;
        BlockConstant = totalhealth/BlockAmount;
        for (int i = 0; i < BlockAmount; i++)
        {
            GameObject healthblockbackground = Instantiate(HealthBlockPrefab, BarPos, Quaternion.identity, entity.transform);
            healthblockbackground.transform.localScale = new Vector2(1f / BlockAmount,0.75f);
            healthblockbackground.GetComponent<SpriteRenderer>().color = Color.black;
            healthblockbackground.GetComponent<SpriteRenderer>().sortingOrder = -1;
            GameObject healthblock = Instantiate(HealthBlockPrefab, BarPos, Quaternion.identity, entity.transform);
            HealthBlock block = new HealthBlock(BlockConstant, BlockConstant, (int)healthtype , blockamount, healthblock);
            BlockList.Add(block);
            BarPos.x += (1f / BlockAmount) / 1.5f;
        }
        UpdateHealth();
    }
    public void TakeDamage(float damage, int damagetype, float knockback, bool modifydamage = true)
    {
        for (int i = BlockAmount-1; i > -1; i--)
        {
            if (BlockList[i].BlockCurrentHealth != 0)
            {
                float truedamage;
                if (modifydamage) truedamage = ModifyDamage(damage, damagetype, BlockList[i].BlockType);
                else truedamage = damage;
                if (truedamage == 0) Debug.Log(gameObject.name + " Immune");
                if (BlockList[i].BlockCurrentHealth - truedamage >= 0)
                {
                    BlockList[i].BlockCurrentHealth -= truedamage;
                    UpdateHealth();
                    return;
                }
                if (BlockList[i].BlockCurrentHealth - truedamage < 0)
                {
                    float remainder = truedamage - BlockList[i].BlockCurrentHealth;
                    BlockList[i].BlockCurrentHealth = 0;
                    UpdateHealth();
                    TakeDamage(remainder, damagetype, 0, false);
                    return;
                }
            }
        }
        return;
    }
    private float ModifyDamage(float damage, int damagetype, int healthtype)
    {
        switch (healthtype) 
        { 
            case 0:
                if (damagetype == 1) damage += 3;
                break;
            case 1:
                if (damagetype == 0) damage -= 3;
                if (damagetype == 1) damage += 1;
                if (damagetype == 2) damage *= 2;
                break;
            case 2:
                if (damagetype != 2) damage = 0;
                break;
        }
        return (damage > 0 ? damage : 0);
    }
    public void Heal(float heal)
    {
        for (int i = 0; i < BlockAmount; i++)
        {
            if(BlockList[i].BlockCurrentHealth < BlockList[i].BlockMaxHealth)
            {
                if(BlockList[i].BlockCurrentHealth + heal <= BlockList[i].BlockMaxHealth)
                {
                    BlockList[i].BlockCurrentHealth += heal;
                    UpdateHealth();
                    return;
                }
                if (BlockList[i].BlockCurrentHealth + heal > BlockList[i].BlockMaxHealth)
                {
                    float remainder = heal - BlockList[i].BlockMaxHealth;
                    BlockList[i].BlockCurrentHealth = BlockList[i].BlockMaxHealth;
                    UpdateHealth();
                    Heal(remainder);
                    return;
                }
            }
        }
    }
    public void UpdateHealth()
    {
        CurrentHealth = 0;
        for (int i = 0; i < BlockAmount; i++)
        {
            CurrentHealth += BlockList[i].BlockCurrentHealth;
            BlockList[i].UpdateBlock();
        }
        if (CurrentHealth <= 0) Die();
    }
    public void Die()
    {
        if (gameObject.name != "Player")
        {
            GameObject itempickups = Resources.Load<GameObject>("Prefabs/ItemPrefab");
            GameObject itemdrops = Instantiate(itempickups, gameObject.transform.position - new Vector3(0, 0.2f, 0), Quaternion.identity);
            itemdrops.GetComponent<ItemPickup>().item = GameData.Data.ItemList[Random.Range(0,GameData.Data.ItemList.Count)];
            gameObject.GetComponent<Animator>().SetTrigger("Dead");
        }
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject, 1.0f);
    }
}
