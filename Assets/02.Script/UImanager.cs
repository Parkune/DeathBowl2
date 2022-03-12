using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UImanager : MonoBehaviour
{

    public Slider engineSlider;
    private int engineEnegy;
    private int tresureScore;
    public GameObject frezeEfect;
    public TMP_Text shipTresureText;
    public TMP_Text upSideTresureText;
    public int MaxValue;
    public int MinValue;
    public WaitForSecondsRealtime waitTime = new WaitForSecondsRealtime(1f);
    public bool gameStart;
    public float time;
    DOTweenTMPAnimator animator;
    Tween tween;
    IEnumerator enegyDown;
    public static UImanager instance;

    // Start is called before the first frame update
    void Start()
    {
        tresureScore = 0;
        engineSlider.maxValue = MaxValue;
        engineSlider.minValue = MinValue;
        engineSlider.value = MaxValue;
        engineEnegy = MaxValue;
        gameStart = true;
        enegyDown = EnegyDownCo();
        EnegyDown();
        instance = this;
        shipTresureText.text = "0";
        animator = new DOTweenTMPAnimator(shipTresureText);
    }



    public void TimeSwich()
    {
        StartCoroutine(TimeSwichDown());
    }

    IEnumerator TimeSwichDown()
    {
        time = Time.timeScale;
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = time;
    }


    public void EnegyUp(int Enegy)
    {
        engineEnegy += Enegy;
        if(engineEnegy < 30)
        {
            engineEnegy = 30;
        }
        engineSlider.DOValue(engineEnegy, 1).Play();
    }

    void EnegyDown()
    {
        StartCoroutine(enegyDown);
    }

    public void StopEngine()
    {
        StartCoroutine(stopEngine());
    }

    IEnumerator stopEngine()
    {
        StopCoroutine(enegyDown);
        frezeEfect.SetActive(true);
        yield return new WaitForSecondsRealtime(5f);
        StartCoroutine(enegyDown);
        frezeEfect.SetActive(false);
    }


    public void TresureScoreUP(int tresureNum)
    {
        tresureScore += tresureNum;
        shipTresureText.text = tresureScore.ToString();
        upSideTresureText.text = tresureScore.ToString();
      
        tween = animator.DOShakeCharScale(0, 1f, new Vector3(0.5f, 0.5f, 0.5f), 3, 10, true);
        Color cor = Random.ColorHSV(0, 1, 0.5f, 1, 1, 1);
        //tween = animator.DOColorChar(0, cor, 2);
        shipTresureText.DOColor(cor, 1);
    }

    IEnumerator EnegyDownCo()
    {
        while(gameStart)
        { 
          yield return waitTime;
            engineEnegy -= 1;
            engineSlider.DOValue(engineEnegy, 1).Play();
        }
    }
}
