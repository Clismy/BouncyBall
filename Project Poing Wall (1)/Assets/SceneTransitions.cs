using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitions : MonoBehaviour
{
    [SerializeField] float transitionSpeed;

    bool fadeIn = false;
    bool fadeOut = false;

    SimpleBlit simpleBlit;
    Material mat;
    PressToStart pressToStart;
    ResetScene reset;

    void Awake()
    {
        pressToStart = GetComponent<PressToStart>();

        simpleBlit = GetComponent<SimpleBlit>();

        mat = simpleBlit.TransitionMaterial;
        mat.SetFloat("_Cutoff", 1);
    }

    void Start()
    {
        FadeOut();
    }

    void Update()
    {
        if (fadeOut)
        {
            float cutOff = mat.GetFloat("_Cutoff");
            cutOff = Mathf.MoveTowards(cutOff, 0f, Time.deltaTime * transitionSpeed);
            mat.SetFloat("_Cutoff", cutOff);

            if (cutOff == 0)
            {
                pressToStart.enabled = true;
                fadeOut = false;
            }
        }
        else if (fadeIn)
        {
            float cutOff = mat.GetFloat("_Cutoff");
            cutOff = Mathf.MoveTowards(cutOff, 1f, Time.deltaTime * transitionSpeed);
            mat.SetFloat("_Cutoff", cutOff);

            if (cutOff == 1)
            {
                reset.ReloadScene();
                fadeIn = false;
            }
        }
    }

    public void FadeIn(ResetScene resetScene)
    {
        reset = resetScene;
        fadeIn = true;
    }

    void FadeOut()
    {
        fadeOut = true;
    }
}