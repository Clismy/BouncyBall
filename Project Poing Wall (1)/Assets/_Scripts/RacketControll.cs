using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketControll : MonoBehaviour
{
    [SerializeField] GameObject racket;
    Rigidbody rb;
    Vector3 mouse;
    Vector3 previousMouse;

    Vector3 velo;

    [SerializeField] float vertical;

    void Start()
    {
        rb = racket.GetComponent<Rigidbody>();
    }

    void Update()
    {
        mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2));

        Vector3 screen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 2));

        Debug.DrawLine(new Vector3(screen.x, screen.y, screen.z), new Vector3(screen.x, screen.y + vertical, screen.z), Color.blue);

        velo = (mouse - previousMouse);

        if (mouse.y < (screen.y + vertical))
        {
            
        }


        rb.velocity = ((racket.transform.right * mouse.x)) / Time.deltaTime;
       

        previousMouse = mouse;
    }

    void FixedUpdate()
    {
        
    }
}