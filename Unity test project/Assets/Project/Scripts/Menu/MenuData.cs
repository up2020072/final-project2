using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuData : MonoBehaviour
{
    public static MenuData Data;
    public GameObject UIManager;
    public Sprite NormalFrame;
    public Sprite SelectedFrame;
    public Item SelectedItem;
    public float SelectedHotbarSlot;
    void Start()
    {
        Data = this;
        DontDestroyOnLoad(gameObject);
        SelectedHotbarSlot = 0;
    }
}
