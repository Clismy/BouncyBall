using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    float totalScore;
    public float finalTotalScore;
    bool popEffect = true;
    bool startPop = false;

    [SerializeField] Vector3 normalScaleUp, extraScaleUp;
    [SerializeField] float normalScore, extraScore;
    Vector3 currentScaleUp;

    [HideInInspector] public bool activate = false;

    public float TotalScore
    {
        get => totalScore;
        set
        {
            DetermentPop(value);
        }
    }
    [SerializeField] TextMeshProUGUI scoreText;

    public static Score instance;
    float startTime;

    void Awake()
    {
        PressToStart.OnStartGame += ActivateScore;

    }

    void OnDestroy()
    {
        PressToStart.OnStartGame -= ActivateScore;
    }

    void Start()
    {
        instance = this;    
    }

    void ActivateScore()
    {
        activate = true;
    }

    void OnLevelWasLoaded(int level)
    {
        if(instance != this)
        {
            Destroy(instance);
            instance = this;
        }
        totalScore = 0;
        startTime = Time.time;
    }

    void Update()
    {
        if (!activate) return;

        scoreText.text = (((int)(Time.time - startTime) / 5) * 5 + totalScore).ToString();
        finalTotalScore = int.Parse(scoreText.text);

        if (startPop)
        {
            if (popEffect)
            {
                Vector3 scale = scoreText.transform.localScale;

                scale = Vector3.MoveTowards(scale, currentScaleUp, Time.deltaTime * 10f);

                if (scale == currentScaleUp)
                {
                    popEffect = false;
                }
                scoreText.transform.localScale = scale;
            }
            else
            {
                Vector3 scale = scoreText.transform.localScale;

                scale = Vector3.MoveTowards(scale, Vector3.one, Time.deltaTime * 10f);

                if (scale == Vector3.one)
                {
                    startPop = false;
                    popEffect = true;
                }
                scoreText.transform.localScale = scale;
            }
        }
    }


    void DetermentPop(float newValue)
    {
        if (newValue == (totalScore + extraScore))
        {
            currentScaleUp = extraScaleUp;
        }
        
        else
        {
            currentScaleUp = normalScaleUp;
        }

        startPop = true;
        totalScore = newValue;
    }
}