using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowHighScore : MonoBehaviour
{
    TextMeshProUGUI highScoreText;
    void OnEnable()
    {
        highScoreText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        highScoreText.text = OfflineScoreBoard.getHighScores()?.row[0]?.score.ToString();
    }
}