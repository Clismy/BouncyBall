using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    [SerializeField] float rightLimit;
    [SerializeField] float leftLimit;
    [SerializeField] float speed;
    int direction = 1;

    void Start()
    {
        
    }

    void Update()
    {
        if(transform.position.x > rightLimit)
        {
            direction = -1;
        }
        else if(transform.position.x < leftLimit)
        {
            direction = 1;
        }

        Vector3 movement = Vector3.right * direction * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Vector3.right * leftLimit, 0.2f);
        Gizmos.DrawWireSphere(Vector3.right * rightLimit, 0.2f);
    }
}