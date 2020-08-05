using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BetterRigidbody2D : MonoBehaviour
{
    public Action<RaycastHit2D> OnEnterCollision;
    public Action<RaycastHit2D> OnEnterTrigger;

    public Vector2 velocity;
    private LayerMask collisionMask;
    RaycastHit2D[] raycastHits = new RaycastHit2D[10];
    ContactFilter2D cFilter;
    private Collider2D coll;

    public void Start()
    {
        coll = GetComponent<Collider2D>();
        collisionMask = Physics2D.GetLayerCollisionMask(gameObject.layer);

        cFilter.useTriggers = false;
        cFilter.SetLayerMask(collisionMask);
        cFilter.useLayerMask = true;
    }

    void FixedUpdate()
    {
        var totalVelocity = velocity * Time.deltaTime;
        if(totalVelocity != Vector2.zero)
        {
            velocity = Bounce(totalVelocity) * velocity.magnitude;
        }
    }

    Vector2 Bounce(Vector2 remainingVelocity)
    {
        int count = coll.Cast(remainingVelocity, cFilter, raycastHits, remainingVelocity.magnitude);

        if (count == 0)
        {
            transform.Translate(remainingVelocity);
            return remainingVelocity.normalized;
        }

        var closestHit = FindClosest(count);

        var bRb = closestHit.transform.GetComponent<BetterRigidbody2D>();
        if (bRb != null)
        {
            bRb.OnEnterCollision?.Invoke(closestHit);
            OnEnterCollision?.Invoke(closestHit);
        }

        float distanceFromClosest = Vector2.Distance(transform.position, closestHit.point);
        distanceFromClosest -= coll.bounds.extents.x;
        distanceFromClosest = Mathf.Max(distanceFromClosest, 0.01f);

        if (distanceFromClosest < 0.031f)
        {
            Debug.Log("GOOOOO");
            var reverseDirection = -(remainingVelocity.normalized);
            var i = 0;
            while(Physics2D.OverlapCircle(reverseDirection * i * 0.1f, coll.bounds.extents.x * 1.1f) != null)
            {
                i++;
            }

            float distance = i * 0.1f;
            distance += coll.bounds.extents.x;

            var bigD = (Vector2)transform.position - ((Vector2)transform.position + (reverseDirection * distance));

            coll.Cast(remainingVelocity, cFilter, raycastHits, remainingVelocity.magnitude);
            var normal = raycastHits[0].normal;

            transform.Translate(bigD);

            bigD = Vector2.Reflect(bigD, normal.normalized);
            Bounce(bigD); //THICC
        }
        
        var moveDistance = (remainingVelocity.magnitude - distanceFromClosest);
        if(moveDistance <= 0)
        {
            Debug.Log(distanceFromClosest);
            return Vector2.zero;
        }
        var newVelocity = moveDistance * remainingVelocity.normalized;

        Vector2 reflect = Vector2.Reflect(newVelocity, closestHit.normal.normalized);

        int triggerCount = coll.Cast(remainingVelocity, cFilter, raycastHits, distanceFromClosest);
        for(int i = 0; i < triggerCount; i++)
        {
            raycastHits[i].transform.GetComponent<BetterRigidbody2D>()?.OnEnterTrigger?.Invoke(raycastHits[i]);
            OnEnterTrigger?.Invoke(raycastHits[i]);
        }

        transform.Translate(remainingVelocity.normalized * distanceFromClosest);
        return Bounce(reflect);
    }

    RaycastHit2D FindClosest(int c)
    {
        var closest = raycastHits[0];
        for (int i = 0; i < c; i++)
        {
            var it = raycastHits[i];

            if (Vector2.Distance(transform.position, closest.point) > Vector2.Distance(transform.position, it.point))
            {
                closest = it;
            }
        }
        return closest;
    }
}