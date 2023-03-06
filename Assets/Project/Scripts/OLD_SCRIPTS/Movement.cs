using UnityEngine;

public class Movement : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb2D;

    public void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    private Vector2 movement = Vector3.zero;
    private void FixedUpdate()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Movement", movement.magnitude);
        animator.SetBool("LegAnimator", true);
        rb2D.MovePosition(rb2D.position + movement * 2.5f * Time.deltaTime);
    }


}

