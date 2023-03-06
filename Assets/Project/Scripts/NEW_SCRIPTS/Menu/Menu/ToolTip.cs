using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    public Items tooltipitem;
    public Text ItemName;
    public Text ItemDescription;
    public Text Type;
    public Text DamageText;
    public Text SpeedText;
    public bool Consumable;
    public bool ForSale;

    void Start()
    {
        Consumable = false;
        ForSale = false;
    }
    void EnableToolTip()
    {
        ItemName.text = tooltipitem.ItemName;
        ItemName.color = GameData.Data.RarityColours[(int)tooltipitem.rarity];

        ItemDescription.text = tooltipitem.Description;
        ItemDescription.color = GameData.Data.RarityColours[(int)tooltipitem.rarity];

        Type.text = tooltipitem.GetType().ToString();
        Type.color = GameData.Data.RarityColours[(int)tooltipitem.rarity];
        if (tooltipitem is Interactable)
        {
            SpeedText.gameObject.SetActive(true);
            //SpeedText.text = tooltipitem.speed.ToString();
            SpeedText.color = GameData.Data.RarityColours[(int)tooltipitem.rarity];
            SpeedText.gameObject.SetActive(false);
        }
        else
        {
            SpeedText.gameObject.SetActive(false);
        }
        if (tooltipitem is Weapon)
        {
            DamageText.gameObject.SetActive(true);
            DamageText.text = (tooltipitem as Weapon).damage.ToString() + " Damage";
            DamageText.color = GameData.Data.RarityColours[(int)tooltipitem.rarity];
        }
        else
        {
            DamageText.gameObject.SetActive(false);
        }
    }
}
