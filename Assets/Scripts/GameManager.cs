using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("GamePlay parameters")]
    public int liveDefault = 3;
    public bool isPlaying;
    private float timeCount;

    
    [HideInInspector] public int liveCount;

    [HideInInspector] public int score;

    [Header("Components of player and effects")]
    public GameObject[] listPlayer;
    public GameObject currPlayer;
    public GameObject explodeEffect;
    public GameObject winningEffect;

    [Header("Coin Parmeters")]
    public GameObject coinObject;
    public List<GameObject> listCoin = new List<GameObject>();
    public int numberOfCoins;
    private void Awake()
    {
        Instance = this;
        
    }

    private void Start()
    {
        timeCount = 0;
        liveCount = liveDefault;
        score = 0;
        currPlayer = listPlayer[0];
        isPlaying = true;

        GameScence.Instance.UpdateLive(liveCount);
        GameScence.Instance.UpdateScore(score);

        for(int i = 0; i < listCoin.Count; i++)
        {
            int k = UnityEngine.Random.Range(0, listCoin.Count);
            //GameObject tempObj = listCoin[i];
            //listCoin[i] = listCoin[k];
            //listCoin[k] = tempObj;
            (listCoin[i], listCoin[k]) = (listCoin[k], listCoin[i]); // swap
        }

        int maxCoin = Mathf.Min(numberOfCoins, listCoin.Count);
        for(int i = 0; i < maxCoin; i++)
        {
            Instantiate(coinObject, listCoin[i].transform.position, listCoin[i].transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlaying)
        {
            timeCount += Time.deltaTime;
            GameScence.Instance.CountTime(timeCount);
        }       
    }

    public void UpdateLive(int i)
    {
        liveCount += i;
        if (liveCount == 0)
        {
            ActiveExplode();
            StopPlaying();
            currPlayer.SetActive(false);
        }
            
        GameScence.Instance.UpdateLive(liveCount);
        
    }

    private void ActiveExplode()
    {
        GameObject expoding = Instantiate(explodeEffect, currPlayer.transform.position, explodeEffect.transform.rotation);
        Destroy(expoding, expoding.GetComponent<ParticleSystem>().main.duration);
        currPlayer.SetActive(false);
    }

    public void ActiveWinning()
    {
        GameObject winParticle = Instantiate(winningEffect, currPlayer.transform.position, winningEffect.transform.rotation);
        Destroy(winParticle, winParticle.GetComponent<ParticleSystem>().main.duration);
    }

    public void UpdateScore(int i)
    {
        score += i;
        Debug.Log(score);
        GameScence.Instance.UpdateScore(score);
    }

    public void StopPlaying()
    {
        isPlaying = false;
        currPlayer.GetComponent<Rigidbody2D>().velocity.Set(0, 0);
        currPlayer.GetComponent<Rigidbody2D>().Sleep();
    }
}
