using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public int MaxHealth;
    public int CurrentHealth;
    public int CurrentHealthTotal;
    public int HealthConstant;
    public int damage;
    public int CurrentMoney;
    public float HealthSpacing;

    public GameObject DeathMessagePrefab;
    public GameObject HealthBlockPrefab;
    public GameObject HealthBackgroundPrefab;

    public Transform InitialHealthPos;
    public Transform Player;

    public Vector2 NewHealthPos;
    public IList HealthList;


    void Start()
    {
        HealthList = new List<int>();
        NewHealthPos = InitialHealthPos.position;
        HealthSpacing = 1 / ((float)MaxHealth / HealthConstant);
        for (int i = 0; i < MaxHealth/ HealthConstant; i++)
        { 
            HealthList.Add(HealthConstant);
            GameObject Healthblock = Instantiate(HealthBlockPrefab,NewHealthPos,Quaternion.identity, Player);
            GameObject HealthBlockBackground = Instantiate(HealthBackgroundPrefab, NewHealthPos, Quaternion.identity, Player);
            Healthblock.name = "HealthBlock" + i;
            NewHealthPos.x += HealthSpacing/1.5f;
        }
        
    }
    public void Update()
    {
        HealthTotal();
        if (CurrentHealthTotal <= 0)
        {
            Die();
        }
    }
    public void DamagePlayer(int Damage, float Time)
    {
        damage = Damage;
        Invoke("TakeDamage", Time);
       
    }
    void TakeDamage()
    {
       CurrentHealth = (int)HealthList[HealthList.Count-1];
       if (CurrentHealth - damage >= 0)
       {
            CurrentHealth -= damage;
            HealthList.RemoveAt(HealthList.Count - 1);
            HealthList.Add(CurrentHealth);
       }
       else
            if (CurrentHealth - damage < 0)
        {
            int RemainingDamage = damage - CurrentHealth;
            CurrentHealth -= CurrentHealth;
            HealthList.RemoveAt(HealthList.Count - 1);
            HealthList.Add(CurrentHealth);
            HealthList.Remove(0);
            DamagePlayer(RemainingDamage, 0);
        }

       if (CurrentHealthTotal<=0)
       {
           Die();
       }


    }
    public void HealPlayer()
    {
        CurrentHealth = (int)HealthList[HealthList.Count - 1];
        int Heal = 2;
        if (CurrentHealthTotal<MaxHealth)
        { 
            if (CurrentHealth + Heal <= HealthConstant)
            {
                HealthList.RemoveAt(HealthList.Count - 1);
                HealthList.Add(CurrentHealth + Heal);
            }
            if (CurrentHealth + Heal > HealthConstant)
            {
                int RemainingHeal = CurrentHealth + Heal - HealthConstant;
                HealthList.RemoveAt(HealthList.Count - 1);
                HealthList.Add(HealthConstant);
                HealthList.Add(RemainingHeal);
            }
            if (CurrentHealth == HealthConstant)
            {
                HealthList.Add(Heal);
            }
        }
        else
        {
            return;
        }
    }
    void HealthTotal()
    {
        CurrentHealthTotal = 0;
        for (int i = 0; i < HealthList.Count; i++)
        {
            CurrentHealthTotal += (int)HealthList[i];

        }
    }
    void Die()
    {
        GetComponent<Movement>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GameObject DeathMessage = Instantiate(DeathMessagePrefab, transform.position, Quaternion.identity);
        Invoke("Respawn", 3.0f);
    }

    void Respawn()
    {
        SceneManager.LoadScene(1);
        SceneManager.LoadScene(0, LoadSceneMode.Additive);
    }


}
