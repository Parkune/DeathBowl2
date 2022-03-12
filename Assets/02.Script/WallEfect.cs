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
                print("��� ������?");
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
            //�������� Ư�� �������� �̿����� �ʰ� 4���� ������
            //�ٽ� ������� ���� 
            //���� �� �� ������ �ݺ��ϰ� ��
            //�׷����� Ư�� ������ 8������ ���� �� 4���� �ȳ����� 4���� ������ �ؾ���
            //�׷��Ÿ� �̸� �����س��°� ���� ������? �׳� ���� ���ִ°͸� �ݺ��ϴ�?
            //�׷� �������� �Ѽյȴ�.
            //�̸� ������ ����� ���� �� �� �ϳ��� ���� ���� ������? �׷� �������� ������ �Ƴ�
            //�װ� �� ����ȭ�� ������ ���� ������?
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

    // 4���� ���� ��ġ�� ��Ÿ��
    // 3���� ���� ��ġ�� ��Ÿ��
    // 2~5������ �������� ��Ÿ��


}
