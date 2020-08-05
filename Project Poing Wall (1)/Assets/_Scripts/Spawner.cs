using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spawner : MonoBehaviour
{
    [SerializeField] float spawnTime;
    [SerializeField] GameObject[] obstacles;
    float timePassed;

    Camera cam;
    float leftScreen, rightScreen, topScreen;

    bool activate = false;


    void Awake()
    {
        PressToStart.OnStartGame += ActivateSpawner;
        activate = true;
    }
    void OnDestroy()
    {
        PressToStart.OnStartGame -= ActivateSpawner;
    }

    void Start()
    {
        cam = Camera.main;

        leftScreen = cam.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).x;
        rightScreen = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f)).x;
        topScreen = cam.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y;
    }

    void Update()
    {
        //if (!activate) return;

        timePassed += Time.deltaTime;

        if(timePassed > spawnTime)
        {
            SpawnWalls();
            timePassed = 0;
        }
    }

    void ActivateSpawner()
    {
        activate = true;
    }

    void SpawnWalls()
    {
        //float outChance = Random.Range(0f, 10f);

        float randomX = Random.Range(leftScreen + 0.2f, rightScreen - 0.2f);
        float randomY = Random.Range(topScreen, topScreen + 8f);
        float randomZrot = Random.Range(0, 36) * 10;

        float randomSize = Random.Range(0.7f, 1.5f);

        GameObject prefab = obstacles.OrderBy(i=>Random.value).First();

        var instance = Instantiate(prefab, new Vector3(randomX, topScreen + 2f, 0f), Quaternion.Euler(Vector3.forward * randomZrot), transform);
        instance.transform.localScale = instance.transform.localScale * randomSize;
    }
}