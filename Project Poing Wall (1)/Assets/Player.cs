using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CustomInput ci;
    CustomCollision cc;

    [SerializeField] float movementSpeed;
    BetterRigidbody2D rb;


    void Awake()
    {
        rb = GetComponent<BetterRigidbody2D>();
        rb.OnEnterCollision += OnEnterCollision;
        rb.OnEnterTrigger += OnEnterTrigger;
    }
    void Start()
    {
        //ci = GetComponent<CustomInput>();
        //cc = GetComponent<CustomCollision>();
    }
    void Update()
    {
        //ci.GetInput();
    }

    void FixedUpdate()
    {

    }

    void OnEnterCollision(RaycastHit2D other)
    {

    }

    void OnEnterTrigger(RaycastHit2D other)
    {

    }
}