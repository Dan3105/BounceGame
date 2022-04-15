using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameScene : TemplateSingleton<GameScene>
{

    public TextMeshProUGUI livesUI;
    public TextMeshProUGUI timeUI;
    public TextMeshProUGUI scoreUI;

    public GameObject pausedTheme;
    public bool isPaused;

    public GameObject loseTheme;

    private void Start()
    {
        isPaused = false;
        loseTheme.SetActive(false);
    }

    private void Update()
    {
        if (pausedTheme.activeInHierarchy)
        {
            Time.timeScale = 0f;
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1f;
            isPaused = false;
        }
            
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

    public void TriggerLoseTheme()
    {
        loseTheme.SetActive(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuTheme()
    {
        SceneSystem.MenuTheme();
    }

    public void SavingData()
    {
        GameManager.Instance.SaveData();
    }
}
