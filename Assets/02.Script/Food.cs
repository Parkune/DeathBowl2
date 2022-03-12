using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    public int tresureNum = 1;

    public void Start()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(DeactiveDelay());
    }

    IEnumerator DeactiveDelay()
    {
        yield return new WaitForSecondsRealtime(5f);
        gameObject.SetActive(false);
        print("왜사라질까");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        { 
        UImanager.instance.TresureScoreUP(tresureNum);
        gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
    }
}
