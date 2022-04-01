using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [Header("Parameters")]
    public float distance;
    [SerializeField] public int plusScore;
    private bool isTaken;
    
    [Header("Components")]
    [SerializeField] Collider2D downCol;
    [SerializeField] GameObject[] CoinOn;
    [SerializeField] GameObject[] CoinOff;
    private GameObject player;

    private void OnEnable()
    {
        SwitchCoinOn(true);
        isTaken = false;
    }

    private void FixedUpdate()
    {
        if(!isTaken)
        {
            RaycastHit2D[] rays = Physics2D.RaycastAll(downCol.bounds.center, Vector2.up, distance);
            foreach (var hitter in rays)
            {
                if (hitter.collider.CompareTag("Coin collider"))
                {
                    GameManager.Instance.UpdateScore(plusScore);
                    isTaken = true;
                    SwitchCoinOn(false);
                }

            }
        }
        
    }


    private void OnDrawGizmos()
    {

        Gizmos.DrawLine(downCol.bounds.center, downCol.bounds.center + Vector3.up * distance);

    }

    private void SwitchCoinOn(bool turn)
    {
        foreach (var child in CoinOn)
            child.SetActive(turn);
        foreach (var child in CoinOff)
            child.SetActive(!turn);
    }
}
