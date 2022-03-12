using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseScript : MonoBehaviour
{
    // Start is called before the first frame update
    // 페이즈 마다 진행되어야 할 수심의 높이를 결정하는 매개변수
    public float waterHight;
    // 현재 수심을 결정하는 게임 오브젝트, 물체가 해당 오브젝트에 닿을 경우 파괴 
    public GameObject waterSurface;
    public WaitForSecondsRealtime waterSurfaceTime = new WaitForSecondsRealtime(1f);
    private float donwPosition;
    private float downScale;

    //목표 수심 전 까지 일정 주기로 내려간다.
    //그러기 위해선 현재 트랜스폼 - 현재와 목표 수심차 /30한 값을 1초 마다 한 번씩 30번 실행한다. 

    // 블럭의 목표 크기를 설정하는 함수 
    // 현재 크기의 스케일 - 트랜스폼 시작 당시의 스케일 값과 목표의 차이를 /60한 값을 0.5초 마다 60번 실행한다.

    public float goalBlockScale;
    public GameObject blockObject;
    public GameObject phase2BlockObject;

    public WaitForSecondsRealtime blockScaleTime = new WaitForSecondsRealtime(0.25f);


    public float phaseTime; 
    public bool Phase0;
    public bool Phase1;
    public bool Phase2;
    public bool Phase3;
    public bool Phase4;
    public bool Phase5;

    public WaitForSecondsRealtime blockCreateTime = new WaitForSecondsRealtime(5f);

    IEnumerator respwanEnemys;
    IEnumerator respwanFoods;


    


    private void Update() 
    {
        phaseTime += Time.unscaledDeltaTime;
        //불값을 이용하여 1회만 플레이 되도록
        if(phaseTime >= 0 &&  Phase0 == false)
        {

            Phase0 = true;
            Phase0Fuction();
        } else if(phaseTime >= 15 && Phase1 == false)
        {
            print("Phase01");
            Phase1 = true;
            Phase1Fuction();
        } else if (phaseTime >= 30 && Phase2 == false)
        {
            print("Phase02");
            Phase2 = true;
            Phase2Fuction();
        } else if (phaseTime >= 45 && Phase3 == false)
        {
            print("Phase03");
            Phase3 = true;
            Phase3Fuction();
        } else if (phaseTime >= 60 && Phase4 == false)
        {
            print("Phase04");
            Phase4 = true;
            Phase4Fuction();
        } else if (phaseTime >= 75 && Phase5 == false)
        {
            print("Phase05");
            Phase5 = true;
            Phase5Fuction();
        }
    }

    IEnumerator waterSurfaceDown(float donwPosition)
    {
        while(waterSurface.transform.position.y >= waterHight)
        {
            //내려가는 함수
            yield return waterSurfaceTime;
            waterSurface.transform.position -= -Vector3.down*donwPosition;  
        }
    }
    public GameObject defaltRightBlock;
    IEnumerator BlockScaleDown(float downScale, GameObject block)
    {
        while(block.transform.localScale.y >= goalBlockScale)
        {
            yield return blockScaleTime;
            block.transform.localScale -= -Vector3.down*downScale;
            defaltRightBlock.transform.localScale -= -Vector3.down * downScale; 
        }
    }

    IEnumerator BlockCenterPosition(float downPosition)
    {
        while(waterSurface.transform.position.y - blockObject.transform.position.y<= 18)
        {
            yield return waterSurfaceTime;
            blockObject.transform.localPosition -= Vector3.down * downPosition;
            defaltRightBlock.transform.localPosition -= -Vector3.down * donwPosition;
        }
    }

    public float fixBlockScale;
    public bool blockCreate;
    public float CrateBlock;
    public GameObject destroyPosition;
    IEnumerator MoveBlockCreate(float fixBlockScale, float CrateBlock)
    {
        while(blockCreate)
        {
            float num = (waterSurface.transform.position.y) / CrateBlock;
            print(waterSurface.transform.position.y);
            float createNum = UnityEngine.Random.Range(1, CrateBlock);
            
            for (int i = 1; i <= createNum; i++)
            {
                    Vector3 respwanPos = new Vector3(destroyPosition.transform.position.x, (num * 0.5f + num * (i-1)), destroyPosition.transform.position.z);
                    GameObject wall = ObjectPooler.SpawnFromPool("WALL", respwanPos);
                    WallRefelecter wallRef = wall.GetComponent<WallRefelecter>();
                    wallRef.fixBlockScale = fixBlockScale;
                    wallRef.moveRangeDistance = num;
                    wallRef.isMove = true;
            }
            yield return blockCreateTime;
        }
    }
    public List<GameObject> blockRespwanPoint = new List<GameObject>();
    public List<Transform> blockTransform = new List<Transform>();
    public bool isCreateDelay;
    public GameObject destroyPosition2;
    //페이즈가 진행되는 동안
    //미리 넣어둔 8개의 포인트들이 생산 공장에 요청된 블럭 갯수에 따라서 균등 분배된다.
    //
    //랜덤 레인지를 통해 1에서 BlockAdd 사이에 값을 뽑아서 

    private int tempNum;
    IEnumerator MoveBlockCreater2(float BlockScale, float BlockAdd)
    {
        if(blockRespwanPoint.Count >=1)
        {
          blockTransform.RemoveRange(0, tempNum);
        }
        float pointposition = (waterSurface.transform.position.y-4) / BlockAdd;
        for (int i = 0; i <= BlockAdd; i++)
        {
            if(i%2 == 0)
            { 
                blockRespwanPoint[i].transform.position = new Vector3(destroyPosition.transform.position.x, (pointposition*0.5f + (i*pointposition- BlockScale)), destroyPosition.transform.position.z);
                blockTransform.Add(blockRespwanPoint[i].transform);
            }else
            {
                blockRespwanPoint[i].transform.position = new Vector3(destroyPosition2.transform.position.x, (pointposition * 0.5f + (i * pointposition - BlockScale)), destroyPosition2.transform.position.z);
                blockTransform.Add(blockRespwanPoint[i].transform);
            }
        }
        int blockTransformCount = blockTransform.Count;
        while(!isCreateDelay)
        {
            print(blockTransform.Count);
            float blockCount = UnityEngine.Random.Range(4, BlockAdd);
            for (int i = 0; i < blockCount; i++)
            {
                int randomepos = UnityEngine.Random.Range(0, blockTransform.Count);
                var pullSpace = ObjectPooler.SpawnFromPool("WALL", blockTransform[randomepos].position);
                WallRefelecter wallRef = pullSpace.GetComponent<WallRefelecter>();
                wallRef.moveRangeDistance = pointposition;
                wallRef.isMove = true;
                blockTransform.RemoveAt(randomepos);
            }
            tempNum = blockTransform.Count;
            yield return blockCreateTime;
            blockTransform.RemoveRange(0, blockTransform.Count);
            for (int i = 0; i <= BlockAdd; i++)
            {
                blockTransform.Add(blockRespwanPoint[i].transform);
            }
        }
    }

    //다른 방식으로 진행하자. Phase 실행 시 -> 특정 obj가 생성될 경우 그 친구의 특정 스크립트에 접근하여
    //반복 운동의 이동 범위에 대한 min/max값을 지정해준다.
    //그러기 위해서는 1. 오브젝트 풀링 진행. 2.그 값에 이동 민 맥스값 전달 -> 스타트 시 해당 오브젝트의 코룬틴 진행하도록 전달.
    //몇초 마다 기존 오브젝트를 숨기고, 다시 생성하는 함수 실행. -> 오브젝트 풀링으로 돌아가는 건 해당 오브젝트가 책임짐
    //여기서 결정해야 할 건 -> 생성되어야 할 오브젝트의 갯수/생성 주기 마다 지금 수심을 확인/오브젝트 생성 갯수/2의 포지션에 오브젝트 생성
    //오브젝트가 생성되었다면, min max는 현재 각 오브젝트의 포지션에서 저 위의 값 ex 4였으면 4/1/2 에 생성되었다면 +-그값
    //일정 주기 마다 해야하기에 전달해야 할 변수는 오브젝트 생성 갯수, 주기는 동일, 
    // 새성 스케일값 정해주는 함수 필요
    //생성 위치는 양쪽 다 달라야 한다.
    //랜덤 위치 생성은 특정 포지션에 고정 위치를 설정해두고, 그 포지션 중 n개를 뽑아 생성, 겹치지 않도록 설정
    //다만 해당 큐브들은 생성 크기가 랜덤으로 설정된다. 
    public void Phase0Fuction()
    {
      enemyRespwan = true;
      foodRespwan = true;
      foodNumber = 4;
      enemyNumber = 1;
      respwanFoods = Respwanfood(foodNumber);
      respwanEnemys = RespwanEnemy(enemyNumber);
      StartCoroutine(respwanFoods);
      StartCoroutine(respwanEnemys);
    //  waterHight = 45;
      goalBlockScale = 30f;
    //  fixBlockScale = 8;
    //  CrateBlock = 2;
    //  blockCreate = true;
    //  donwPosition = (waterSurface.transform.position.y - waterHight)/30;
      downScale = (blockObject.transform.localScale.y - goalBlockScale)/30f;
      downScaleBlock = BlockScaleDown(downScale, blockObject);
        //  StartCoroutine(waterSurfaceDown(donwPosition));
      StartCoroutine(downScaleBlock);
        //  StartCoroutine(MoveBlockCreate(fixBlockScale, CrateBlock));
    }

    public void Phase1Fuction()
    {
        StopCoroutine(respwanFoods);
        StopCoroutine(respwanEnemys);
        StopCoroutine(downScaleBlock);
        foodNumber = 5;
        enemyNumber = 2;
        goalBlockScale = 25f;
        downScale = (blockObject.transform.localScale.y - goalBlockScale) / 60f;
        respwanFoods = Respwanfood(foodNumber);
        respwanEnemys = RespwanEnemy(enemyNumber);
        downScaleBlock = BlockScaleDown(downScale, blockObject);
        StartCoroutine(respwanFoods);
        StartCoroutine(respwanEnemys);
        StartCoroutine(downScaleBlock);
        TimeScaleChange(1.2f);
    }

    public void Phase2Fuction()
    {
        StopCoroutine(respwanFoods);
        StopCoroutine(respwanEnemys);
        StopCoroutine(downScaleBlock);
        foodNumber = 6;
        enemyNumber = 3;
        goalBlockScale = 20f;
        waterHight = 30;
        downScale = (blockObject.transform.localScale.y - goalBlockScale) / 60f;
        donwPosition = (waterSurface.transform.position.y - waterHight) / 30;
        respwanFoods = Respwanfood(foodNumber);
        respwanEnemys = RespwanEnemy(enemyNumber);
        downScaleBlock = BlockScaleDown(downScale, blockObject);
        surfaceDown = waterSurfaceDown(donwPosition);
        StartCoroutine(respwanFoods);
        StartCoroutine(respwanEnemys);
        StartCoroutine(downScaleBlock);
        StartCoroutine(surfaceDown);
        StartCoroutine(BlockCenterPosition(donwPosition));
        TimeScaleChange(1.4f);
        Defultblock.Move = true;
        Defultblock.moveRangeDistance = waterSurface.transform.position.y*0.2f;
        Defultblock.wallSpeed = 1.5f;
        DefultblockRight.Move = true;
        DefultblockRight.moveRangeDistance = waterSurface.transform.position.y * 0.2f;
        DefultblockRight.wallSpeed = 1.5f;
    }

    public void Phase3Fuction()
    {
        StopCoroutine(respwanFoods);
        StopCoroutine(respwanEnemys);
        StopCoroutine(downScaleBlock);
        foodNumber = 7;
        enemyNumber = 4;
        goalBlockScale = 15f;
        waterHight = 25;
        downScale = (blockObject.transform.localScale.y - goalBlockScale) / 60f;
        donwPosition = (waterSurface.transform.position.y - waterHight) / 30;
        respwanFoods = Respwanfood(foodNumber);
        respwanEnemys = RespwanEnemy(enemyNumber);
        downScaleBlock = BlockScaleDown(downScale, blockObject);
        surfaceDown = waterSurfaceDown(donwPosition);
        StartCoroutine(respwanFoods);
        StartCoroutine(respwanEnemys);
        StartCoroutine(downScaleBlock);
        StartCoroutine(surfaceDown);
        TimeScaleChange(1.6f);
        Defultblock.moveRangeDistance = waterSurface.transform.position.y * 0.25f;
        Defultblock.wallSpeed = 2f;
        Defultblock.guidLine = 1f;
        DefultblockRight.moveRangeDistance = waterSurface.transform.position.y * 0.25f;
        DefultblockRight.wallSpeed = 2f;
        DefultblockRight.guidLine = 1f;
    }

    public void Phase4Fuction()
    {
        StopCoroutine(respwanFoods);
        StopCoroutine(respwanEnemys);
        StopCoroutine(downScaleBlock);
        StopCoroutine(surfaceDown);
        foodNumber = 8;
        enemyNumber = 5;
        goalBlockScale = 5f;
        downScale = (blockObject.transform.localScale.y - goalBlockScale) / 60f;
        respwanFoods = Respwanfood(foodNumber);
        respwanEnemys = RespwanEnemy(enemyNumber);
        downScaleBlock = BlockScaleDown(downScale, blockObject);
        StartCoroutine(respwanFoods);
        StartCoroutine(respwanEnemys);
        StartCoroutine(downScaleBlock);
        TimeScaleChange(2f);
        Defultblock.moveRangeDistance = waterSurface.transform.position.y * 0.3f;
        Defultblock.wallSpeed = 3f;
        Defultblock.guidLine = 3f;
        DefultblockRight.moveRangeDistance = waterSurface.transform.position.y * 0.3f;
        DefultblockRight.wallSpeed = 3f;
        DefultblockRight.guidLine = 3f;
    }

    public void Phase5Fuction()
    {
        StopCoroutine(respwanFoods);
        StopCoroutine(respwanEnemys);
        StopCoroutine(downScaleBlock);
        StopCoroutine(BlockCenterPosition(donwPosition));
        foodNumber = 9;
        enemyNumber = 5;
        respwanFoods = Respwanfood(foodNumber);
        respwanEnemys = RespwanEnemy(enemyNumber);
        blockCreateScale = 1;
        blockNum = 6;
        blockFactory2 = MoveBlockCreater2(blockCreateScale, blockNum);
        StartCoroutine(blockFactory2);
        StartCoroutine(respwanFoods);
        StartCoroutine(respwanEnemys);
        TimeScaleChange(2.5f);
        Destroy(blockObject);
        Destroy(defaltRightBlock);
        blockCreate = true;
    }
    
    public GameObject spwanPoint;

    public Vector3 originPos;

    public WaitForSecondsRealtime respwanTime = new WaitForSecondsRealtime(3f);

    private int foodNumber;
    private int enemyNumber;
    IEnumerator downScaleBlock;
    IEnumerator surfaceDown;
    IEnumerator blockFactory2;
    DeafultWall Defultblock;
    DeafultWall DefultblockRight;
    private int blockNum;
    private int blockCreateScale;
    private void Start()
    {
        originPos = waterSurface.transform.position;
        respwanFoods = Respwanfood(foodNumber);
        respwanEnemys = RespwanEnemy(enemyNumber);
        downScaleBlock = BlockScaleDown(downScale, blockObject);
        surfaceDown = waterSurfaceDown(donwPosition);
        blockFactory2 = MoveBlockCreater2(blockCreateScale, blockNum);
        Defultblock = blockObject.GetComponent<DeafultWall>();
        DefultblockRight = defaltRightBlock.GetComponent<DeafultWall>();
        startGame = true;
        StartCoroutine(itemRespwan());
        for (int i = 0; i < blockRespwanPoint.Count; i++)
        {
            blockTransform.Add(blockRespwanPoint[i].transform);
        }
    }
    private Vector3 GetRandomSpawnPosition()
    {
        float px = spwanPoint.transform.position.x;
        float py = spwanPoint.transform.position.y - (originPos.y - waterSurface.transform.position.y) ;
        float halfScaleX = spwanPoint.transform.localScale.x * 0.5f;
        float halfScaleY = (spwanPoint.transform.localScale.y - (originPos.y - waterSurface.transform.position.y))* 0.5f;

        float minX = px - halfScaleX; float maxX = px + halfScaleX;
        float minY = py - halfScaleY; float maxY = py + halfScaleY;

        float x = UnityEngine.Random.Range(minX, maxX);
        float y = UnityEngine.Random.Range(minY, maxY);
        float z = spwanPoint.transform.position.z;

        Vector3 origin = new Vector3(x, y, z);

        return origin;
    }

    public bool enemyRespwan;
    IEnumerator RespwanEnemy(int respawnNum)
    {
        WaitForSeconds createTime = new WaitForSeconds(0.8f);
        while (enemyRespwan)
        {
            yield return respwanTime;
            for (int i = 0; i < respawnNum; i++)
            {
                GameObject ENEMY = ObjectPooler.SpawnFromPool("ENEMY", GetRandomSpawnPosition());
                yield return createTime;
            }
            
        }
    }

    public bool foodRespwan;
    IEnumerator Respwanfood(int foodNum)
    {
        WaitForSeconds createTime = new WaitForSeconds(0.5f);
        while(foodRespwan)
        {
            yield return respwanTime;
            for (int i = 0; i < foodNum; i++)
            {
                GameObject food = ObjectPooler.SpawnFromPool("FOOD", GetRandomSpawnPosition());
                yield return createTime;
            }
        }
    }
    public void TimeScaleChange(float changeNum)
    {
        Time.timeScale = changeNum;
    }

    public bool startGame;
    WaitForSecondsRealtime respwanItem = new WaitForSecondsRealtime(10f);
    IEnumerator itemRespwan()
    {
        while(startGame)
        {
            float itemNum = UnityEngine.Random.Range(1, 3);
            yield return respwanItem;
            if (itemNum ==1 )
            {
                GameObject time = ObjectPooler.SpawnFromPool("TIME", GetRandomSpawnPosition());
            } else if (itemNum == 2)
            {
                GameObject jewel = ObjectPooler.SpawnFromPool("JEWEL", GetRandomSpawnPosition());
            }
        }
    }
}
