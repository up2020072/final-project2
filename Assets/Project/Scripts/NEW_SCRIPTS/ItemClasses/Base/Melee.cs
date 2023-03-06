using UnityEngine;
[CreateAssetMenu(menuName = "Item/Weapon/Melee Weapon")]
public class Melee : Weapon
{
    public enum Knockback { None, Low, Average, High }
    public Knockback knockback = Knockback.None;
    public float range;
    public override void UseItem(GameObject origin, LayerMask targets)
    {
        Collider2D[] HitEntities = Physics2D.OverlapCircleAll(origin.transform.position, range, targets);
        foreach (Collider2D entity in HitEntities)
        {
            entity.GetComponent<HealthBar>().TakeDamage(damage, (int)damagetype, (int)knockback);
        }
    }
}