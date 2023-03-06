using UnityEngine;

public class EnemyGoblinCombat : MonoBehaviour
{
    public Vector2 EnemyDirection;
    public Vector2 PlayerPosition;
    public Vector2 EnemyPosition;
    public float OutOfRange;
    public Transform SearchPoint;
    public float SearchRange;
    public Animator animator;
    public int GoblinDamage;
    public Vector2 localscale;

    void Start()
    {
        InvokeRepeating("Attack", 0.0f, 0.5f);
    }

    public void Update()
    {
        EnemyPosition = transform.position;
        PlayerPosition = GameData.Data.player.transform.position;
        EnemyDirection = new Vector2(PlayerPosition.x - transform.position.x, PlayerPosition.y - transform.position.y);
        OutOfRange = Mathf.Sqrt(EnemyDirection.x * EnemyDirection.x + EnemyDirection.y * EnemyDirection.y);
    }

    void Attack()
    {
        if (GetComponent<EnemyGoblinCombat>().enabled == true)
        {
            if (OutOfRange < SearchRange * Mathf.Pow(transform.localScale.x, 2))
            {
                GoblinDamage = Mathf.RoundToInt(1 * Mathf.Pow(transform.localScale.x, 2));
                GameData.Data.player.GetComponent<PlayerHealth>().DamagePlayer(GoblinDamage, 0.3f);
            }
            else
                return;
        }


        else
            return;

    }
    private void OnDrawGizmosSelected()
    {
        if (SearchPoint == null)
            return;
        Gizmos.DrawWireSphere(SearchPoint.position, SearchRange);

    }

}

