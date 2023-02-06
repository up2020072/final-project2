using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Vector2 PlayerPosition;

    void Start()
    {
        //GetComponent<PlayerCombat>().enabled = false;
    }
    private void Update()
    {
        PlayerPosition = transform.position;
    }
}