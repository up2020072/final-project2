using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBlock : MonoBehaviour
{
    private GameObject Block;
    public float BlockMaxHealth;
    public float BlockCurrentHealth;
    public int BlockType;
    private int BlockAmount;
    public HealthBlock(float maxhealth, float currenthealth, int healthtype, int blockamount, GameObject block)
    {
        BlockAmount = blockamount;
        BlockMaxHealth = maxhealth;
        BlockCurrentHealth = currenthealth;
        BlockType = healthtype;
        Block = block;
        switch (BlockType)
        {
            //normal
            case 0:
                Block.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            //armour
            case 1:
                Block.GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            //shields
            case 2:
                Block.GetComponent<SpriteRenderer>().color = Color.cyan;
                break;
        }
    }
    public void UpdateBlock()
    {
        Vector2 scale = new Vector2((1f / BlockAmount) * (BlockCurrentHealth / BlockMaxHealth), 0.75f);
        Block.transform.localScale = scale;
    }
}
