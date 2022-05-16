using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class BallController : TemplateSingleton<BallController>
{
    [Header("Player Components")]
    public PlayerData currentData;
    public GameObject currentControl;
    public SpriteRenderer spriteRender;
    public CircleCollider2D col;
    public Rigidbody2D rg2d;
    [Header("Moving Components")]
    private float xMovement;
    private Vector2 newVelo;

    [Header("Jumping Components")]
    [SerializeField] LayerMask whatIsGround;
    private bool isGround;
    [SerializeField] float bufferJump = 0.2f;
    private float bufferCounter;
    [SerializeField] float coyoteJump = 0.2f;
    private float coyoteCounter;

    private bool jumping;

    [Header("Slope Checking Components")]
    private Vector2 slopePerpendicular;
    private float slopeAngle;
    private bool onSlope;

    [Header("Effects and Animations")]
    public GameObject explodeEffect;    
    public Animator currAnim;

    [Header("UI Components")]
    public ButtonScript leftKey;
    public ButtonScript rightKey;
    public ButtonScript jumpKey;

    private void OnEnable()
    {
        leftKey.clickDown.AddListener(GetInputKeyLeft);
        leftKey.clickUp.AddListener(DisableMoving);

        rightKey.clickDown.AddListener(GetInputKeyRight);
        rightKey.clickUp.AddListener(DisableMoving);

        jumpKey.clickDown.AddListener(GetJumpDown);
        jumpKey.clickUp.AddListener(GetJumpUp);


    }

    private void Update()
    {
        if (currentControl != null && (GameManager.Instance.isPlaying && !GameScene.Instance.isPaused))
            GetInput();
    }

    private void FixedUpdate()
    {
        if (currentControl != null && col != null)
        {
            isGround = Physics2D.Raycast(currentControl.transform.position, Vector2.down,
                        currentData.rayGround, whatIsGround);

            SlopeCheck();

            Moving();

            Jumping();
        }    
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = (Vector2)currentControl.transform.position - new Vector2(0f, col.bounds.center.y / 2);
        SlopeCheckVertical(checkPos);
    }

    void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D rayHorizon = Physics2D.Raycast(checkPos, Vector2.down, Mathf.Infinity, LayerMask.GetMask("Ground","Coin"));
       
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
        else
            onSlope = false;
    }

    void GetInput()
    {
        //Keyboard
        //xMovement = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) || jumping)
            bufferCounter = bufferJump;
        else
            bufferCounter -= Time.deltaTime;

        if (isGround || onSlope)
            coyoteCounter = coyoteJump;
        else
            coyoteCounter -= Time.deltaTime;
        //UI
        
    }

    void Moving()
    {
        
        if (isGround && onSlope)
        {
            newVelo.Set(currentData.speed * slopePerpendicular.x * -xMovement, currentData.speed * slopePerpendicular.y * -xMovement);
            rg2d.velocity = Vector2.Lerp(rg2d.velocity, newVelo,
                currentData.lerpSpeed * 3 * Time.deltaTime);
        }
        else if (isGround && !onSlope)
        {
            newVelo.Set(currentData.speed * xMovement, 0f);
            rg2d.velocity = Vector2.Lerp(rg2d.velocity, newVelo, 
                currentData.lerpSpeed * Time.deltaTime);
        }
        else if(!isGround)
        {
            newVelo.Set(currentData.speed * xMovement, rg2d.velocity.y);
            rg2d.velocity = Vector2.Lerp(rg2d.velocity, newVelo,
                currentData.lerpSpeed * 3 * Time.deltaTime);
        }
    }
    void Jumping()
    {
        
        //float powerJump = onSlope ? 1.4f : 1f;
        if(bufferCounter > 0f && coyoteCounter > 0f && (isGround || onSlope))
        {
            rg2d.AddForce(Vector2.up * currentData.jumpForce, ForceMode2D.Impulse);
            
            bufferCounter = 0f;
            coyoteCounter = 0f;
        }
        //if (rg2d.velocity.y > 0f && coyoteCounter > 0f)
        //{

        //}
    }

    public void GetInputKeyLeft()
    {

        xMovement = -1;
    }

    public void GetInputKeyRight()
    {
        xMovement = 1;
    }

    public void GetJumpDown()
    {
        jumping = true;
        
    }

    public void GetJumpUp()
    {
        jumping = false;
    }

    public void DisableMoving()
    {
        xMovement = 0;
    }

    private void OnDrawGizmos()
    {
        //if(currentControl != null)
        //{
        //    Gizmos.color = Color.black;
        //    Gizmos.DrawLine(currentControl.transform.position,
        //        currentControl.transform.position + Vector3.down * currentData.rayGround);
        //}
       
    }

    public void SetFalseObject()
    {

        currAnim.SetTrigger("isDead");
        currentControl.SetActive(false);
    }

    public void ReInitPlayer(PlayerData data)
    {
        currentData = data;

        //slow
        spriteRender.sprite = data.ballSprite;
        //spriteRender.sprite = data.ballSprite;
        Debug.Log(spriteRender.sprite);

        currAnim.runtimeAnimatorController = data.animator;

        GameManager.Instance.currMode = data.indexVersion;
        col.radius = data.ballSprite.bounds.extents.x > data.ballSprite.bounds.extents.y ?
    data.ballSprite.bounds.extents.x : data.ballSprite.bounds.extents.y;
        Debug.Log(col.radius);
    }
}
