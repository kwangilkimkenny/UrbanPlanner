// 블록 단위별로 subGioPos 생성하여 별도 관

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenLogic_Block_type3 : MonoBehaviour
{
    // generate sub_road prefab
    public GameObject subRoadPrefab;

    // Draw subRoads
    [SerializeField]
    private GameObject lineGeneratorPrefab;
    [SerializeField]
    private GameObject linePointPrefab;

    public GameObject newLineGen;



    // 불러올 게임오브젝트 리스트 선언
    private List<GameObject> GiposSpawnPrefabs = new List<GameObject>();
    // 1차 계산
    private List<GameObject> interSecObj = new List<GameObject>();
    // 총 81개의 블for (int i = 0; i < spawnPrefabs.Count + 1; i++)
    private List<GameObject> interSecPos_ = new List<GameObject>();
    private List<Vector3> startPosEachBlock = new List<Vector3>();
    // 2차 블럭의 4 지점의 위치를 저장한 리스트
    private List<GameObject> PosEachBlock = new List<GameObject>();

    public GameObject SubPosObj;

    // block subgiopos
    // int = k 로 블록당 id값임 총 81개임

    public Dictionary<int, List<GameObject>> dict = new Dictionary<int, List<GameObject>>();
    public Dictionary<int, List<GameObject>> dict_ = new Dictionary<int, List<GameObject>>();






    // subRoads 를 그리기 위한 positon 추출
    public void DrawSubRoads_GioPos_Vertical()
    {
        // get updated value - current
        Debug.Log("run getOriginGioPos()! ");
        getOriginGioPos();
        Debug.Log("PosEachBlock.Count : " + PosEachBlock.Count/4); //81

        int key_ = 0;
        for (int k = 0; k < PosEachBlock.Count; k += 4)
        {

            GameObject P1 = PosEachBlock[k];
            GameObject P2 = PosEachBlock[k + 1];
            GameObject P3 = PosEachBlock[k + 2];
            GameObject P4 = PosEachBlock[k + 3];

            // 점이 몇개가 생성될 것인지 결정
            //int RangeNums = Random.Range(2, 10);
            int RangeNums = 10;

            // 중간지점값 설정
            float div = 1f / RangeNums;

            
            for (int i = 0; i < PosEachBlock.Count / 4; i++)
            {

                if (!dict_.ContainsKey(key_))
                {
                    //add
                    dict_.Add(key_, new List<GameObject>());
                }

                // Vertical 
                //Vector3.Lerp(vec3 from, vec3 to, float time)
                Vector3 newPos1 = Vector3.Lerp(P1.transform.position, P2.transform.position, div);
                GameObject subPos1 = Instantiate(SubPosObj, newPos1, Quaternion.identity);

                dict_[key_].Add(subPos1); // 0 4 8 ...

                Vector3 newPos4 = Vector3.Lerp(P3.transform.position, P4.transform.position, div);
                GameObject subPos4 = Instantiate(SubPosObj, newPos4, Quaternion.identity);

                dict_[key_].Add(subPos4); // 0 4 8 ...


                // 위에서 생성한 subPos를 기반으로 블록 내부의 subGioPos 추가


                int num = 1;
                float div_ = 1f / RangeNums;
                while (num <= RangeNums)
                {
                    Vector3 subNewPos1 = Vector3.Lerp(subPos1.transform.position, subPos4.transform.position, div_);
                    //Debug.Log("subNewPos1");
                    GameObject subPos1_ = Instantiate(SubPosObj, subNewPos1, Quaternion.identity);

                    dict_[key_].Add(subPos1_);

                    num++;
                    div_ += (1f / RangeNums);
                }

                div += (1f / RangeNums);
            }
            key_ += 1;

        }

        foreach ( GameObject obj in dict_[0])
        {

            Debug.Log("dic_ 의 values count : " + obj);
        }



    }





    //// subRoads 를 그리기 위한 Sub block positon 추출
    //public void DrawSubRoads_GioPos_Horizontal()
    //{
    //    // get updated value - current
    //    Debug.Log("run getOriginGioPos()! ");
    //    getOriginGioPos();

    //    for (int k = 0; k < PosEachBlock.Count; k += 4 )
    //    {
    //        //Debug.Log("PosEachBlock.Count : " + PosEachBlock.Count);
    //        GameObject P1 = PosEachBlock[k];
    //        GameObject P2 = PosEachBlock[k + 1];
    //        GameObject P3 = PosEachBlock[k + 2];
    //        GameObject P4 = PosEachBlock[k + 3];

    //        // 점이 몇개가 생성될 것인지 결정
    //        //int RangeNums = Random.Range(2, 10);
    //        int RangeNums = 10;

    //        float div = 1f / RangeNums;

    //        int key_ = 0;
    //        for (int i = 0; i < RangeNums; i++)
    //        {

    //            //Debug.Log("k:" + k);
    //            // 블럭의 중간 생성지점 랜덤으로 포인트 생성
    //            //float div = Random.Range(0f, 1f);

    //            //Debug.Log("div:" + div);

    //            //var dict = new Dictionary<int, List<GameObject>>();

    //            if (!dict.ContainsKey(key_))
    //            {
    //                //add
    //                dict.Add(key_, new List<GameObject>());
    //            }

    //            //// Vertical 
    //            ////Vector3.Lerp(vec3 from, vec3 to, float time)
    //            //Vector3 newPos1 = Vector3.Lerp(P1.transform.position, P2.transform.position, div);
    //            //GameObject subPos1 = Instantiate(SubPosObj, newPos1, Quaternion.identity);

    //            //dict[key_].Add(subPos1);

    //            //Vector3 newPos4 = Vector3.Lerp(P3.transform.position, P4.transform.position, div);
    //            //GameObject subPos4 = Instantiate(SubPosObj, newPos4, Quaternion.identity);

    //            //dict[key_].Add(subPos4);

    //            // ---------------------------------------------------------------------------
    //            // Horizontal 
    //            Vector3 newPos3 = Vector3.Lerp(P1.transform.position, P3.transform.position, div);
    //            GameObject subPos3 = Instantiate(SubPosObj, newPos3, Quaternion.identity);

    //            dict[key_].Add(subPos3);

    //            Vector3 newPos2 = Vector3.Lerp(P2.transform.position, P4.transform.position, div);
    //            GameObject subPo2 = Instantiate(SubPosObj, newPos2, Quaternion.identity);

    //            dict[key_].Add(subPo2);

    //            key_ += 4;

    //            div += (1f / RangeNums); 
    //        }

    //    }

    //}




    public void GenerateNewLine()
    {
        Debug.Log("dict_.Count : " + dict_.Count); // 325
        //for (int i = 0; i < dict_.Count + 1; i += 4)
        //{
        //    GenerateNewLine_detailed(i);
        //    Debug.Log("dic.count : " + i);
        //}

        GenerateNewLine_detailed(0);


    }





    // 생성한 subRoads posion을 이용하여 subRoads Line 생성! 블럭의 딕녀러리 키 값이 입력되며너 세부 지점값추출하여 라인 그리기
    public void GenerateNewLine_detailed(int keyValue)
    {
        // 딕셔너리에서 블럭별로 관리를 해야함. 딕셔너리에서 블력별로 세부 생성지점 가져오기
        //GameObject[] allPoints = GameObject.FindGameObjectsWithTag("gioPoint_sub"); // 이건 전체코드를 가져오는 방법임


        List<GameObject> getNewGiPos = new List<GameObject>();

        // 키값으로 딕셔너리의 value 즉, 블럭 내부의 지점의 오브젝트 리스트를 하나씩 불러온다.
        // 블럭의 딕셔너리 키값에 해당하는 내부블럭의 gioPos 추출, (라인그리는 값으로 활용해야함)
        // 순서정렬

        List<GameObject> temp = new List<GameObject>();
        List<GameObject> temp2 = new List<GameObject>();
        List<GameObject> temp3 = new List<GameObject>();
        List<GameObject> temp4 = new List<GameObject>();
        List<GameObject> temp5 = new List<GameObject>();
        List<GameObject> temp6 = new List<GameObject>();

        Debug.Log("dict_[keyValue].Count :" + dict_[keyValue].Count); // 972
        Debug.Log("dict_[1].Count :" + dict_[1].Count); //  972??? 키 하나에 벨류가 모두 포함됨???

        for (int w = 0; w < dict_[keyValue].Count + 1; w++) //82

            if (w < 10 )
            {
                getNewGiPos.Add(dict_[keyValue][w]); // 리스트에 값을 담는다.
            }
            else if (10 <= w && w < 20)
            {
                temp.Add(dict_[keyValue][w]);
            }
            else if (20 <= w && w < 30)
            {
                if (w == 20)
                {
                    temp.Reverse(); // 순서를 바꿔준다.

                    foreach (GameObject j in temp)
                    {
                        getNewGiPos.Add(j); // 저장해준다.
                    }
                }
                getNewGiPos.Add(dict_[keyValue][w]); // 리스트에 값을 담는다.
            }
            else if (30 <= w && w < 40)
            {
                //List<Vector3> temp2 = new List<Vector3>();
                temp2.Add(dict_[keyValue][w]);

            }
            else if (40 <= w && w < 50)
            {
                if (w == 40)
                {
                    temp2.Reverse(); // 순서를 바꿔준다.

                    foreach (GameObject k in temp2)
                    {
                        getNewGiPos.Add(k);
                    }
                }
                getNewGiPos.Add(dict_[keyValue][w]); // 리스트에 값을 담는다.
            }
            else if (50 <= w && w < 60)
            {
                //List<Vector3> temp3 = new List<Vector3>();
                temp3.Add(dict_[keyValue][w]);

            }
            else if (60 <= w && w < 70)
            {
                if (w == 60)
                {
                    temp3.Reverse(); // 순서를 바꿔준다.

                    foreach (GameObject k in temp3)
                    {
                        getNewGiPos.Add(k);
                    }
                }
                getNewGiPos.Add(dict_[keyValue][w]); // 리스트에 값을 담는다.
            }
            else if (70 <= w && w < 80)
            {
                //List<Vector3> temp4 = new List<Vector3>();
                temp4.Add(dict_[keyValue][w]);
            }
            else if (80 <= w && w < 90)
            {
                if (w == 80)
                {
                    temp4.Reverse(); // 순서를 바꿔준다.

                    foreach (GameObject k in temp4)
                    {
                        getNewGiPos.Add(k);
                    }
                }

                getNewGiPos.Add(dict_[keyValue][w]); // 리스트에 값을 담는다.
            }
            else if (90 <= w && w < 100)
            {
                //List<Vector3> temp5 = new List<Vector3>();
                temp5.Add(dict_[keyValue][w]);
            }
            else if (100 == w)
            {
                temp5.Reverse(); // 순서를 바꿔준다.

                foreach (GameObject k in temp5)
                {
                    getNewGiPos.Add(k);
                }

            }
            else
            {
                //Debug.Log("no more spawn prefabs!");
            }

    




        Vector3[] allPointPositions = new Vector3[getNewGiPos.Count];

        if (getNewGiPos.Count >= 2)
        {
            for (int i = 0; i < getNewGiPos.Count; i++)
            {
                allPointPositions[i] = getNewGiPos[i].transform.position;
            }
           
            SpawnLineGenerator(allPointPositions);
        }
        else
        {
            Debug.Log("Need 2 or more points to draw a line.");
        }
    }


    //public void GenerateNewLine()
    //{
    //    GameObject[] allPoints = GameObject.FindGameObjectsWithTag("gioPoint_sub");
    //    Vector3[] allPointPositions = new Vector3[allPoints.Length];

    //    Vector3[] allPointPositions_reOrder = new Vector3[allPointPositions.Length];

    //    if (allPoints.Length >= 2)
    //    {
    //        for (int i = 0; i < allPoints.Length; i++)
    //        {
    //            allPointPositions[i] = allPoints[i].transform.position;
    //        }


    //        // ㄹ 형태로 vertical horizontal을 만들기위해 순서를 정렬해야 한다.
    //        // Vertical 순서정렬하기
    //        for (int i = 0; i < allPointPositions.Length + 1; i++)
    //        {

    //        }


    //        SpawnLineGenerator(allPointPositions_reOrder);
    //    }
    //    else
    //    {
    //        Debug.Log("Need 2 or more points to draw a line.");
    //    }
    //}




    private void SpawnLineGenerator(Vector3[] linePoints)
    {
        GameObject newLineGen = Instantiate(lineGeneratorPrefab);
        LineRenderer lRend = newLineGen.GetComponent<LineRenderer>();

        lRend.positionCount = linePoints.Length;
        lRend.SetPositions(linePoints);
    }










    public void ClearAllSubRoadPoints()
    {
        GameObject[] allRoadPonts = GameObject.FindGameObjectsWithTag("gioPoint_sub");

        foreach (GameObject p in allRoadPonts)
        {
            Destroy(p);
        }

        GameObject[] getSubRoadLine = GameObject.FindGameObjectsWithTag("subRoad");

        foreach (GameObject q in getSubRoadLine)
        {
            Destroy(q);
        }

    }






    public void getOriginGioPos()
    {
        // GioPos 태그를 가진 GameObject를 모두 찾아서 새로운 리스트에 하나씩 담는다. 이것은 위치값을 추적하기 위함이다.
        foreach (GameObject spawnp in GameObject.FindGameObjectsWithTag("GioPos"))
        {
            GiposSpawnPrefabs.Add(spawnp);
            //Debug.Log("check 1 !");
        }

        // 4 지점의 조합 리스트값이 필요함. ---> Blocks로 계산해야 ㅎ
        for (int i = 0; i < GiposSpawnPrefabs.Count - 11; i++)
        {

            if (i == 9 || i == 19 || i == 29 || i == 39 || i == 49 || i == 59 || i == 69 || i == 79 || i == 89 || i == 99 || i == 109)
            {
                continue;
            }

            interSecObj.Add(GiposSpawnPrefabs[i]);
            interSecObj.Add(GiposSpawnPrefabs[i + 1]);
            interSecObj.Add(GiposSpawnPrefabs[i + 10]);
            interSecObj.Add(GiposSpawnPrefabs[i + 11]);

        }

        int k = 0;
        for (int i = 0; i <= interSecObj.Count / 4 - 1; i++)
        {
            //브럭의 위치 추출
            PosEachBlock.Add(interSecObj[k]);
            PosEachBlock.Add(interSecObj[k + 1]);
            PosEachBlock.Add(interSecObj[k + 2]);
            PosEachBlock.Add(interSecObj[k + 3]);



            k += 4;
        }

    }



    public void ResetSubGioPosition()
    {


        Debug.Log("Reset! clear all items!");
        interSecPos_.Clear();
        startPosEachBlock.Clear();
        GiposSpawnPrefabs.Clear();
        interSecObj.Clear();
        PosEachBlock.Clear();
        GiposSpawnPrefabs.Clear();

        Debug.Log("Reset! and regenerate sub_gio_position again!!!!!");
        //PosEachBlock.Clear();
        //Debug.Log("PosEachBlock.Count : " + PosEachBlock.Count);


        foreach (GameObject gitm_ in GameObject.FindGameObjectsWithTag("gioPoint_sub"))
        {
            //gitm_.SetActive(false);
            Destroy(gitm_);

        }

    }


}








//// 블록의 L 형태의 subGio position을 생성하고나서 맨 위 상단 긑에 ㄱ 형태의 포인트를 생성한다.  

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GridGenLogic_Block_type3 : MonoBehaviour
//{
//    // generate sub_road prefab
//    public GameObject subRoadPrefab;

//    // Draw subRoads
//    [SerializeField]
//    private GameObject lineGeneratorPrefab;
//    [SerializeField]
//    private GameObject linePointPrefab;

//    public GameObject newLineGen;



//    // 불러올 게임오브젝트 리스트 선언
//    private List<GameObject> GiposSpawnPrefabs = new List<GameObject>();
//    // 1차 계산
//    private List<GameObject> interSecObj = new List<GameObject>();
//    // 총 81개의 블for (int i = 0; i < spawnPrefabs.Count + 1; i++)
//    private List<GameObject> interSecPos_ = new List<GameObject>();
//    private List<Vector3> startPosEachBlock = new List<Vector3>();
//    // 2차 블럭의 4 지점의 위치를 저장한 리스트
//    private List<GameObject> PosEachBlock = new List<GameObject>();

//    public GameObject SubPosObj;

//    // subRoads 를 그리기 위한 positon 추출
//    public void DrawSubRoads()
//    {
//        // get updated value - current
//        Debug.Log("run getOriginGioPos()! ");
//        getOriginGioPos();

//        for (int k = 0; k < PosEachBlock.Count; k += 4)
//        {
//            //Debug.Log("PosEachBlock.Count : " + PosEachBlock.Count);
//            GameObject P1 = PosEachBlock[k];
//            GameObject P2 = PosEachBlock[k + 1];
//            GameObject P3 = PosEachBlock[k + 2];
//            GameObject P4 = PosEachBlock[k + 3];

//            // 점이 몇개가 생성될 것인지 결정
//            int RangeNums = Random.Range(2, 10);
//            //int RangeNums = 10;

//            float div = 1f / RangeNums;

//            for (int i = 0; i < RangeNums; i++)
//            {

//                //Debug.Log("k:" + k);
//                // 블럭의 중간 생성지점 랜덤으로 포인트 생성
//                //float div = Random.Range(0f, 1f);

//                //Debug.Log("div:" + div);

//                //Vector3.Lerp(vec3 from, vec3 to, float time)
//                Vector3 newPos1 = Vector3.Lerp(P1.transform.position, P2.transform.position, div);
//                GameObject subPos1 = Instantiate(SubPosObj, newPos1, Quaternion.identity);

//                //Vector3 newPos2 = Vector3.Lerp(P2.transform.position, P4.transform.position, div);
//                //GameObject subPo2 = Instantiate(SubPosObj, newPos2, Quaternion.identity);

//                Vector3 newPos3 = Vector3.Lerp(P1.transform.position, P3.transform.position, div);
//                GameObject subPos3 = Instantiate(SubPosObj, newPos3, Quaternion.identity);

//                //Vector3 newPos4 = Vector3.Lerp(P3.transform.position, P4.transform.position, div);
//                //GameObject subPos4 = Instantiate(SubPosObj, newPos4, Quaternion.identity);
//                // ㄱ 자 부분 생성

//                if (k == 32 || k == 68 || k == 104 || k == 140 || k == 176 || k == 212 || k == 248 || k == 284 || k == 320)
//                {
//                    //Debug.Log("div:" + div);

//                    Vector3 newPos2 = Vector3.Lerp(P2.transform.position, P4.transform.position, div);
//                    GameObject subPo2 = Instantiate(SubPosObj, newPos2, Quaternion.identity);


//                    Vector3 newPos4 = Vector3.Lerp(P3.transform.position, P4.transform.position, div);
//                    GameObject subPos4 = Instantiate(SubPosObj, newPos4, Quaternion.identity);
//                }

//                div += (1f / RangeNums);
//            }
//        }

//    }

//    // 생성한 subRoads posion을 이용하여 subRoads Line 생성!
//    public void GenerateNewLine()
//    {
//        GameObject[] allPoints = GameObject.FindGameObjectsWithTag("gioPoint_sub");
//        Vector3[] allPointPositions = new Vector3[allPoints.Length];

//        if (allPoints.Length >= 2)
//        {
//            for (int i = 0; i < allPoints.Length; i++)
//            {
//                allPointPositions[i] = allPoints[i].transform.position;
//            }

//            SpawnLineGenerator(allPointPositions);
//        }
//        else
//        {
//            Debug.Log("Need 2 or more points to draw a line.");
//        }
//    }


//    public void ClearAllSubRoadPoints()
//    {
//        GameObject[] allRoadPonts = GameObject.FindGameObjectsWithTag("gioPoint_sub");

//        foreach (GameObject p in allRoadPonts)
//        {
//            Destroy(p);
//        }

//        GameObject[] getSubRoadLine = GameObject.FindGameObjectsWithTag("subRoad");

//        foreach (GameObject q in getSubRoadLine)
//        {
//            Destroy(q);
//        }

//    }




//    private void SpawnLineGenerator(Vector3[] linePoints)
//    {
//        GameObject newLineGen = Instantiate(lineGeneratorPrefab);
//        LineRenderer lRend = newLineGen.GetComponent<LineRenderer>();

//        lRend.positionCount = linePoints.Length;
//        lRend.SetPositions(linePoints);
//    }


//    public void getOriginGioPos()
//    {
//        // GioPos 태그를 가진 GameObject를 모두 찾아서 새로운 리스트에 하나씩 담는다. 이것은 위치값을 추적하기 위함이다.
//        foreach (GameObject spawnp in GameObject.FindGameObjectsWithTag("GioPos"))
//        {
//            GiposSpawnPrefabs.Add(spawnp);
//            //Debug.Log("check 1 !");
//        }

//        // 4 지점의 조합 리스트값이 필요함. ---> Blocks로 계산해야 ㅎ
//        for (int i = 0; i < GiposSpawnPrefabs.Count - 11; i++)
//        {

//            if (i == 9 || i == 19 || i == 29 || i == 39 || i == 49 || i == 59 || i == 69 || i == 79 || i == 89 || i == 99 || i == 109)
//            {
//                continue;
//            }

//            interSecObj.Add(GiposSpawnPrefabs[i]);
//            interSecObj.Add(GiposSpawnPrefabs[i + 1]);
//            interSecObj.Add(GiposSpawnPrefabs[i + 10]);
//            interSecObj.Add(GiposSpawnPrefabs[i + 11]);

//        }

//        int k = 0;
//        for (int i = 0; i <= interSecObj.Count / 4 - 1; i++)
//        {
//            //브럭의 위치 추출
//            PosEachBlock.Add(interSecObj[k]);
//            PosEachBlock.Add(interSecObj[k + 1]);
//            PosEachBlock.Add(interSecObj[k + 2]);
//            PosEachBlock.Add(interSecObj[k + 3]);

//            k += 4;
//        }

//    }



//    public void ResetSubGioPosition()
//    {


//        Debug.Log("Reset! clear all items!");
//        interSecPos_.Clear();
//        startPosEachBlock.Clear();
//        GiposSpawnPrefabs.Clear();
//        interSecObj.Clear();
//        PosEachBlock.Clear();
//        GiposSpawnPrefabs.Clear();

//        Debug.Log("Reset! and regenerate sub_gio_position again!!!!!");
//        //PosEachBlock.Clear();
//        //Debug.Log("PosEachBlock.Count : " + PosEachBlock.Count);


//        foreach (GameObject gitm_ in GameObject.FindGameObjectsWithTag("gioPoint_sub"))
//        {
//            //gitm_.SetActive(false);
//            Destroy(gitm_);

//        }

//    }


//}