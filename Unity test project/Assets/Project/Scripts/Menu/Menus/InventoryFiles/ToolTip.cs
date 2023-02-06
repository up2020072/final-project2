using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    public Text ItemName;
    public Text ItemDescription;
    public Text DamageText;
    public Text SpeedText;
    public int ItemPrice;
    public int ItemRarity;

    public GameObject SellButton;
    public GameObject UseButton;

    public string itemname;
    public string itemdescription;
    public string damagetext;
    public string speedtext;

    public bool Consumable;
    public bool ForSale;

    public List<Color> RarityColours;

    void Start()
    {
        Consumable = false;
        ForSale = false;
    }

    void Update()
    {
        ItemName.text = itemname;
        ItemName.color = RarityColours[ItemRarity];
        ItemDescription.text = itemdescription;
        ItemDescription.color = RarityColours[ItemRarity];
        if (speedtext == "None")
        {
            SpeedText.text = null;
        }
        else
        {
            SpeedText.text = speedtext;
            SpeedText.color = RarityColours[ItemRarity];
        }
        if (damagetext == " Damage")
        {
            DamageText.text = null;
        }
        else
        {
            DamageText.text = damagetext;
            DamageText.color = RarityColours[ItemRarity];
        }
        if (damagetext == null)
        {
            DamageText.text = null;
        }
        if (Consumable == true)
        {
            UseButton.SetActive(true);
        }
        else
        {
            UseButton.SetActive(false);
        }

        if (ForSale == true)
        {
            SellButton.SetActive(true);
            SellButton.GetComponentInChildren<Text>().text = "Sell Item $" + ItemPrice;
        }
        else
        {
            SellButton.SetActive(false);
        }
    }
}
