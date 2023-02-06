using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar_old : MonoBehaviour
{
    public Vector2 localscale;
    public GameObject HealthBlock;
    public Vector2 Scale;
    public float MaxHealth;
    public float CurrentHealth;
    public float HealthConstant;
    public int Pointer;
    public string Index;
    public List<float> List;

    void Start()
    {
        int NameLength = HealthBlock.name.Length;
        if (NameLength > 16)
        {
            if (NameLength == 16)
            {
                Index = HealthBlock.name.Substring(16, 1);
            }
            if (NameLength > 16)
            {
                Index = HealthBlock.name.Substring(16, NameLength - 16);
            }
        }
        else
        {
            return;
        }
        Pointer = int.Parse(Index);
        MaxHealth = transform.parent.GetComponent<Health>().MaxHealth;
        HealthConstant = transform.parent.GetComponent<Health>().HealthConstant;
        List = transform.parent.GetComponent<Health>().EnemyHealthList;
    }


    void Update()
    {
        if (HealthBlock.name.Length > 16)
        {
            if (List.Count - 1 < Pointer)
            {
                CurrentHealth = 0;
            }
            else
            {
                CurrentHealth = List[Pointer];
            }

            Scale = new Vector2(1 / (MaxHealth / HealthConstant) * CurrentHealth / HealthConstant, 1f);
            transform.localScale = Scale;
        }
    }
 
}
