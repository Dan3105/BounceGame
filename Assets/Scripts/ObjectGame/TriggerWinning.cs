using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWinning : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coin collider"))
        {
            Debug.Log("Winnn");
            GameManager.Instance.StopPlaying();
            GameManager.Instance.ActiveWinning();
        }
    }
}
