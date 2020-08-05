using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeHeart : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        anim.Play("SwipePop", 0, 1);
    }

    void OnEnable()
    {
        anim?.Play("SwipePop");
    }

    void OnDisable()
    {
        anim?.Play("SwipeDepop");
    }
}