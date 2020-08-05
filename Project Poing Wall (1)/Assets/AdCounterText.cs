using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdCounterText : MonoBehaviour
{
    TextMeshProUGUI adCounterText;
    void OnEnable()
    {
        adCounterText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        adCounterText.text = OfflineScoreBoard.getHighScores()?.adCounter.ToString();
    }
}