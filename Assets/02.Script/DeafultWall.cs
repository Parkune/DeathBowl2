using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeafultWall : MonoBehaviour
{
    private Transform thisTR;
    private Vector3 reflectionVec;
    public int sincosNum;
    void Start()
    {
        thisTR = this.transform;
        if (thisTR.position.x > 0)
        {
            reflectionVec = Vector3.left;
        }
        else if (thisTR.position.x < 0)
        {
            reflectionVec = Vector3.right;
        }
        startPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerMove move = collision.gameObject.GetComponent<PlayerMove>();
            move.foward = reflectionVec;
            move.StartFlip();
        }
    }
    private Vector3 startPos;
    //private Vector3 distPos;
    public float wallSpeed;
    public float moveRangeDistance;
    public bool Move;
    public float guidLine;
    
    // Update is called once per frame
    void Update()
    {
        if(Move)
        {
            if(sincosNum ==1)
            { 
                Vector3 distPos = startPos;
                distPos.y += moveRangeDistance * Mathf.Sin(Time.unscaledTime* wallSpeed)-guidLine;
                this.transform.position = distPos;
            } else if (sincosNum == 2)
            {
                Vector3 distPos = startPos;
                distPos.y += moveRangeDistance * Mathf.Cos(Time.unscaledTime * wallSpeed) - guidLine;
                this.transform.position = distPos;
            }
        }
    }
}
