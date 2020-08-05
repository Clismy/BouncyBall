using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressToStart : MonoBehaviour
{
    bool startGame = false;

    public delegate void OnStart();
    public static OnStart OnStartGame;

    [SerializeField] Transform startScreen;

    void Start()
    {
        this.enabled = false;
        startScreen.gameObject.SetActive(true);
    }

    void Update()
    {
        if(startGame)
        {
            return;
        }

        if(Input.touchCount == 1 || Input.GetMouseButtonDown(0))
        {
            //OnStartGame.Invoke();
            startScreen.gameObject.SetActive(false);
            startGame = true;
        }
    }
}