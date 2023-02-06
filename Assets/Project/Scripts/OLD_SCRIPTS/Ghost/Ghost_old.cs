using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_old : MonoBehaviour
{
    public Animator animator;
    public GameObject enemy;
    private Rigidbody2D rb2D;
    public float speed;
    public float NewSpeed;
    public Vector2 EnemyDirection;
    public Vector2 PlayerPosition;
    public Vector2 EnemyPosition;
    public float OutOfRange;
    public GameObject Player;
    public float PlayerCurrentHealth;
    public float PlayerMaxHealth;
    public bool CanMove;
    public bool Angry;

    void Start()
    {
        Angry = false;
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Search();
        EnemyPosition = transform.position;
        PlayerPosition = Player.transform.position;
        PlayerCurrentHealth = Player.GetComponent<PlayerHealth>().CurrentHealthTotal;
        PlayerMaxHealth = Player.GetComponent<PlayerHealth>().MaxHealth;
        if (PlayerCurrentHealth/PlayerMaxHealth<0.4)
        {
            Angry = true;
            animator.SetBool("Angry",true);
        }
        else
        {
            Angry = false;
            animator.SetBool("Angry",false);

        }
        EnemyDirection = new Vector2(PlayerPosition.x - transform.position.x, PlayerPosition.y - transform.position.y);
        OutOfRange = Mathf.Sqrt(EnemyDirection.x * EnemyDirection.x + EnemyDirection.y * EnemyDirection.y);
        if (EnemyDirection.x < 0)
        {
            animator.SetBool("Mirror", true);
        }
        else
            animator.SetBool("Mirror", false);
        EnemyDirection.Normalize();
    }

    void Move()
    {
        if (Angry == false)
        {
            if (OutOfRange > 1.5)
            {
                NewSpeed = (speed * OutOfRange)-0.45f;
                rb2D.MovePosition(rb2D.position + EnemyDirection * NewSpeed * EnemyDirection.magnitude / Mathf.Pow(transform.localScale.x, 2) * Time.fixedDeltaTime);
            }
        }
        if (Angry == true)
        {
            if (OutOfRange > 0.3)
            {
                NewSpeed = speed * 5;
                rb2D.MovePosition(rb2D.position + EnemyDirection * NewSpeed * EnemyDirection.magnitude / Mathf.Pow(transform.localScale.x, 2) * Time.fixedDeltaTime);
            }
        }
    }
    void Search()
    {
        if (OutOfRange < 0.4 * transform.localScale.x & Angry==true)
        {
                //GetComponent<EnemyGhostCombat>().enabled = true;
                animator.SetBool("Attack", true);
        }
         else
                //GetComponent<EnemyGhostCombat>().enabled = false;
                animator.SetBool("Attack", false);
     
    }
}
