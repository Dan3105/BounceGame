using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Cinemachine;
public class GameManager : TemplateSingleton<GameManager>
{    
    [Header ("Current Mode Player")]
    public int currMode; // x
    public CinemachineVirtualCamera cam;
    [Header("GamePlay parameters")]
    public int liveDefault = 3;
    public int firstMode;
    public bool isPlaying;
    [HideInInspector] public float timeCount;

    [HideInInspector] public int liveCount;

    [HideInInspector] public int score;

    Vector2 savedPos;

    [Header("Components of player and effects")]
    // public GameObject[] listPlayer;
    public Transform startPos; 
    public PlayerData[] listPlayerData;// (list data)
    //public GameObject[] listPlayerObject;//x
    public PlayerData currData; //x 
    
    //public GameObject currPlayer; //x
    public GameObject explodeEffect;
    public GameObject winningEffect;

    [Header("Coin Parmeters")]
    public GameObject coinObject;
    public List<GameObject> listCoin = new List<GameObject>();
    public List<GameObject> keepTrackCoin = new List<GameObject>();
    public int numberOfCoins;


    private void Start()
    {
        if (SceneSystem.isClickedContinue)
        {
            LoadData();
        }
        else
        {
            //set default
            timeCount = 0;
            liveCount = liveDefault;

            PlayerPrefs.DeleteAll();
            //test PlayerPrefbs
            score = 0;

            BallController.Instance.currentControl.SetActive(true);
            if(firstMode >= listPlayerData.Length)
            {
                currData = listPlayerData[0];
                currMode = 0;
            }
            else
            {
                currData = listPlayerData[firstMode];
                currMode = firstMode;
            }

            BallController.Instance.currentControl.transform.position = startPos.position;

            //Debug.Log(currPlayer.name);
            BallController.Instance.ReInitPlayer(currData);
            isPlaying = true;

            GameScene.Instance.UpdateLive(liveCount);
            GameScene.Instance.UpdateScore(score);

            //Set coin random
            for (int i = 0; i < listCoin.Count; i++)
            {
                int k = UnityEngine.Random.Range(0, listCoin.Count);
                (listCoin[i], listCoin[k]) = (listCoin[k], listCoin[i]); // swap
            }

            int maxCoin = Mathf.Min(numberOfCoins, listCoin.Count);

            for (int i = 0; i < maxCoin; i++)
            {
                GameObject temp = Instantiate(coinObject, listCoin[i].transform.position, listCoin[i].transform.rotation);
                keepTrackCoin.Add(temp);
            }
            //}
            //Debug.Log(currMode);
        }
        //set camera
        UpdateCamera(BallController.Instance.currentControl);
    }
    // Update is called once per frame
    void Update()
    {
        if(isPlaying && !GameScene.Instance.isPaused)
        {
            timeCount += Time.deltaTime;
            GameScene.Instance.CountTime(timeCount);
        }       
    }

    public void UpdateLive(int i)
    {
        liveCount += i;
        if (liveCount == 0)
        {
            StopPlaying();
            BallController.Instance.currAnim.Play(currData.animationDestroy);
            
            SceneSystem.isClickedContinue = false;
            PlayerPrefs.DeleteKey(SaveSystem.KEY_SYSTEM);
            StartCoroutine("TriggerLoseScene");
        }
            
        GameScene.Instance.UpdateLive(liveCount);
        
    }

    public void ActiveExplode()
    {
        GameObject expoding = Instantiate(explodeEffect,
            BallController.Instance.currentControl.transform.position, explodeEffect.transform.rotation);
        Destroy(expoding, expoding.GetComponent<ParticleSystem>().main.duration);
    }

    public void ActiveWinning()
    {
        GameObject winParticle = Instantiate(winningEffect,
            BallController.Instance.currentControl.transform.position, winningEffect.transform.rotation);
        Destroy(winParticle, winParticle.GetComponent<ParticleSystem>().main.duration);
    }

    public void UpdateScore(int i)
    {
        score += i;

        GameScene.Instance.UpdateScore(score);
    }

    public void StopPlaying()
    {
        isPlaying = false;
        BallController.Instance.rg2d.velocity.Set(0, 0);
        BallController.Instance.rg2d.Sleep();
    }

    private IEnumerator TriggerLoseScene()
    {
        yield return new WaitForSeconds(1f);
        GameScene.Instance.TriggerLoseTheme();
    }

    public void UpdateCamera(GameObject mode)
    {
        cam.Follow = mode.transform;
        cam.LookAt = mode.transform;
    }

    public void SaveData()
    {

        SaveSystem.SavePlayer();
        SceneSystem.MenuTheme();

    }

    private void LoadData()
    {
        GameStatusData savedData = SceneSystem.savedData;
        if (savedData == null)
            return;
        else
        {
            
            //load status
            liveCount = savedData.liveRemain;
            timeCount = savedData.timeCount;
            score = savedData.score;

            //load character
            currMode = savedData.currentMode;
            currData = listPlayerData[currMode];
            BallController.Instance.ReInitPlayer(currData);
            GameObject refPlayer = BallController.Instance.currentControl;
            refPlayer.transform.position = new Vector3(savedData.position[0],
                savedData.position[1], savedData.position[2]);

            //load Coin
            for (int i = 0; i < savedData.numCoins; i++)
            {
                Vector2 position = new Vector2(savedData.coinPosition[i].x,
                    savedData.coinPosition[i].y);
                GameObject coin = Instantiate(coinObject, position, coinObject.transform.rotation);

                if (savedData.isTaken[i])
                    coin.GetComponent<CoinScript>().SwitchCoinOn(false);

                keepTrackCoin.Add(coin);
            }
            
            //load UI
            GameScene.Instance.UpdateLive(liveCount);
            GameScene.Instance.UpdateScore(score);
            isPlaying = true;
        }

    }


}
