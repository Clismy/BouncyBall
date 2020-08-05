using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed;
    float speedModifier;
    float startTime = 0;
    [SerializeField] bool speedUp = false;

    bool activate = false;

    void Awake()
    {
        PressToStart.OnStartGame += ActivateMovement;
    }

    void OnDestroy()
    {
        PressToStart.OnStartGame -= ActivateMovement;
    }

    void Update()
    {
       // if (!activate) return;

        if (speedUp)
        {
            speedModifier = Mathf.Min((int)(Time.time - startTime) / 5 * 0.1f, speed * 3);
        }

        transform.Translate(Vector3.down * (speed + speedModifier) * Time.deltaTime);
    }

    void ActivateMovement()
    {
        activate = true;
    }

    void OnLevelWasLoaded(int level)
    {
        startTime = Time.time;    
    }
}