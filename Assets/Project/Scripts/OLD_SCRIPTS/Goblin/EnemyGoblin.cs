using UnityEngine;

public class EnemyGoblin : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb2D;
    public float speed = 0.5f;
    public Vector2 EnemyDirection;
    public Vector2 PlayerPosition;
    public Vector2 EnemyPosition;
    public float OutOfRange;


    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        EnemyPosition = transform.position;
        PlayerPosition = GameData.Data.player.transform.position;
        EnemyDirection = new Vector2(PlayerPosition.x - transform.position.x, PlayerPosition.y - transform.position.y);
        OutOfRange = Mathf.Sqrt(EnemyDirection.x * EnemyDirection.x + EnemyDirection.y * EnemyDirection.y);
        EnemyDirection.Normalize();
        if (GetComponent<Health>().GetHit == false && transform.GetComponent<Health>().GetHit == false && gameObject.GetComponent<BoxCollider2D>().enabled == true)
        {
            Move();
        }
        animator.SetFloat("Horizontal", EnemyDirection.x);
        animator.SetFloat("Vertical", EnemyDirection.y);
        animator.SetFloat("Movement", EnemyDirection.magnitude);

    }

    void Move()
    {
        if (OutOfRange < 7 && OutOfRange > 0.3 * transform.localScale.x)
        {
            rb2D.MovePosition(rb2D.position + EnemyDirection * speed / Mathf.Pow(transform.localScale.x, 2) * Time.deltaTime);
            animator.SetBool("Attack", false);
            GetComponent<EnemyGoblinCombat>().enabled = false;
        }
        else if (OutOfRange < 0.3 * transform.localScale.x)
        {
            Invoke("Search", 0.5f);
        }
        else
        {
            return;
        }
    }

    void Search()
    {
        animator.SetBool("Attack", true);
        GetComponent<EnemyGoblinCombat>().enabled = true;
    }
}

