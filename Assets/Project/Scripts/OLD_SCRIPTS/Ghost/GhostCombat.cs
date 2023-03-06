using UnityEngine;

public class GhostCombat : MonoBehaviour
{
    public GameObject Character;
    public Vector2 EnemyDirection;
    public Vector2 PlayerPosition;
    public Vector2 EnemyPosition;
    public float OutOfRange;
    public float SearchRange;
    public Animator animator;
    public int GhostDamage;
    public Vector2 localscale;

    void Start()
    {
        InvokeRepeating("Attack", 0.0f, 1f);
    }

    public void Update()
    {
        EnemyPosition = transform.position;
        PlayerPosition = Character.transform.position;
        EnemyDirection = new Vector2(PlayerPosition.x - transform.position.x, PlayerPosition.y - transform.position.y);
        OutOfRange = Mathf.Sqrt(EnemyDirection.x * EnemyDirection.x + EnemyDirection.y * EnemyDirection.y);
    }

    void Attack()
    {
        if (GetComponent<GhostCombat>().enabled == true)
        {
            if (OutOfRange < SearchRange * Mathf.Pow(transform.localScale.x, 2))
            {
                Character.GetComponent<PlayerHealth>().DamagePlayer(GhostDamage, 0.3f);
            }
            else
                return;
        }
        else
            return;
    }
}