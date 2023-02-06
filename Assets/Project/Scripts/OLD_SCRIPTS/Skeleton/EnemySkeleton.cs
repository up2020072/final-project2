using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : MonoBehaviour
{
    public Animator animator;
    public Transform SearchPoint;
    public float ArrowSpeed = 1.0f;
    public Vector2 EnemyDirection;
    public Vector2 PlayerPosition;
    public Vector2 EnemyPosition;
    private GameObject Character;
    public GameObject enemy;
    public float OutOfRange;
    private Rigidbody2D rb2D;
    public float speed = 0.2f;
    public bool CanMove;
    public Vector2 localscale;

    void Start()
    {
        CanMove = true;
        GetComponent<EnemySkeletonCombat>().enabled = false;
        rb2D = GetComponent<Rigidbody2D>();
        speed = speed / Mathf.Pow(transform.localScale.x, 2);
    }

    void Update()
    {
        Search();
        if (GetComponent<Health>().damage == 0)
        {
            Move();
        }
        EnemyPosition = transform.position;
        PlayerPosition = GameData.Data.player.transform.position;
        EnemyDirection = new Vector2(PlayerPosition.x - transform.position.x, PlayerPosition.y - transform.position.y);
        if (EnemyDirection.x > 0)
        {
            animator.SetBool("Mirror", true);
        }
        else
            animator.SetBool("Mirror", false);
        OutOfRange = Mathf.Sqrt(EnemyDirection.x * EnemyDirection.x + EnemyDirection.y * EnemyDirection.y);
        EnemyDirection.Normalize();
        animator.SetFloat("Horizontal", EnemyDirection.x);
        animator.SetFloat("Vertical", EnemyDirection.y);
    }

    void Move()
    {
        if (OutOfRange > 4)
        {
            EnemyDirection.Normalize();
            if (OutOfRange < 7)
            {
                rb2D.MovePosition(rb2D.position + EnemyDirection * speed * Time.fixedDeltaTime);
                animator.SetBool("Movement", true);
            }
        }
        if (OutOfRange <2 )
            {
                rb2D.MovePosition(rb2D.position + -EnemyDirection * speed * Time.fixedDeltaTime);
                GetComponent<EnemySkeletonCombat>().enabled = false;
                animator.SetBool("Movement", true);
                animator.SetBool("Firing", false);
        }
        if (OutOfRange > 2)
        {
            if (OutOfRange < 4)
            {
                animator.SetBool("Movement", false);
            }
        }
        if (OutOfRange > 7)
        {
          animator.SetBool("Movement", false);
        }
    }

    void Search()
    {
        if (OutOfRange < 5)
        {
            if (OutOfRange > 2)
            {
                GetComponent<EnemySkeletonCombat>().enabled = true;
                animator.SetBool("Firing", true);
            }
        }
        if (OutOfRange > 5)
        {
            GetComponent<EnemySkeletonCombat>().enabled = false;
        }
        else
            return;
    }

   
    private void OnDrawGizmosSelected()
    {
        if (SearchPoint == null)
            return;
        Gizmos.DrawWireSphere(SearchPoint.position, 2);
        Gizmos.DrawWireSphere(SearchPoint.position, 5);
        Gizmos.DrawWireSphere(SearchPoint.position, 7);
    }
}