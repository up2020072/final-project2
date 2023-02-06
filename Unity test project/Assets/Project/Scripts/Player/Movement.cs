using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb2D;

    public void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    private Vector2 movement = Vector3.zero;
    public void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Movement", movement.magnitude);
        animator.SetBool("LegAnimator", true);
    }

    private void FixedUpdate()
    {
        float Speed = GameData.Data.PlayerSpeed;
        rb2D.MovePosition(rb2D.position + movement * Speed * Time.fixedDeltaTime);
    }

 
}

   