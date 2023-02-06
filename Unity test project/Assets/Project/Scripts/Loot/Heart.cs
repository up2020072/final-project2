using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public GameObject Menu;
    public GameObject heart;
    public float AttackRange = 0.5f;
    public LayerMask Player;
    private Rigidbody2D rb2D;
    public Vector2 hover;
    public Vector2 InitialPos;
    public float Position;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        hover = new Vector2(0, 0.1f);
        InitialPos = transform.position;
    }

    void Update()
    {
        Hover();
        Collider2D[] CoinPickup = Physics2D.OverlapCircleAll(transform.position, AttackRange, Player);
        foreach (Collider2D player in CoinPickup)
        {
            player.GetComponent<PlayerHealth>().HealPlayer();
            Destroy(heart);
            Debug.Log("Heart Picked up");
        }
    }
    void Hover()
    {
        Position = transform.position.y - InitialPos.y;
        if (Position > 0.1)
        {
            hover = new Vector2(0, -0.05f);
        }
        if (Position < 0)
        {
            hover = new Vector2(0, 0.1f);
        }
        rb2D.MovePosition(rb2D.position + hover * Time.deltaTime);
    }
}