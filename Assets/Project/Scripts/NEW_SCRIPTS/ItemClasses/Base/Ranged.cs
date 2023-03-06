using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon/Ranged Weapon")]
public class Ranged : Weapon
{
    public GameObject projectile;
    public int AmmoConsumption;
    private Vector2 ArrowDirection;
    private Vector2 Target;
    public enum Accuracy { Inaccurate, Accurate, Precise }
    public void CheckTargets(GameObject origin)
    {
        if (origin.name == "PlayerWeapon")
        {
            Target = new Vector2(GameData.Data.MousePos.x, GameData.Data.MousePos.y);
        }
        else
        {
            Target = GameData.Data.player.transform.position;
        }
    }
    public override void UseItem(GameObject origin, LayerMask layers)
    {
        CheckTargets(origin);
        if (origin.name != "PlayerWeapon" | GameData.Data.UIManager.GetComponent<Commands>().TakeItem("arrow", AmmoConsumption))
        {
            Vector2 pos = origin.transform.position;
            ArrowDirection = new Vector2(Target.x - pos.x, Target.y - pos.y);
            GameObject Arrow = Instantiate(projectile, pos, Quaternion.identity);
            Arrow.GetComponent<Rigidbody2D>().velocity = ArrowDirection.normalized * 3;
            Arrow.GetComponent<EnemyArrow>().EnemyLayers = layers;
            Arrow.transform.Rotate(0, 0, Mathf.Atan2(ArrowDirection.y, ArrowDirection.x) * Mathf.Rad2Deg);
            Destroy(Arrow, 5.0f);
        }
        else
        {
            Debug.Log("out of ammo");
        }
    }
}
