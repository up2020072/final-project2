using UnityEngine;
[CreateAssetMenu(menuName = "Item/Consumable/HealingItem")]
public class HealingConsumable : Consumable
{
    public float HealAmount;
    public override void UseItem(GameObject origin, LayerMask targets)
    {
        origin.GetComponentInParent<HealthBar>().Heal(HealAmount);
        GameData.Data.UIManager.GetComponent<InventoryManager>().ConsumeItem();
    }
}