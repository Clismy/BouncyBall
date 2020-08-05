using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBall : MonoBehaviour
{
    Camera cam;
    float leftScreen, rightScreen, topScreen;
    CameraMove cm;
    [SerializeField] float minSpeed, maxSpeed;

    void Start()
    {
        cm = GetComponent<CameraMove>();

        cam = Camera.main;

        leftScreen = cam.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).x;
        rightScreen = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f)).x;
        topScreen = cam.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y;

        Respawn();
    }

    public void Respawn()
    {
        float randomX = Random.Range(leftScreen, rightScreen);
        float randomY = Random.Range(topScreen, topScreen + 8f);

        cm.speed = Random.Range(minSpeed, maxSpeed);

        transform.position = new Vector3(randomX, randomY);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Respawn();
    }
}