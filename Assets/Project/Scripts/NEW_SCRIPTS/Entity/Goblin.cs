using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    public void Start()
    {
        this.Init();
        Health = gameObject.AddComponent<HealthBar>();
        Health.InitHealthBar(15, 15, HealthBar.HealthType.Normal, 5, gameObject);
    }
}
