using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingWall : MonoBehaviour
{
    [SerializeField] Animator anim;

    public void Blink()
    {
        anim.Play("Light Up");
    }
}