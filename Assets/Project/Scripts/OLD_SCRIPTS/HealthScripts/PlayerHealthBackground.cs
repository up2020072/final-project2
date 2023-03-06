using UnityEngine;

public class PlayerHealthBackground : MonoBehaviour
{
    public Vector2 localscale;
    public GameObject Character;
    public Vector2 Scale;
    public float MaxHealth;
    public float HealthConstant;

    void Start()
    {
        MaxHealth = Character.GetComponent<PlayerHealth>().MaxHealth;
        HealthConstant = Character.GetComponent<PlayerHealth>().HealthConstant;
    }


    void Update()
    {
        Scale = new Vector2(1 / (MaxHealth / HealthConstant), 1f);
        transform.localScale = Scale;
    }
}
