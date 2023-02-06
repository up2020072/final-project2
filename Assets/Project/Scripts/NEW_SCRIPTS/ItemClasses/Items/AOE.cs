using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item/Weapon/AOE")]

public class AOE : Melee
{
    public override void UseItem(GameObject origin, LayerMask targets)
    {
        Collider2D[] HitEntities = Physics2D.OverlapCircleAll(origin.transform.position, range, targets);
        foreach (Collider2D entity in HitEntities)
        {
            float distance = (entity.transform.position - origin.transform.position).magnitude;
            float falloff = 1/distance;
            entity.GetComponent<HealthBar>().TakeDamage(damage * falloff, (int)damagetype, (int)knockback);
        }
    }
}
