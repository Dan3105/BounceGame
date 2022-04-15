using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameStatusData
{
    public int liveRemain;
    public float timeCount;
    public int score;
    public int currentMode;

    public float[] position;
    public int numCoins;
    public List<Vector2> coinPosition;
    public bool[] isTaken;

    public GameStatusData(int m)
    {
        liveRemain = GameManager.Instance.liveCount;
        timeCount = GameManager.Instance.timeCount;
        score = GameManager.Instance.score;


        currentMode = GameManager.Instance.currMode;

        position = new float[3];
        position[0] = BallController.Instance.currentControl.transform.position.x;
        position[1] = BallController.Instance.currentControl.transform.position.y;
        position[2] = BallController.Instance.currentControl.transform.position.z;

        //save current coins location and its status in game
        numCoins = GameManager.Instance.keepTrackCoin.Count;
        coinPosition = new List<Vector2>();
        isTaken = new bool[numCoins];
        for (int i = 0; i < numCoins; i++)
        {
            GameObject temp = GameManager.Instance.keepTrackCoin[i];
            if (temp.GetComponent<CoinScript>().isTaken)
                isTaken[i] = true;
            else
                isTaken[i] = false;

            coinPosition.Add(new Vector2(temp.transform.position.x, temp.transform.position.y));

        }
    }
}
