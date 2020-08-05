using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFollowVelocity : MonoBehaviour
{
    Vector3 startPos;
    Rigidbody2D rb;

    void Start()
    {
        startPos = transform.localPosition;
        rb = transform.parent.parent.parent.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 velocity = rb.velocity.normalized;
        //velocity.x = Mathf.Clamp(velocity.x, -0.1f, 0.1f);
        //velocity.y = Mathf.Clamp(velocity.y, -0.1f, 0.1f);

        velocity /= 10;
        Vector3 finalPosition = (startPos + velocity);

        transform.localPosition = finalPosition;
    }
}