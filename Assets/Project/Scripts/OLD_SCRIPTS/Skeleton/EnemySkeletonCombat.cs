using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonCombat : MonoBehaviour {
    public GameObject ArrowPrefab;
    public GameObject Character;
    public Vector2 EnemyDirection;
    public Vector2 PlayerPosition;
    public Vector2 EnemyPosition;
    

    void Start()
    {
        InvokeRepeating("Attack", 1.5f, 1.5f);
    }

    private void Update()
    {
        EnemyPosition = transform.position;
        PlayerPosition = GameData.Data.player.transform.position;
    }

    void Attack()
    {
        if (GetComponent<EnemySkeletonCombat>().enabled == true)
        {
            EnemyDirection = new Vector2(PlayerPosition.x - transform.position.x, PlayerPosition.y - transform.position.y);
            GameObject EnemyArrow = Instantiate(ArrowPrefab, transform.position, Quaternion.identity,transform);
            EnemyArrow.GetComponent<Rigidbody2D>().velocity = EnemyDirection * 1.5f;
            EnemyArrow.transform.Rotate(0, 0, Mathf.Atan2(EnemyDirection.y, EnemyDirection.x) * Mathf.Rad2Deg);
            Destroy(EnemyArrow, 2.0f);
        }
        else
            return;
           
    }
    
}

