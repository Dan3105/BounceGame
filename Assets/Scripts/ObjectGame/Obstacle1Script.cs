using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle1Script : MonoBehaviour
{
    [Header("Component")]
    public Collider2D col;
    private Collider2D playerCol;
    [Header("Parameters")]
    public bool isVertical; // true vertical, false horizon
    public float speed;
    public float range;
    private float maxRange;
    private float minRange;
    public float originalPos;
    public float goalPos;
    public float coolDown;
    [Range(2f, 10f)] 
    public float forcePush;
    private int dir;



    private Vector2 dirMove;
    // Start is called before the first frame update
    void Awake()
    {
        dir = range > 0 ? 1 : -1;
        originalPos = isVertical ? transform.position.x : transform.position.y;
        goalPos = originalPos + range;
        maxRange = Mathf.Max(originalPos, goalPos);
        minRange = Mathf.Min(originalPos, goalPos);
        dirMove = isVertical ? Vector2.right : Vector2.up;
        coolDown = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        coolDown -= Time.deltaTime;
        transform.Translate(dirMove * dir * speed * Time.deltaTime);
        if (isVertical)
        {
            if (transform.position.x >= maxRange || transform.position.x <= minRange)
            {
                float change = transform.position.x > maxRange ? maxRange : minRange;
                transform.position = new Vector2(change, transform.position.y);
                dir *= -1;
            }
                
        }
        else
        {
            if (transform.position.y >= maxRange || transform.position.y <= minRange)
            {
                float change = transform.position.y > maxRange ? maxRange : minRange;
                transform.position = new Vector2(transform.position.x, change);
                dir *= -1;
            }
        }
            
                

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && coolDown < 0f && GameManager.Instance.isPlaying)
        {
            playerCol = collision.gameObject.GetComponent<Collider2D>();
            int dir = collision.collider.bounds.center.x > col.bounds.center.x ? -1 : 1;
            GameManager.Instance.UpdateLive(-1);
            StartCoroutine("IgnorePlayer");
            GameManager.Instance.PlayHitting();
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * forcePush * dir, ForceMode2D.Impulse);
            
            coolDown = 0.3f;

        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (isVertical)
            Gizmos.DrawLine((Vector2)transform.position, new Vector2(transform.position.x + range, transform.position.y));
        else
            Gizmos.DrawLine((Vector2)transform.position, new Vector2(transform.position.x, transform.position.y + range));
    }

    IEnumerator IgnorePlayer()
    {
        Physics2D.IgnoreCollision(playerCol, col, true);
        yield return new WaitForSeconds(0.4f);
        Physics2D.IgnoreCollision(playerCol, col, false);
    }
}
