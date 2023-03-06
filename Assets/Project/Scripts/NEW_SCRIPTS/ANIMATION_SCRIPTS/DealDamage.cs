using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public void Damage()
    {
        GameData.Data.SelectedSlot.item.UseItem(gameObject, LayerMask.GetMask("Enemy"));
        GameData.Data.ItemCooldown = false;
    }
}
