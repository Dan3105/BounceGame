using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Parameters")]
    private float timeCount;

    public int liveDefault = 3;
    [HideInInspector] public int liveCount;
    [HideInInspector] public int score;

    [Header("Components")]
    public GameObject[] playerList;
    public GameObject currPlayer;
    private void Awake()
    {
        Instance = this;
       
    }

    private void Start()
    {
        timeCount = 0;
        liveCount = liveDefault;
        currPlayer = playerList[0];
        GameScence.Instance.UpdateLive(liveCount);
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        GameScence.Instance.CountTime(timeCount);
    }

    public void UpdateLive(int i)
    {
        liveCount -= i;
        GameScence.Instance.UpdateLive(liveCount);
    }
}
