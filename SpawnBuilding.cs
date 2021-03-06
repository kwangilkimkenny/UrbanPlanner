using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using System.Linq;
using System.Threading;
using UnityEngine.UI;

public class SpawnBuilding : MonoBehaviour

{
    // 처음 빌딩을 생성할 때 사용
    public GameObject[] spawnPrefab;

    // 블럭안의 그라운드 재질 종류별 프리핍
    //public GameObject asplatPrefab;


    // 다시 빌딩을 생성할 때 사용
    public GameObject[] newBuildings;

    private Transform lineStart, lineEnd;

    // public Transform lineStart;
    public bool spawning = false;
    public float spawnFrequency = 3f;
    public float spawnTimer = 2f;

    // 생성한 빌딩 수  - > UI에 표시
    public Text ContBDConuter;


    // Block Indicator
    [SerializeField]
    public GameObject BlockIndicatorPrefab;


    // 불러올 게임오브젝트 리스트 선언
    private List<GameObject> spawnPrefabs = new List<GameObject>();

    // spawnPrefabs_는 실제 spawn BD에서 게산할 값들임
    public List<GameObject> spawnPrefabs_ = new List<GameObject>();

    // 생성한 빌딩의 게임오브젝트를 추적하기 위한 리스트 선언
    public List<GameObject> buildingPrebs = new List<GameObject>();

    // 생성한 빌딩의 게임오브젝트를 추적하기 위한 리스트 선언  --- use this list values to manage swawned buildings
    public List<GameObject> ConsBuildings = new List<GameObject>();


    public int maxBuilding = 50;
    public int buildingCount = 0;

    float buildingFootPrint = 1f;

    // building이 스폰되어 있는가? 기본값은 안되어있다. 
    public bool isSpawn = false;


    // 지도상의 블럭 즉, 4지점을 추출하기 위한 gameobject 르 순서대로 묶은 리스트 값 선언
    public List<GameObject> blocks = new List<GameObject>();

    // 총 81개의 블for (int i = 0; i < spawnPrefabs.Count + 1; i++)
    public List<Vector3> interSecPos = new List<Vector3>();


    // 블럭들의 중간 위치값을 모두 추출하여 리스트에 담음. 이것이 블럭당 건물들의 생성 위치 중심지점이 됨 
    public List<Vector3> spawnLocations = new List<Vector3>();

    // 생성된 블럭들의 개별 면적 저장 리스트
    public List<float> allAreaValueList = new List<float>();

    // 블럭에 빌딩생성을 위한 면적범위의 원 반지름 값 리스트로 81개가 되어야 spawnBD_Raius_All
    public List<float> spawnBD_Raius_All = new List<float>();

    private bool state = true;

    // 블럭이 현재 81개임 -- 나중에 가변값을 변경 가능
    //public GameObject[] BlockIndicator_Array = new GameObject[81]; // 사용하지 않ㅇ
    public List<GameObject> BlockIndicator_List = new List<GameObject>();

    // 빌딩 면적 접근하기 - 빌딩에 적용한 BuildingSize 클래스 접근
    BuildingSize getBD_area;

    // Block당 건물 수 기록한 리스트
    public List<int> BuildingsInBlocks = new List<int>();

    // Start position of each blocks
    public List<Vector3> startPosEachBlock = new List<Vector3>();

    public List<GameObject> temp_list = new List<GameObject>();


    public float raycastDis = 100f;
    //public GameObject GioPrefab;
    public Vector3 itmPos;




    public void StartSpawn()
    {
        ResetAllBuilding();

        if (state == true) // 즉, 스폰되어 있지 않았다면 스폰한다.
        {
            SpawnPos_FindIntersection();

            Spawn();

            state = false;
        }
        else // true 라면, 즉, 스폰되었다면 
        {

            // 아예 새로 만들어 버린다.
            SpawnPos_FindIntersection();

            Spawn();

            state = true;
        }
    }


    public void ResetAllBuilding()
    {

        foreach (GameObject buildings in GameObject.FindGameObjectsWithTag("building"))
        {

            Destroy(buildings);
        }

        foreach (GameObject bloIndiCa in GameObject.FindGameObjectsWithTag("BlockIndicator"))
        {
            Destroy(bloIndiCa);
        }

        spawnPrefabs_.Clear();
        spawnBD_Raius_All.Clear();
        ConsBuildings.Clear();
        interSecPos.Clear();
        allAreaValueList.Clear();
        spawnPrefabs.Clear();
        buildingPrebs.Clear();
        blocks.Clear();
        spawnLocations.Clear();
        BlockIndicator_List.Clear();


        ContBDConuter.text = "Buildings : " + ConsBuildings.Count.ToString();


    }



    public object PositionRaycast(GameObject obj_)
    {
        RaycastHit hit;

        Physics.Raycast(obj_.transform.position, Vector3.down, out hit, raycastDis);

        itmPos = hit.point;

        //Debug.Log("itmPos : " + itmPos);

        return itmPos;

    }



    private void Spawn()
    {
        Debug.Log("Contructing Buildings...");


        foreach (GameObject subGiopos in GameObject.FindGameObjectsWithTag("gioPoint_sub"))
        {
            temp_list.Add(subGiopos);
        }
        Debug.Log("추출된 빌딩 포지션 수 :" + temp_list.Count); // "9720개/81블ㄹ = 120개/블럭"


        int j = 0;
        int k = 0;
        int SpawnedBD = 0;
        float AddedBDArea = 0;

        // 빌딩 생성위치 저장을위한 리스트  
        List<Vector3> BD_Set_Postion = new List<Vector3>();



        for (int i = 0; i <= interSecPos.Count / 4 - 1; i++)
        {

            //Debug.Log("interSecPos.Count -> i :" + i); // 0,1,2,3,4,5
            //Debug.Log("interSecPos.Count -> k :" + k); // 0,4,8,12,16

            // get 4 gioPos points
            Vector3[] interSecpos_ = new Vector3[4];

            interSecpos_[0] = new Vector3(interSecPos[k].x, interSecPos[k].y, interSecPos[k].z);
            //Debug.Log("interSecpos_[0]: " + interSecpos_[0]);

            interSecpos_[1] = new Vector3(interSecPos[k + 1].x, interSecPos[k + 1].y, interSecPos[k + 1].z);
            //Debug.Log("interSecpos_[1]: " + interSecpos_[1]);

            interSecpos_[2] = new Vector3(interSecPos[k + 2].x, interSecPos[k + 2].y, interSecPos[k + 2].z);
            //Debug.Log("interSecpos_[2]: " + interSecpos_[2]);

            interSecpos_[3] = new Vector3(interSecPos[k + 3].x, interSecPos[k + 3].y, interSecPos[k + 3].z);
            //Debug.Log("interSecpos_[3]: " + interSecpos_[3]);

            // 블럭위치를 알려주는 인디케이터
            GameObject BlockIndicator = Instantiate(BlockIndicatorPrefab, spawnLocations[i], Quaternion.identity);

            PositionRaycast(BlockIndicator); // itmPos.y

            BlockIndicator.transform.position = new Vector3(spawnLocations[i].x, itmPos.y + 0.7f, spawnLocations[i].z);

            // GameObject[] BlockIndicator_Array = new GameObject[interSecPos.Count / 4 - 1];
            // 리스트에 추가 - 이 값은 UI에서 추적 관리할것임 -> 연결코드는 UI_Get_info_Block 임!

            // Debug.Log("BlockIndicator :" + BlockIndicator); // 저장이 잘됨

            BlockIndicator_List.Add(BlockIndicator);


            
            while (0 < allAreaValueList[i] - AddedBDArea) // 블럭 면적에서 추가된 빌딩들 면적을 빼다가 - 값이 나오기 전에 while 빠져나옴
            {

                // 빌딩 랜덤으로 추출 --> 향후 빌딛의 종류와 수를 지정할 수 있어야 함
                int selection = Random.Range(0, spawnPrefab.Length);

                GameObject SelectedPrefab = spawnPrefab[selection];

                //Debug.Log("블럭의 면적 :" + allAreaValueList[i]);
                //Debug.Log("추가된 빌딩들의 면적 :" + AddedBDArea);


                // 방법 1 : 심점과 4지점의 거리를 비교해서 가장 큰 거리로 원을 만들어 그 안에서 랜덤으로 위치값을 추출, 단 타겟이 시각형이기 때문에 원밖에 빌딩이 생성될 수 있음
                //Vector2 randPos = Random.insideUnitCircle * spawnBD_Raius_All[i];
                //Debug.Log("spawnBD_Raius_All[i]    : " + spawnBD_Raius_All[i]);

                // 방법 2: 스폰 레이지를 4각형으로 만들어서 스폰하는 방법. 이 방법을 적용할 것!  성공
                Vector3 spTarget = new Vector3(
                                             Random.Range(-0.5f, 0.5f),
                                             0f,
                                             Random.Range(-0.5f, 0.5f)
                                              );



                Vector3 rangePos = spawnLocations[i] + new Vector3(spTarget.x * buildingFootPrint, 0, spTarget.z * buildingFootPrint);
                    //Debug.Log("spawnLocations[i]  : " + spawnLocations[i]);
                    //Debug.Log("rangePos : " + rangePos);


                // 블럭안에 빌딩 포함여부 체크 기능은 사용하지 않음 - 계산시간이 많이 걸려 성능 저하 원인

                GameObject SpawnInstance = Instantiate(SelectedPrefab, rangePos, Quaternion.identity);

                //Debug.Log("BD is spawned in Block - SpawnInstance : " + SpawnInstance);

                

                // Move new object to the calculated spawn location
                SpawnInstance.transform.position = rangePos;


                // 이제 빌딩의 위치를 각 블록안에 재배치하면 됨. "dict_<key>. list<gameobject values>" 이걸 해야햐는데...
                // j 는 idxCnt
                SpawnInstance.transform.position = ChangePosition(SpawnInstance.transform.position, Random.Range(j, j+120));

                
                // 빌딩 생성 위치 저장, 위치 체크해서 빈 위치에는 아스팔스 생성하기, 하지만 모든 위치에 생성해서 빌딩 위치이도해도 도로지형 유지할 것 
                BD_Set_Postion.Add(SpawnInstance.transform.position);

                // 생성된 빌딩 SpawnInstance 를 리스트에 담아서 관리해보자.
                ConsBuildings.Add(SpawnInstance);

                //Debug.Log("생성한 빌딩 수 : " + ConsBuildings.Count);  // --> 빌딩을 수정하면 업데이트 되어야 함
                ContBDConuter.text = "Buildings : " + ConsBuildings.Count.ToString();


                getBD_area = SpawnInstance.GetComponent<BuildingSize>();

                // 추가된 빌딩 면적 계산  --> 계산된 Block의 면적보다 작아야 한다.
                //Debug.Log("추가된 개별 빌딩의 면적 - BD_area : " + getBD_area.BD_area); // -------->>>>> 생성한 건물 면적이 산출되지 않음

                AddedBDArea += getBD_area.BD_area;


                // add Buindling
                SpawnedBD += 1;

                // 블럭내의 건물생성포인트 개수 120개씩 구 
                j += 1;
            }


            // 빌딩을 생성하지 않은 부분은 세부도로재질로 지형 생성, 그러나 모든 위치에 도로 생성 
            //foreach (GameObject ePos in temp_list)
            //{

            //    GameObject groudSpInstance = Instantiate(asplatPrefab, ePos.transform.position, transform.rotation);
            //}


            //Debug.Log("temp_list.Count / (interSecPos.Count/4) : " + temp_list.Count / (interSecPos.Count/4)); // 120
            //Debug.Log("SpawnedBD : " + SpawnedBD); // 25

            // 블럭안에 빌딩을 세우지 말아야 하는 수 = 120 - 블럭안에서 생성한 빌딩 수
            j = j + (temp_list.Count / (interSecPos.Count/4)) - SpawnedBD;

            //Debug.Log("j : " + j);

            // 블럭당 생성된 빌딩 수 리스트에 기록
            BuildingsInBlocks.Add(SpawnedBD);


        
            // 다음 블럭을 계산하기 위해 값 초기화
            AddedBDArea = 0;
            SpawnedBD = 0;



            // -----------------------------------------------------------------------------------------

            // * 아래 로직은 계산 시간이 너무 많이 걸림 * //  --> meshRenderer에 스폰하는 방법을 고려해보기로 


            //if (IsPointInPolygon(rangePos, interSecpos_) == true) // 생성값이 4 지점의 중심에 있다면, 즉 폴리곤 안에 있다면 빌딩 생성
            //{

            //    GameObject SpawnInstance = Instantiate(SelectedPrefab, rangePos, Quaternion.identity);

            //    //Debug.Log("BD is spawned in Block");

            //    // Move new object to the calculated spawn location
            //    SpawnInstance.transform.position = rangePos;

            //    SpawnedBD += 1;

            //}
            //else
            //{
            //    //Debug.Log("BD is not spawned in Block");

            //    if (SpawnedBD == 5) break;

            //}
            // -----------------------------------------------------------------------------------------


            k += 4;


        }

    }


    public Vector3 ChangePosition(Vector3 inputObj, int idxCnt)
    {
        try
        {
            inputObj = temp_list[idxCnt].transform.position;


        }
        catch
        {
            Debug.Log("index out of range.");
        }

        return inputObj;
    }


    // Find the intersection of two lines - 빌딩생성을 위한 블럭당 지형의 중심위치 추출
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
            : new Vector3((b2 * c1 - b1 * c2) / delta, 0, (a1 * c2 - a2 * c1) / delta);
    }




    // 생성된 gioPos의 위치값을 추출하여 블럭으로 재구성(1block = 4giPos)으로 총 100개의 포인트가 81개의 블럭으로 구성되고, 각 블럭의 중심위치를 추출하여 (중심점에서 빌딩을 생성)
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

        // Debug.Log("interSecPos.Count" + interSecPos.Count); // 324개로 4로 나누면 81개, 9X9의 블럭으로 구성되었기 때문
                                        // 4개의 지점으로 블럭조합 리스트가 완성되면 (,,,)의 리스트로 그룹데이터 생성됨, 이것을 가지고 중간지점  == > interSecPos

        // 지도상의 블럭 즉, 4지점을 추출하기 위한 gameobject르 순서대로 묶은 리스트 값 선언 
        int k = 0;
        for (int i = 0; i <= interSecPos.Count / 4 - 1; i++)
        {
            //Debug.Log("interSecPos[k]" + interSecPos[k]); // 위치값이 잘 나옴
            //Debug.Log("interSecPos[k]" + interSecPos[k+1]);
            //Debug.Log("interSecPos[k]" + interSecPos[k+2]);
            //Debug.Log("interSecPos[k]" + interSecPos[k+3]);
            Vector3 spawnLocation = FindIntersection(interSecPos[k], interSecPos[k + 1], interSecPos[k + 2], interSecPos[k + 3]);

            //Debug.Log("interSecPos[k]");
            //브럭의 시작점 위치 추출
            startPosEachBlock.Add(interSecPos[k]);

            //Debug.Log("spawnLocation" + spawnLocation);
            // 생성위치추출로 81개의 블럭 중심위치를 리스트로 저장
            spawnLocations.Add(spawnLocation);

            // 생성위치 + 블럭의 4 지점위치 데이터를 바탕으로 블럭의 면적을 계산하고, 면적안에 빌딩을 스폰(면적안에서만 빌딩을 물리적을 위치시켜야 함), 면적보다크면 빌딩스폰 정지
            // 벡터 연산이기 때문에 삼각형으로 나누어서 면적을 계산
            float area1 = Area_tri(interSecPos[k], interSecPos[k + 1], interSecPos[k + 2]);
            float area2 = Area_tri(interSecPos[k + 1], interSecPos[k + 3], interSecPos[k + 2]);
            // 블록의 면적 : 기본크기는 1임, gioPos 단위가 1이니까. 
            float areaOfBlock = area1 + area2;
            //Debug.Log("블럭당 면적 : " + areaOfBlock);

            //블럭당 면적으로 리스트에 저장할 것
            allAreaValueList.Add(areaOfBlock);

            //빌딩의 스폰 범위를 계산하기 위한 중심 vs 4개 지점의 최대거리값 계산하기
            List<float> LonggistDst = new List<float>();

            float D1 = Vector3.Distance(spawnLocation, interSecPos[k]);
            LonggistDst.Add(D1);
            //Debug.Log("D1 :" + D1);
            float D2 = Vector3.Distance(spawnLocation, interSecPos[k + 1]);
            LonggistDst.Add(D2);
            float D3 = Vector3.Distance(spawnLocation, interSecPos[k + 2]);
            LonggistDst.Add(D3);

            float D4 = Vector3.Distance(spawnLocation, interSecPos[k + 3]);
            LonggistDst.Add(D4);

            // 가장큰 값 취득
            //Debug.Log("Max : " + LonggistDst.Max());
            // 스폰생성의 반지름 개별값
            float spawnBD_Raius = LonggistDst.Max();
            // 리스트로 저장 spawnBD_Raius_All
            spawnBD_Raius_All.Add(spawnBD_Raius); 

            k += 4;
        }

    }


    // 벡터 3 지점의 내적 계산
    static float Area_tri(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 side1;
        side1 = b - a;

        Vector3 side2;
        side2 = c - a;

        Vector3 prep;
        prep = Vector3.Cross(side1, side2);

        float perpLength;
        perpLength = prep.magnitude;

        float areaResult = perpLength / 2;

        return areaResult;

    }




    public bool IsPointInPolygon(Vector3 p, Vector3[] polygon)
    {
        double minX = polygon[0].x;
        double maxX = polygon[0].x;
        double minZ = polygon[0].z;
        double maxZ = polygon[0].z;
        for (int i = 1; i < polygon.Length; i++)
        {
            Vector3 q = polygon[i];
            minX = System.Math.Min(q.x, minX);
            maxX = System.Math.Max(q.x, maxX);
            minZ = System.Math.Min(q.z, minZ);
            maxZ = System.Math.Max(q.z, maxZ);
        }

        if (p.x < minX || p.x > maxX || p.z < minZ || p.z > maxZ)
        {
            return false;
        }

        bool inside = false;
        for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
        {
            if ((polygon[i].z > p.z) != (polygon[j].z > p.z) &&
                 p.x < (polygon[j].x - polygon[i].x) * (p.z - polygon[i].z) / (polygon[j].z - polygon[i].z) + polygon[i].z)
            {
                inside = !inside;
            }
        }

        return inside;
    }




}
