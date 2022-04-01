using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScence : MonoBehaviour
{
    public static GameScence Instance;

    public TextMeshProUGUI livesUI;
    public TextMeshProUGUI timeUI;
    public TextMeshProUGUI scoreUI;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    public void UpdateLive(int live)
    {
        GameManager.Instance.liveCount = live;
        livesUI.text = "Live: " + GameManager.Instance.liveCount.ToString();
    }

    public void CountTime(float time)
    {
        float second = time % 60;
        float min = time / 60;

        string secondText = second >= 10 ? ((int)second).ToString() : "0" + ((int)second).ToString(); 
        string minText = min >= 10 ? ((int)min).ToString() : "0" + ((int)min).ToString();

        timeUI.text = minText + ":" + secondText;
    }

    public void UpdateScore(int score)
    {
        scoreUI.text = "Score: " + score.ToString();
    }
}
