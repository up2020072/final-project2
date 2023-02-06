using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float Smooth = 0.125f;
    public float Timer = 0f;
    public float ShakeConstant = 0.05f;
    public Transform player;
    public Vector3 DistanceFromScreen;
    public Vector3 NewPos;
    public Vector3 ScreenPosPixels;
    public Vector3 ScreenPosRounded;
    public Vector3 PixelToPos;
    private Items SelectedItem;
    private void Update()
    {
        if (SelectedItem is Melee)
        {
            Timer += Time.fixedDeltaTime * (int)(SelectedItem as Melee).speed;
        }
        else return;
        if (GameData.Data.GetHit == true)
        {
            Timer = 0f;
            Shake();
        }
        if (GameData.Data.ItemCooldown == false)
        {
            GameData.Data.GetHit = false;
        }
    }
    void FixedUpdate()
    {
        Timer += Time.deltaTime;
        ScreenPosPixels = GetComponent<Camera>().WorldToScreenPoint(player.position);
        ScreenPosRounded = new Vector3(Mathf.RoundToInt(ScreenPosPixels.x), Mathf.RoundToInt(ScreenPosPixels.y),Mathf.RoundToInt(ScreenPosPixels.z));
        PixelToPos = GetComponent<Camera>().ScreenToWorldPoint(ScreenPosRounded);
        NewPos = Vector3.Lerp(transform.position,(player.position+DistanceFromScreen),Smooth);
        SelectedItem = GameData.Data.UIManager.GetComponent<InventoryManager>().InventorySlots[GameData.Data.SelectedSlotNum].item;
        transform.position = new Vector3((NewPos.x), (NewPos.y), -10);
    }
    void Shake()
    {
        transform.localPosition = transform.position + Random.insideUnitSphere * ShakeConstant * 3/(0.2f+(int)((SelectedItem as Melee).speed)) /(1+Timer);
    }
}
