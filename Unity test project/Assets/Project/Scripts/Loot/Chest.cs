using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject Player;
    public Transform chest;
    public Animator animator;
    public GameObject BronzeCoin;
    public GameObject SilverCoin;
    public GameObject GoldCoin;
    public GameObject Hearts;
    public GameObject Arrow;
    public Vector2 Distance;
    private bool ChestOpened;
    [Range(1,10)]
    public int LootRarity;
    private GameObject LootDrop;

    void Start()
    {
        ChestOpened = false;
    }
    void Update()
    {
        Distance = Player.transform.position - chest.position;
        if (Distance.magnitude < 0.5f & Distance.magnitude > -0.5f)
        {
            if (ChestOpened == false)
            {
                if (Input.GetKeyUp(KeyCode.V) == true)
                {
                    ChestOpened = true;
                    animator.SetTrigger("OpenChest");
                    Invoke("OpenChest", 1f);
                }
            }
        }
    }
    void OpenChest()
    {
        for (int i = 0; i < Random.Range(3*LootRarity,5*LootRarity); i++)
        {
            List<GameObject> PotentialDrops = new List<GameObject>()
            {BronzeCoin, SilverCoin, GoldCoin, Arrow};
            LootDrop = PotentialDrops[Random.Range(0,4)] as GameObject;
            Vector3 position = new Vector2(chest.position.x + Random.Range(-0.4f, 0.4f), chest.position.y + Random.Range(-0.4f, 0.4f));
            GameObject lootdrop = Instantiate(LootDrop, position, Quaternion.identity);
        }
    }
}
