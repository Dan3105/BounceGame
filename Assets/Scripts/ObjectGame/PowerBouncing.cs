using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBouncing : MonoBehaviour
{
    public enum DIR { RIGHT, LEFT, TOP };
    public DIR dir;
    public float pushForce;
    Vector2 direction;

    private void Awake()
    {
        switch(dir)
        {
            case DIR.RIGHT:
                direction = Vector2.right;
                break;
            case DIR.LEFT:
                direction = Vector2.left;
                break;
            case DIR.TOP:
                direction = Vector2.up;
                break;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Rigidbody2D>().AddForce(direction * pushForce, ForceMode2D.Impulse);
        }    
    }
}
