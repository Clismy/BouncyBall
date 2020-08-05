using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPosition : MonoBehaviour
{
    enum WallPlacement
    {
        Up, Left, Right, Down
    }

    [SerializeField, Header("Debug")] bool debugMode = false; 

    [SerializeField] WallPlacement wallPlacement;
    [SerializeField] float offset;
    Vector2 screenSize;

    void Update()
    {
        if(screenSize != new Vector2(Screen.width, Screen.height) || debugMode) 
        {
            Vector3 worldScreenPos = Vector3.zero;
            Vector3 finalPos = Vector3.zero;

            if(wallPlacement == WallPlacement.Up)
            {
                worldScreenPos = Camera.main.ScreenToWorldPoint(new Vector3(0f, Screen.height, 10f));
                finalPos = Vector3.right * transform.position.x + Vector3.up * (worldScreenPos.y + offset);
            }
            else if(wallPlacement == WallPlacement.Left)
            {
                worldScreenPos = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 10));
                finalPos = Vector3.right * (worldScreenPos.x - -offset) + Vector3.up * transform.position.y;
            }
            else if(wallPlacement == WallPlacement.Right)
            {
                worldScreenPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 10f));
                finalPos = Vector3.right * (worldScreenPos.x + offset) + Vector3.up * transform.position.y;
            }
            else if(wallPlacement == WallPlacement.Down)
            {
                worldScreenPos = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0, 10f));
                finalPos = Vector3.right * transform.position.x + Vector3.up * (worldScreenPos.y - -offset);
            }

            screenSize = new Vector2(Screen.width, Screen.height);
            transform.position = finalPos;
        }
    }
}
