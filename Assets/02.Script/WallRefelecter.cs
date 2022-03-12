using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRefelecter : MonoBehaviour
{
    private Transform thisTR;
    private Vector3 reflectionVec;
    private Vector3 startPos;
    private Vector3 distPos;
    private float wallSpeed;
    public float fixBlockScale;
    public float moveRangeDistance;

/*    private void Start()
    {
        thisTR = this.transform;
        if (thisTR.position.x < 0)
        {
            reflectionVec = Vector3.left;
        }
        else if(thisTR.position.x > 0)
        {
            reflectionVec = Vector3.right;
        }
        startPos = transform.position;
        this.transform.localScale = new Vector3(thisTR.localScale.x , fixBlockScale, thisTR.localScale.z );
        distPos = startPos;
    }*/

    private void OnEnable()
    {

        
         
        this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, fixBlockScale, this.gameObject.transform.localScale.z);
        if (this.gameObject.transform.position.x < 0)
        {
            reflectionVec = Vector3.left;
        }
        else if (this.gameObject.transform.position.x > 0)
        {
            reflectionVec = Vector3.right;
        }
        startPos = transform.position;        
        distPos = startPos;
        StartCoroutine(DeactiveDelay());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if( collision.collider.CompareTag("Player"))
       {
           PlayerMove move = collision.gameObject.GetComponent<PlayerMove>();
            move.foward = reflectionVec;
       }
    }

    public bool isMove;
    private void Update()
    {
        if(isMove)
        { 
         distPos.y += moveRangeDistance* 0.0025f * Mathf.Sin(Time.unscaledTime);
         this.gameObject.transform.position = distPos;
        }
    }

    IEnumerator DeactiveDelay()
    {
        yield return new WaitForSecondsRealtime(5f);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
    }
}
