using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CustomCollision : MonoBehaviour
{
    [SerializeField] LayerMask nonTriggerLayer;
    [SerializeField] LayerMask triggerLayer;

    Vector2 dirReflect;
    Vector2 playerVel;

    bool updateVel = false;

    CircleCollider2D circle;
    [SerializeField] float distance;

    void Start()
    {
        circle = GetComponent<CircleCollider2D>();
    }

    public Vector2 PhysicalCollisions(Vector2 oldVel)
    {
        RaycastHit2D[] collHits = Physics2D.CircleCastAll(transform.position, circle.radius, oldVel.normalized, distance, nonTriggerLayer);

        if(collHits.Length > 0)
        {
            Vector2 averageNormal = collHits.Aggregate(collHits[0].normal, (A, N) => (A + N.normal) / 2, I => I);
            return Vector2.Reflect(oldVel, averageNormal.normalized);
        }

        return oldVel;
    }

    public void TriggerCollisions()
    {
        Collider2D[] collHits = Physics2D.OverlapCircleAll(transform.position, circle.radius, triggerLayer);

        
    }

    public void SetVelocity(Vector2 newVel)
    {
        playerVel = newVel;
    }

    public Vector2 GetUpdatedVelocity(Vector2 oldVel, float movementSpeed)
    {
        if(updateVel)
        {
            updateVel = false;
            return dirReflect * movementSpeed;
        }

        return oldVel;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        dirReflect = Vector2.Reflect(playerVel.normalized, other.contacts[0].normal);
        updateVel = true;
    }
}