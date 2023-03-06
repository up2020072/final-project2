using UnityEngine;

public class EnemyHealthBackground : MonoBehaviour
{
    public Vector2 localscale;
    public Vector2 Scale;
    public float MaxHealth;
    public float HealthConstant;

    void Start()
    {
        if (gameObject.name.Length > 16)
        {
            MaxHealth = transform.parent.GetComponent<Health>().MaxHealth;
            HealthConstant = transform.parent.GetComponent<Health>().HealthConstant;
        }
    }


    void Update()
    {
        if (gameObject.name.Length > 16)
        {
            Scale = new Vector2(1 / HealthConstant, 1f);
            transform.localScale = Scale;
        }
    }
}
