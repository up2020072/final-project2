using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

    public GameObject Character;
    public GameObject Menu;
    public GameObject PauseMenu;
    public GameObject Inventory;
    public GameObject PlayerStats;
    public GameObject Shop;
    public GameObject InventorySlots;
    public GameObject ToolTip;
    public GameObject HotBar;

    public Text HealthText;
    public Text ArrowText;
    public Text MoneyText;
    public Text Description;
    public Text LevelText;
    public Slider Experience;


    public bool InventoryIsOpen;
    public bool InventoryCooldown;

    public bool PauseIsOpen;
    public bool PauseCooldown;

    public bool ShopIsOpen;
    public bool ShopCooldown;

    public bool UIOpen;

    public float CurrentMoney;
    public float CurrentHealth;
    public float CurrentAmmo;

    public string ItemDescription;

    void Start ()
    {
        Time.timeScale = 0f;
        InventoryIsOpen = false;
        PauseIsOpen = false;
        ShopIsOpen = false;
        UIOpen = false;
        Menu.SetActive(true);
	}

    private void Update()
    {
        
        if (Input.GetKeyDown("escape") & PauseIsOpen == false & UIOpen == false)
        {
            PauseIsOpen = true;
            PauseMenu.SetActive(true);
            PauseCooldown = true;
            UIOpen = true;
            Invoke("CoolDown", 0.1f);
        }
        if (Input.GetKeyDown("escape") & PauseIsOpen == true & PauseCooldown == false)
        {
            PauseMenu.SetActive(false);
            PauseIsOpen = false;
            UIOpen = false;
        }


        if (Input.GetKeyDown("e") & InventoryIsOpen == false & UIOpen == false)
        {
            Inventory.SetActive(true);
            InventoryIsOpen = true;
            InventoryCooldown = true;
            UIOpen = true;
            Invoke("CoolDown", 0.1f);
          
        }
        if (Input.GetKeyDown("e") & InventoryIsOpen == true & InventoryCooldown == false)
        {
            Inventory.SetActive(false);
            InventoryIsOpen = false;
            ToolTip.SetActive(false);
            UIOpen = false;
        }

        if (Input.GetKeyDown("q") & ShopIsOpen == false & UIOpen == false)
        {
            Shop.SetActive(true);
            ShopIsOpen = true;
            ShopCooldown = true;
            UIOpen = true;
            Invoke("CoolDown", 0.1f);

        }
        if (Input.GetKeyDown("q") & ShopIsOpen == true & ShopCooldown == false)
        {
            Shop.SetActive(false);
            ShopIsOpen = false;
            UIOpen = false;
        }
        float CurrentExperience = GameData.Data.CurrentExperience;
        float ExperienceRequired = GameData.Data.ExperienceRequired;
        Experience.value = CurrentExperience / ExperienceRequired;
        if (CurrentExperience > ExperienceRequired)
        {
            GameData.Data.Level += 1;
            GameData.Data.CurrentExperience -= ExperienceRequired;
        }
        CurrentHealth = GameData.Data.CurrentHealth;
        CurrentAmmo = GameData.Data.CurrentAmmo;
        CurrentMoney = GameData.Data.CurrentMoney;
        ItemDescription = GameData.Data.ItemDescription;
        HealthText.text = CurrentHealth.ToString();
        ArrowText.text = CurrentAmmo.ToString();
        MoneyText.text = CurrentMoney.ToString();
        Description.text = ItemDescription.ToString();
        LevelText.text = "Level " + GameData.Data.Level;
    }
    public void GetMoney(int Money)
    {
        CurrentMoney += Money;
    }
    public void CoolDown()
    {
        InventoryCooldown = false;
        PauseCooldown = false;
        ShopCooldown = false;
    }
    public void StartGame()
    {
        if (SceneManager.GetActiveScene().buildIndex==0)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
        }
        Time.timeScale = 1f;
        HotBar.SetActive(true);
        PlayerStats.SetActive(true);
    }
    public void ResumeGame()
    {
        PauseIsOpen = false;
        Time.timeScale = 1f;
    }
    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(1);
        SceneManager.LoadSceneAsync(0,LoadSceneMode.Additive);
    }
    public void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
