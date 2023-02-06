using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

    public GameObject Menu;
    public GameObject PauseMenu;
    public GameObject Inventory;
    public GameObject HotBar;
    public GameObject ToolTip;
    public GameObject TextInput;
    public GameObject TileMap;
    private int scrolling;
    public float Timer;
    public float OldTime;
    public float NewTime;

    private bool MapOpen = false;
    void Start ()
    {
        Menu.SetActive(true);
    }
    private void Update()
    {
        Menus();
        if (!GameData.Data.TextOpen) { Scroll(); }
        if (Timer < 1)
        {
            Timer += 0.015f;
            Time.timeScale = Mathf.Lerp(OldTime, NewTime, Timer);
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
        //Transform minimap = TileMap.GetComponent<TileMap>().mapindicator.transform;
        //Vector3 dir = GameData.Data.player.GetComponent<Player>().MoveDirection;
        //Vector2 pos = GameData.Data.player.GetComponent<Player>().transform.position;
        //float mapscale = 100f / 256f;
        //minimap.localPosition = new Vector2((pos.x + (pos.y*2))*mapscale -50, -(pos.x - (pos.y*2)) * mapscale - 50);
        //minimap.rotation = Quaternion.Euler(0,0,-(45 +Mathf.Atan2(dir.x, dir.y*2) * (180 / Mathf.PI)));
    }
    private void Menus()
    {
        //need some sort of state system here
        if (Input.GetKeyDown(KeyCode.Escape) && GameData.Data.InventoryOpen == false)
        {
            Time.timeScale = System.Convert.ToSingle(GameData.Data.PauseOpen);
            GameData.Data.PauseOpen = !GameData.Data.PauseOpen;
            PauseMenu.SetActive(GameData.Data.PauseOpen);
        }
        if (Input.GetKeyDown(KeyCode.E) && GameData.Data.PauseOpen == false && GameData.Data.TextOpen == false)
        {
            Timer = 0f;
            OldTime = NewTime;
            NewTime = 1f - (0.9f * System.Convert.ToSingle(!GameData.Data.InventoryOpen));
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
            GameData.Data.InventoryOpen = !GameData.Data.InventoryOpen;
            Inventory.SetActive(GameData.Data.InventoryOpen);
            ToolTip.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Tab) && GameData.Data.InventoryOpen == true)
        {
            GameData.Data.TextOpen = !GameData.Data.TextOpen;
            TextInput.SetActive(GameData.Data.TextOpen);
        }
        if (Input.GetKey(KeyCode.Z)) Zoom(0.1f);
        if (Input.GetKeyDown(KeyCode.G)) TileMap.GetComponent<Map>().GenerateMap();
        if (Input.GetKey(KeyCode.C)) Zoom(-0.1f);
        if (Input.GetKey(KeyCode.R)) Camera.main.orthographicSize = 4; //reset all preferences
        if (Input.GetKeyDown(KeyCode.M) && MapOpen == false)
        {
            TileMap.GetComponent<Map>().mapSprite.rectTransform.localScale = new Vector3(7, 7, 1);
            MapOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.M) && MapOpen == true)
        {
            TileMap.GetComponent<Map>().mapSprite.rectTransform.localScale = new Vector3(2, 2, 1);
            MapOpen = false;
        }
    }
    private void Zoom(float amount)
    {
        float currentzoom = Camera.main.orthographicSize;
        float relativezoom = Camera.main.orthographicSize += (amount * currentzoom / 10);
        Camera.main.orthographicSize = Mathf.Clamp(relativezoom, 1f, 100);
    }
    private void Scroll()
    {
        scrolling = -(int)Input.mouseScrollDelta.y;
        if (scrolling != 0)
        {
            if (scrolling == 1 && GameData.Data.SelectedSlotNum >= 9)
            {
                GameData.Data.ChangeHotbarSlot(0);
            }
            else if (scrolling == -1 && GameData.Data.SelectedSlotNum <= 0)
            {
                GameData.Data.ChangeHotbarSlot(9);
            }
            else
            {
                GameData.Data.ChangeHotbarSlot(GameData.Data.SelectedSlotNum + scrolling);
            }
        }
        for (int i = 48; i < 58; i++)
        {
            if (Input.GetKeyDown((KeyCode)i))
            {
                if (i == 48)
                {
                    GameData.Data.ChangeHotbarSlot(9);
                }
                else
                {
                    GameData.Data.ChangeHotbarSlot(i - 49);
                }
            }
        }
    }
    public void StartGame()
    {
        Timer = 0;
        NewTime = 1;
        HotBar.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(1);
        SceneManager.LoadSceneAsync(0,LoadSceneMode.Additive);
    }
    public void ReloadMap()
    {
        TileMap tm = TileMap.GetComponent<TileMap>();
        foreach (GameObject objects in tm.objects) Destroy(objects);
        //objects.SetActive(false);
    }
    public void ResumeGame()
    {
        Timer = 0f;
        OldTime = NewTime;
        NewTime = 1;
        GameData.Data.PauseOpen = false;
    }
    public void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
