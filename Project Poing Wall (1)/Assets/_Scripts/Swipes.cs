using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipes : MonoBehaviour
{
    public static Swipes instance;
    int swipeIndex = 2;
    public int CurrentSwipes => swipeIndex + 1;
    public const int MAXSWIPES = 3;

    void Start()
    {
        instance = this;
    }

    void OnLevelWasLoaded(int level)
    {
        if (instance != this)
        {
            Destroy(instance);
            instance = this;
        }

        swipeIndex = 2;
    }

    public void IncreaseSwipe()
    {
        if(swipeIndex == MAXSWIPES - 1)
        {
            return;
        }
        swipeIndex += 1;
        transform.GetChild(swipeIndex).GetComponent<SwipeHeart>().enabled = true;
    }

    public void DecreaseSwipe()
    {
        if(swipeIndex < 0)
        {
            return;
        }
        transform.GetChild(swipeIndex).GetComponent<SwipeHeart>().enabled = false;
        swipeIndex -= 1;
    }
}