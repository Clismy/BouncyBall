using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathByBottom : MonoBehaviour
{
    public delegate void OnPlayer();
    public static OnPlayer OnPlayerDead;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "BALL")
        {
            
        }
        else if(collision.transform.tag == "Wall")
        {

        }
        else 
        {
            Destroy(collision.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "BALL")
        {
            //OnPlayerDeath?.Invoke();
            OnPlayerDead.Invoke();
        }
        else if(collision.transform.tag == "Wall")
        {

        }
        else if(collision.transform.tag != "BonusWall")
        {
            Destroy(collision.gameObject);
        }
    }
}