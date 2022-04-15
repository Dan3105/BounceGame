using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneMovement : MonoBehaviour
{
    [Header("Theme Handler")]
    public float speedRolling;
    public GameObject[] listScence;
    public float limitPoint, respawnPoint;

    //[Header("Button Control")]

    private void Awake()
    {
        SceneSystem.CheckSaveData();
        Time.timeScale = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        foreach (var scence in listScence)
            scence.transform.Translate(Vector2.left * speedRolling * Time.deltaTime);

        foreach(var scence in listScence)
            //vector2.left
            if(scence.transform.position.x < limitPoint)
                scence.transform.position = new Vector2(respawnPoint, scence.transform.position.y);
    }

    public void StartGame()
    {
        SceneSystem.PlayGame();
    }

    public void Continue()
    {
        SceneSystem.Continue();
    }
}
