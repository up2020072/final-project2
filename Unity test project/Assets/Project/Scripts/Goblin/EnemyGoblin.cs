using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoblin : MonoBehaviour {
    public Animator animator;
    private Rigidbody2D rb2D;
    public float speed = 0.5f;
    public Vector2 EnemyDirection;
    public Vector2 PlayerPosition;
    public Vector2 EnemyPosition;
    public float OutOfRange;
    public GameObject Character;


    void Start ()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
	
	void Update ()
    {
        Search();
        EnemyPosition = transform.position;
        PlayerPosition = Character.transform.position;
        EnemyDirection = new Vector2(PlayerPosition.x - transform.position.x, PlayerPosition.y - transform.position.y);
        OutOfRange = Mathf.Sqrt(EnemyDirection.x * EnemyDirection.x + EnemyDirection.y * EnemyDirection.y);
        if (EnemyDirection.x > 0)
        {
            animator.SetBool("Mirror", true);
        }
        else
            animator.SetBool("Mirror", false);
        EnemyDirection.Normalize();
        if (GetComponent<Health>().GetHit==false & OutOfRange > 0.4*transform.localScale.x & transform.GetComponent<Health>().GetHit==false)
        {
            Move();
        }
        animator.SetFloat("Horizontal", EnemyDirection.x);
        animator.SetFloat("Vertical", EnemyDirection.y);
        animator.SetFloat("Movement", EnemyDirection.magnitude);
     
    }

    void Move()
    {
        if (OutOfRange < 7)
        {
            rb2D.MovePosition(rb2D.position + EnemyDirection * speed/Mathf.Pow(transform.localScale.x,2) * Time.fixedDeltaTime);
        }

        
        else
            return;
    }

    void Search()
    {
        if (OutOfRange < 0.4*transform.localScale.x)
        {
            animator.SetBool("Attack",true);
            GetComponent<EnemyGoblinCombat>().enabled = true;
        }
        if (OutOfRange > 0.4*transform.localScale.x)
        {
            animator.SetBool("Attack",false);
            GetComponent<EnemyGoblinCombat>().enabled = false;
        }
        else
            return;
    }


   
}
