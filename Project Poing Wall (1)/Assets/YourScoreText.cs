using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YourScoreText : MonoBehaviour
{
    TextMeshProUGUI yourScoreText;

    void OnEnable()
    {
        yourScoreText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        yourScoreText.text = Score.instance.finalTotalScore.ToString();
    }
}