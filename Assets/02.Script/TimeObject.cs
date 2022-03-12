using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        UImanager.instance.TimeSwich();
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        StartCoroutine(DeactiveDelay());
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
