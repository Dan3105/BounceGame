using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class SceneSystem
{
    public static GameStatusData savedData;
    public static bool isSavedBefore;
    public static bool isClickedContinue;

    public static void CheckSaveData()
    {
        if (PlayerPrefs.HasKey(SaveSystem.KEY_SYSTEM))
        {
            savedData = SaveSystem.LoadFile();
            if (savedData != null)
                isSavedBefore = true;
            else
                isSavedBefore = false;
        }
        else
            isSavedBefore = false;
    }

    public static void PlayGame()
    {
        SceneManager.LoadScene(1);
        isClickedContinue = false;
    }

    public static void Continue()
    {
        Debug.Log("Clicked");
        SceneManager.LoadScene(1);
        isClickedContinue = true;
    }

    public static void MenuTheme()
    {
        SceneManager.LoadScene(0);
    }


}
