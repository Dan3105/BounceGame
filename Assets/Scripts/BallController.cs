using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Collider2D col;
    [SerializeField] Rigidbody2D rg2d;
    [Header("Moving Components")]
    public float speed;
    public float smoothMove;
    private float xMovement;
    private Vector2 newVelo;

    [Header("Jumping Components")]
    public float jumpForce;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float checkDistance;
    private bool isGround;
    [SerializeField] float bufferJump = 0.2f;
    private float bufferCounter;
    [SerializeField] float coyoteJump = 0.2f;
    private float coyoteCounter;

    [Header("Slope Checking Components")]
    public float slopeDistance;
    private Vector2 slopePerpendicular;
    private float slopeAngle;
    private float slopeOldAngle;
    private bool onSlope;

    private void Awake()
    {
        rg2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.Raycast(transform.position, Vector2.down, checkDistance, whatIsGround);
        SlopeCheck();

        Moving();

        Jumping();

        
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = (Vector2)transform.position - new Vector2(0f, col.bounds.center.y / 2);
        SlopeCheckVertical(checkPos);
    }

    void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D rayHorizon = Physics2D.Raycast(checkPos, Vector2.down, slopeDistance ,whatIsGround);

        if (rayHorizon)
        {
            slopePerpendicular = Vector2.Perpendicular(rayHorizon.normal).normalized;

            slopeAngle = Vector2.Angle(slopePerpendicular, Vector2.up);

            if (slopeAngle != 90)
            {
                onSlope = true;
            }
            else 
                onSlope = false;

            Debug.DrawRay(rayHorizon.point, slopePerpendicular, Color.green);
            Debug.DrawRay(rayHorizon.point, rayHorizon.normal, Color.black);
        }
    }

    void GetInput()
    {
        xMovement = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            bufferCounter = bufferJump;
        else
            bufferCounter -= Time.deltaTime;

        if (isGround || onSlope)
            coyoteCounter = coyoteJump;
        else
            coyoteCounter -= Time.deltaTime;
    }

    void Moving()
    {
        
        if (isGround && onSlope)
        {
            newVelo.Set(speed * slopePerpendicular.x * -xMovement, speed * slopePerpendicular.y * -xMovement);
            rg2d.velocity = Vector2.Lerp(rg2d.velocity, newVelo, smoothMove * 3 * Time.deltaTime);
        }
        else if (isGround && !onSlope)
        {
            newVelo.Set(speed * xMovement, 0f);
            rg2d.velocity = Vector2.Lerp(rg2d.velocity, newVelo, smoothMove * Time.deltaTime);
        }
        else if(!isGround)
        {
            newVelo.Set(speed * xMovement, rg2d.velocity.y);
            rg2d.velocity = Vector2.Lerp(rg2d.velocity, newVelo, smoothMove * 3 * Time.deltaTime);
        }
    }
    void Jumping()
    {
        //float powerJump = onSlope ? 1.4f : 1f;
        if(bufferCounter > 0f && coyoteCounter > 0f && (isGround || onSlope))
        {
            rg2d.AddForce(Vector2.up *jumpForce, ForceMode2D.Impulse);
            
            bufferCounter = 0f;
            coyoteCounter = 0f;
        }
        //if (rg2d.velocity.y > 0f && coyoteCounter > 0f)
        //{

        //}
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(transform.position, transform.position + Vector3.down * checkDistance);
    }
}
