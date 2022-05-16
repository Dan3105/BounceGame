using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumperScript : MonoBehaviour
{
    public PlayerData dataSwitch;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            GameManager.Instance.PlayHitting();
            BallController.Instance.ReInitPlayer(dataSwitch);
        }    
    }
}
