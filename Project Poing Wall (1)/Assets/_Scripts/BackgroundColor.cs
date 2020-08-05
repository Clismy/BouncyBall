using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColor : MonoBehaviour
{
    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;
    // Start is called before the first frame update
    public void Start()
    {
        gradient = new Gradient();
        colorKey = new GradientColorKey[3];
        colorKey[0].color = new Color(0, 0, 1);
        colorKey[0].time = 0.0f;
        colorKey[1].color = new Color(1, 0, 1);
        colorKey[1].time = 0.2f;
        colorKey[2].color = new Color(0, 0, 1);
        colorKey[2].time = 0.4f;


        alphaKey = new GradientAlphaKey[3];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 0.2f;
        alphaKey[2].alpha = 1.0f;
        alphaKey[2].time = 0.4f;
        gradient.SetKeys(colorKey, alphaKey);



    }
    public Color getColor(float time)
    {

        time %= 0.4f;
        return gradient.Evaluate(time);
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.backgroundColor = getColor(Time.time/100);
    }
}
