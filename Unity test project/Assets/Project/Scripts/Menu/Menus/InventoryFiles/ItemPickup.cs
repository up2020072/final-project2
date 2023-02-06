using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public LayerMask Player;
    private Rigidbody2D rb2D;
    public Vector2 hover;
    private Vector2 InitialPos;
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
        Collider2D[] ItemPickup = Physics2D.OverlapCircleAll(transform.position, 0.3f, Player);
        foreach (Collider2D player in ItemPickup)
        {
            MenuData.Data.UIManager.GetComponent<InventoryManager>().AddItem(item);
            Destroy(gameObject);
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
