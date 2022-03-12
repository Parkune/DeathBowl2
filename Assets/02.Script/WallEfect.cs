using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEfect : MonoBehaviour
{
    // Start is called before the first frame update

    public float scaleA = 5;
    public float scaleB;
    public GameObject Wall;
    public Vector3 pos;

    private void Start()
    {
        scaleB = Wall.gameObject.transform.localScale.y;
        pos = Wall.transform.position;
        print(scaleB);
        StartCoroutine("TheSmall");
    }

    IEnumerator TheSmall()
    {
        while (scaleA < scaleB)
        {
            scaleB -= 00.2f;
            yield return new WaitForSeconds(0.5f);
            Wall.transform.localScale = new Vector2(transform.localScale.x, scaleB);
            if (scaleA > scaleB)
            {
                print("계속 들어오나?");
                TheMoving();
                StopCoroutine("TheSmall");

            }
        }
    }


    void TheMoving()
    {
        StartCoroutine("TheMove");
    }
    public bool move = true;
    IEnumerator TheMove()
    {
        while (move)
        {
            Vector3 v = pos;
            v.y += 10 * Mathf.Sin(Time.time * 2);
            yield return new WaitForSeconds(0.03f);
            Wall.transform.position = v;
        }
    }

    public int levelNum;
    public bool respwanStart;
    public GameObject respwanTrigger;
    IEnumerator TheSetActive()
    {
        if (levelNum == 0)
        {
            int a = 4;
            //포문으로 특정 영역에서 이웃하지 않게 4개를 생성함
            //다시 사라지게 만듬 
            //몇초 후 이 동작을 반복하게 함
            //그럴려면 특정 영역을 8정도로 나눈 후 4개는 안나오게 4개는 나오개 해야함
            //그럴거면 미리 생성해놓는게 좋지 않을까? 그냥 껏다 켜주는것만 반복하는?
            //그럼 랜덤성이 훼손된다.
            //미리 여러게 만들어 놓고 그 중 하나만 열면 되지 않을까? 그럼 랜덤성이 있을거 아냐
            //그게 더 최적화에 도움이 되지 않을까?
            while (respwanStart)
            {

            }
            yield return new WaitForSeconds(2f);
        }
        else if (levelNum == 1)
        {
            while (respwanStart)
            {
                yield return new WaitForSeconds(0.03f);
            }
        }
        else if (levelNum == 2)
        {
            while (respwanStart)
            {
                yield return new WaitForSeconds(0.03f);
            }
        }

    }

    // 4개씩 랜덤 위치에 나타남
    // 3개씩 랜덤 위치에 나타남
    // 2~5곳에서 랜덤으로 나타남


}
