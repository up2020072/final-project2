using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Crosshair : MonoBehaviour
{
    public Vector3 MousePos;
    public GameObject crosshair;

    void Start ()
    {

	}
	void Update () {

        MousePos = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        GameData.Data.MousePos = new Vector2(MousePos.x-0.7f, MousePos.y);
        crosshair.transform.position = GameData.Data.MousePos;
    }
}
