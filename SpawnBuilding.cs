using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;

public class SpawnBuilding : MonoBehaviour

{

    public GameObject[] spawnPrefab;

    private Transform lineStart, lineEnd;

    // public Transform lineStart;
    public bool spawning = false;
    public float spawnFrequency = 3f;
    public float spawnTimer = 2f;


    // 불러올 게임오브젝트 리스트 선언
    private List<GameObject> spawnPrefabs = new List<GameObject>();


    // 생성한 빌딩의 게임오브젝트를 추적하시 위한 리스트 선언
    public List<GameObject> buildingPrebs = new List<GameObject>();

    int maxBuilding = 500;
    int buildingCount = 0;

    // building이 스폰되어 있는가? 기본값은 안되어있다. 
    public bool isSpawn = false;


    // 지도상의 블럭 즉, 4지점을 추출하기 위한 gameobject 르 순서대로 묶은 리스트 값 선언
    public List<GameObject> blocks = new List<GameObject>();

    // 총 81개의 블for (int i = 0; i < spawnPrefabs.Count + 1; i++)
    public List<Vector3> interSecPos = new List<Vector3>();
    //public List<Vector3> p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24p, p25,
    //p26, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45p, p46, p47, p48, p49, p50, p51, p52, p53,
    //p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76,
    //p77, p78, p79, p80, p81 = new List<Vector3>();

    // 블럭들의 중간 위치값을 모두 추출하여 리스트에 담음. 이것이 블럭당 건물들의 생성 위치 중심지점이 됨 
    public List<Vector3> spawnLocations = new List<Vector3>();


    public void StartSpawn()
    {
        // 빌딩이 위치할 지점의 중심지점값 계산 > spawnLocations
        SpawnPos_FindIntersection();

        //Debug.Log("Start spawn building");
        if (!isSpawn) // 즉, 스폰되어 있지 않았다면 스폰한다.
        {
            Debug.Log("Start spawn building");
            for (int j = 0; j <= maxBuilding; j++)
            {
                spawnTimer += Time.deltaTime;

                if (spawnTimer <= spawnFrequency)
                {
                    Spawn();
                    spawnTimer = 0f; // Reset
                }
            }
            // 스폰이 되면 true로 하고, else에서 ReSpawn을 실행하면 생성한 건물을 껐다 켰다 할 수 있음(재활용가능)
            isSpawn = true;
        }
        else // true 라면, 즉, 스폰되었다면 
        {
            ReSpawn();
        }

    }


    private void ReSpawn()
    {

        // 빌딩이 위치할 지점의 중심지점값 계산 > spawnLocations
        SpawnPos_FindIntersection();


        Debug.Log("Building Activated");

        for (int i = 0; i < buildingPrebs.Count; i++)
        {

            // 생성위치 추출
            foreach (Vector3 spPosition in spawnLocations)
            {
                buildingPrebs[i].transform.position = spPosition;
                // 꺼져있는 빌딩을 켜줌
                buildingPrebs[i].SetActive(true);
            }
        }
            

        Debug.Log("Building Activated all!");


    }


    private void Spawn()
    {

        // 4 지점의 조합 리스트값이 필요함. ---> Blocks로 리스트 선언으로 조합을 만드는 함수 개발해야 함

        // 빌딩 랜덤으로 추출
        int selection = Random.Range(0, spawnPrefab.Length);

        GameObject SelectedPrefab = spawnPrefab[selection];

        // 빌딩생성
        if (buildingCount <= maxBuilding)
        {
            // 생성위치 추출
            foreach (Vector3 spPosition in spawnLocations)
            {
                Vector3 spawnPos = spPosition;

                GameObject SpawnInstance = Instantiate(SelectedPrefab, spawnPos, Quaternion.identity);

                // Move new object to the calculated spawn location
                SpawnInstance.transform.position = spawnPos;

                // 생성한 빌딩을 리스트에 등록해서 추척관리할 거임
                buildingPrebs.Add(SpawnInstance);
            }

            buildingCount += 1;
        }

    }


    private Vector3 SpawnPosition()
    {

        // GioPos 태그를 가진 GameObject를 모두 찾아서 새로운 리스트에 하나씩 담는다. 이것은 위치값을 추적하기 위함이다.
        foreach (GameObject spawnp in GameObject.FindGameObjectsWithTag("GioPos"))
        {
            spawnPrefabs.Add(spawnp);
        }

        lineStart = spawnPrefabs[0].transform;
        lineEnd = spawnPrefabs[99].transform;


        // Get Value Ranges along the line (x, y, z)
        float xRange = lineEnd.position.x - lineStart.position.x;
        float yRange = lineEnd.position.y - lineStart.position.y;
        float zRange = lineEnd.position.z - lineStart.position.z;

        Vector3 spawnLocation = new Vector3(lineStart.position.x + (xRange * UnityEngine.Random.value),
                                            lineStart.position.y + (yRange * UnityEngine.Random.value),
                                            lineStart.position.z + (zRange * UnityEngine.Random.value));
        return spawnLocation;
    }



    // Find the intersection of two lines
    static Vector3 FindIntersection(Vector3 s1, Vector3 e1, Vector3 s2, Vector3 e2)
    {
        float a1 = e2.z - s1.z;
        float b1 = s1.x - e2.x;
        float c1 = a1 * s1.x + b1 * s1.z;

        float a2 = e1.z - s2.z;
        float b2 = s2.x - e1.x;
        float c2 = a2 * s2.x + b2 * s2.z;

        float delta = a1 * b2 - a2 * b1;
        //If lines are parallel, the result will be (NaN, NaN).
        return delta == 0 ? new Vector3(float.NaN, float.NaN, float.NaN)
            :new Vector3 ((b2 * c1 - b1 * c2) / delta, 0, (a1 * c2 - a2 * c1) / delta);
    }





    //// Find the intersection of two lines
    //static Vector3 FindIntersection(Vector3 s1, Vector3 e1, Vector3 s2, Vector3 e2)
    //{
    //    float a1 = e1.y - s1.y;
    //    float b1 = s1.x - e1.x;
    //    float c1 = a1 * s1.x + b1 * s1.y;

    //    float a2 = e2.y - s2.y;
    //    float b2 = s2.x - e2.x;
    //    float c2 = a2 * s2.x + b2 * s2.y;

    //    float delta = a1 * b2 - a2 * b1;
    //    //If lines are parallel, the result will be (NaN, NaN).
    //    return delta == 0 ? new Vector3(float.NaN, float.NaN, float.NaN)
    //        : new Vector3((b2 * c1 - b1 * c2) / delta, (a1 * c2 - a2 * c1) / delta);
    //}




    /// <summary>
    ///  이 문제를 해결해ㅑㅇ 함!!!!!!!
    /// </summary>
    // ArgumentOutOfRangeException: Index was out of range. Must be non-negative and less than the size of the collection Parameter name: index






    public void SpawnPos_FindIntersection()
    {

        // GioPos 태그를 가진 GameObject를 모두 찾아서 새로운 리스트에 하나씩 담는다. 이것은 위치값을 추적하기 위함이다.
        foreach (GameObject spawnp in GameObject.FindGameObjectsWithTag("GioPos"))
        {
            spawnPrefabs.Add(spawnp);
            //Debug.Log("check 1 !");
        }

        // 4 지점의 조합 리스트값이 필요함. ---> Blocks로 계산해야 ㅎ
        for (int i = 0; i < spawnPrefabs.Count - 11; i++)
        {

            if (i == 9 || i == 19 || i == 29 || i == 39 || i == 49 || i == 59 || i == 69 || i == 79 || i == 89 || i == 99 || i == 109)
            {
                continue;
            }

            interSecPos.Add(spawnPrefabs[i].transform.position);
            interSecPos.Add(spawnPrefabs[i + 1].transform.position);
            interSecPos.Add(spawnPrefabs[i + 10].transform.position);
            interSecPos.Add(spawnPrefabs[i + 11].transform.position);

        }

        Debug.Log(interSecPos.Count); // 324개로 4로 나누면 81개, 9X9의 블럭으로 구성되었기 때문ㅇ
        // 4개의 지점으로 블럭조합 리스트가 완성되면 (,,,)의 리스트로 그룹데이터 생성됨, 이것을 가지고 중간지점  == > interSecPos

        // 지도상의 블럭 즉, 4지점을 추출하기 위한 gameobject르 순서대로 묶은 리스트 값 선언 
        int k = 0;
        for (int i = 0; i <= interSecPos.Count/4-1; i++)
        {
            Debug.Log("interSecPos[k]" + interSecPos[k]); // 위치값이 잘 나옴
            Debug.Log("interSecPos[k]" + interSecPos[k+1]);
            Debug.Log("interSecPos[k]" + interSecPos[k+2]);
            Debug.Log("interSecPos[k]" + interSecPos[k+3]);
            Vector3 spawnLocation = FindIntersection(interSecPos[k], interSecPos[k + 1], interSecPos[k + 2], interSecPos[k + 3]);

            spawnLocations.Add(spawnLocation);

            //Debug.Log("FindIntersection : " + spawnLocation);

            k += 4;
        }







        //for (int i = 0; i <= blocks.Count; i += 4)
        //{

        //    Vector3 spawnLocation = FindIntersection(blocks[i].transform.position, blocks[i + 1].transform.position, blocks[i + 2].transform.position, blocks[i + 3].transform.position);

        //    spawnLocations.Add(spawnLocation);

        //    Debug.Log("FindIntersection : " + spawnLocation);
        //}

        // 블럭당 생성위치를 저장한 리스트, 값은 총 81개임 
        //return spawnLocations;


        // 백업
        //// Find the intersection of two lines--2 
        //// 두 선이 교차하는 지점 추출, 이 지점이 spawn base position이 된다. 그리고 네 지점의 면적을 구해야 한다. 그 안에 스폰이 되는지 체크하여 안에 있으면 스폰하고, 밖이면 스폰X
        //// 이제 아래 4 지점의 값을 gioPos에서 추출해서 넣으면 중심위치값을 찾아낼 거임. 거기서 스폰하면 됨. 
        //Vector3 spawnLocation = FindIntersection(new Vector3(4, 0, 0), new Vector3(6, 10, 0), new Vector3(0, 3, 0), new Vector3(10, 7, 0)); // 테스트 성
        //Debug.Log("FindIntersection : " + spawnLocation);
        //// 평행성선일 경우는 nan, nan 출력 
        ////Debug.Log("FindIntersection : " + FindIntersection(p(0f, 0f), p(1f, 1f), p(1f, 2f), p(4f, 5f)));
        ////
        //return spawnLocation;



    }


}
