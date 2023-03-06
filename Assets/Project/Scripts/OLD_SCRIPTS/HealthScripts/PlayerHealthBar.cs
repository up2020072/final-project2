using System.Collections;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    public Vector2 localscale;
    public GameObject Character;
    public GameObject HealthBlock;
    public Vector2 Scale;
    public float MaxHealth;
    public float CurrentHealth;
    public float HealthConstant;
    public int Pointer;
    public string Index;
    public IList List;

    void Start()
    {
        if (gameObject.name != "HealthBar")
        {
            int NameLength = HealthBlock.name.Length;
            Index = char.ToString(HealthBlock.name[NameLength - 1]);
            MaxHealth = Character.GetComponent<PlayerHealth>().MaxHealth;
            HealthConstant = Character.GetComponent<PlayerHealth>().HealthConstant;
            List = Character.GetComponent<PlayerHealth>().HealthList;
            Pointer = int.Parse(Index);
        }
    }


    void Update()
    {
        if (gameObject.name != "HealthBar")
        {
            if (List.Count - 1 < Pointer)
            {
                CurrentHealth = 0;
            }
            else
            {
                CurrentHealth = (int)List[Pointer];
            }
            Scale = new Vector2(1 / (MaxHealth / HealthConstant) * CurrentHealth / HealthConstant, 1f);
            transform.localScale = Scale;
        }
    }
}
