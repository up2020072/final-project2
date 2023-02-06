using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEase : MonoBehaviour
{
    public float Smooth = 0.125f;
    public float Timer = 0.2f;
    public float ShakeConstant = 0.05f;
    public Transform player;
    public Vector3 DistanceFromScreen;
    public Vector3 NewPos;
    public Vector3 ScreenPosPixels;
    public Vector3 ScreenPosRounded;
    public Vector3 PixelToPos;

    void FixedUpdate()
    {
        Timer += Time.deltaTime;
        ScreenPosPixels = GetComponent<Camera>().WorldToScreenPoint(player.position);
        ScreenPosRounded = new Vector3(Mathf.RoundToInt(ScreenPosPixels.x), Mathf.RoundToInt(ScreenPosPixels.y),Mathf.RoundToInt(ScreenPosPixels.z));
        PixelToPos = GetComponent<Camera>().ScreenToWorldPoint(ScreenPosRounded);
        NewPos = Vector3.Lerp(transform.position,(player.position+DistanceFromScreen),Smooth);
        transform.position = new Vector3((NewPos.x), (NewPos.y), -10);
        if (GameData.Data.EnemyHit == true)
        {
            Timer = 0;
            GameData.Data.EnemyHit = false;
        }
        if (Timer < 0.2)
        {
            transform.localPosition = transform.position + Random.insideUnitSphere * ShakeConstant * MenuData.Data.SelectedItem.Shake;
        }
    }
}
