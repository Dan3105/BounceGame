using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Animations;
[CreateAssetMenu(fileName = "Player Mode", menuName = "Ball Stats")]
public class PlayerData : ScriptableObject
{
    public new string name;
    public int indexVersion;
    public Sprite ballSprite;
    [Header("Moving")]
    public float speed;
    public float lerpSpeed;

    [Header("Jumping and Gravity")]
    public float jumpForce;
    public float rayGround;

    [Header("Animation action scence")]
    public AnimatorController animator;
    public string animationDestroy;
    public string animationStayIdle;

}
