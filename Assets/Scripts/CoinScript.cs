using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] Collider2D downCol;
    public float distance;
    private GameObject player;

    private void FixedUpdate()
    {
        //RaycastHit2D ray = Physics2D.Raycast(downCol.bounds.center.TransformPoint(Vector3.zero), Vector2.up, distance);
        //if(ray)
        //{
        //    if(ray.collider.CompareTag("Coin collider"))
        //    {
        //        Debug.Log("Passed");
        //    }
        //}
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(downCol.transform.TransformPoint(Vector3.zero), downCol.transform.TransformPoint(Vector3.zero) + Vector3.up * distance);
    }
}
