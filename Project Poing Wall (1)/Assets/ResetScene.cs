using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    [SerializeField] Transform gameOverUI;

    SceneTransitions sceneTransitions;

    void Awake()
    {
        MousePointer.OnPlayerDead += ActivateGameOverScreen;
        DeathByBottom.OnPlayerDead += ActivateGameOverScreen;

        sceneTransitions = Camera.main.GetComponent<SceneTransitions>();
    }

    void OnDestroy()
    {
        MousePointer.OnPlayerDead -= ActivateGameOverScreen;
        DeathByBottom.OnPlayerDead -= ActivateGameOverScreen;
    }

    void ActivateGameOverScreen()
    {
        gameOverUI.gameObject.SetActive(true);

        Score.instance.activate = false;

        if(OfflineScoreBoard.checkIfHighScore((int)Score.instance.finalTotalScore))
        {
            OfflineScoreBoard.setHighScore((int)Score.instance.finalTotalScore);
        }

        OfflineScoreBoard.SetAdCounter(--OfflineScoreBoard.getHighScores().adCounter);

        if(OfflineScoreBoard.getHighScores().adCounter <= 0)
        {
            AdScript.instance.PlayAD();
        }   
    }

    public void TryAgain()
    {
        sceneTransitions.FadeIn(this);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("2dBall 1");
    }
}