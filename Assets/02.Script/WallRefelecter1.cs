using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRefelecter1 : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if( collision.collider.CompareTag("Player"))
       {
           PlayerMove move = collision.gameObject.GetComponent<PlayerMove>();
            move.foward = Vector3.right;
       }
    }

}
