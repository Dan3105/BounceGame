using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constrant1Script : MonoBehaviour
{
    Collider2D col;
    public float distanceMove;
    // Start is called before the first frame update
    void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.UpdateLive(-1);
        }
    }
}
