using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    public Transform AttackPoint;
    private float AttackRange = 0.3f;
    public LayerMask EnemyLayers;
    public int Damage;


    void Start()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }


    void Update()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, 0.07f, EnemyLayers);
        foreach (Collider2D enemy in HitEnemies)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            enemy.GetComponent<HealthBar>().TakeDamage(Damage, 0, 0);
            GetComponent<EnemyArrow>().enabled = false;

        }

    }
    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
