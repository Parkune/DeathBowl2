using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineEnegy : MonoBehaviour
{
    public int Enegy = 5;
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(DeactiveDelay());
    }

    IEnumerator DeactiveDelay()
    {
        yield return new WaitForSecondsRealtime(5f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UImanager.instance.EnegyUp(Enegy);
        gameObject.SetActive(false);
    }


    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
    }
}
