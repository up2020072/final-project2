using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ItemPickup : MonoBehaviour
{
    public Items item;
    public int stacksize;
    //could be simplified by just finding layer
    public LayerMask Player;
    public Vector3 hover;
    private Vector2 InitialPos;
    public float Position;
    private float timeuntildespawn = 15;
    private float flashtime = 5;

    void Start()
    {
        Invoke("DespawnItem", timeuntildespawn);
        InvokeRepeating("DespawnFlashOff", timeuntildespawn - flashtime, 1);
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = item.Sprite;
        hover = new Vector3(0, 0.1f, 0);
        InitialPos = transform.position;

    }
    void FixedUpdate()
    {
        Hover();
        Collider2D[] ItemPickup = Physics2D.OverlapCircleAll(transform.position, 0.5f, Player);
        foreach (Collider2D entity in ItemPickup)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                GameData.Data.UIManager.GetComponent<InventoryManager>().PickupItem(item, stacksize);
                Destroy(gameObject);
            }
        }
    }
    void Hover()
    {
        Position = gameObject.transform.GetChild(0).transform.position.y - InitialPos.y;
        if (Position > 0.1)
        {
            hover = new Vector3(0, -0.05f, 0);
        }
        if (Position < 0)
        {
            hover = new Vector3(0, 0.1f, 0);
        }
        gameObject.transform.GetChild(0).transform.localPosition += hover * Time.deltaTime;
    }
    void DespawnFlashOff()
    {
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        Invoke("DespawnFlashOn", 0.5f);
    }
    void DespawnFlashOn()
    {
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
    void DespawnItem()
    {
        Destroy(gameObject);
    }
}
