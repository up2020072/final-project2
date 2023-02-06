using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    public Transform AttackPoint;
    public float AttackRange=0.3f;
    public LayerMask EnemyLayers;
    public int SkeletonDamage;
    public float localscale;


    void Start()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        localscale = transform.parent.transform.localScale.x;
    }


    void Update()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, 0.3f*localscale, EnemyLayers);
        foreach (Collider2D enemy in HitEnemies)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            SkeletonDamage = Mathf.RoundToInt(2 * Mathf.Pow(localscale, 2));
            enemy.GetComponent<PlayerHealth>().DamagePlayer(SkeletonDamage, 0);
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
