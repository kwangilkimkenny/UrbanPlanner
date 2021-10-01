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

        for (int k = 0; k < PosEachBlock.Count; k += 4)
        {
            //Debug.Log("PosEachBlock.Count : " + PosEachBlock.Count);
            GameObject P1 = PosEachBlock[k];
            GameObject P2 = PosEachBlock[k + 1];
            GameObject P3 = PosEachBlock[k + 2];
            GameObject P4 = PosEachBlock[k + 3];

            // 점이 몇개가 생성될 것인지 결정
            //int RangeNums = Random.Range(2, 10);
            int RangeNums = 10;

            float div = 1f / RangeNums;

            int key_ = 0;
            for (int i = 0; i < RangeNums; i++)
            {

                //Debug.Log("k:" + k);
                // 블럭의 중간 생성지점 랜덤으로 포인트 생성
                //float div = Random.Range(0f, 1f);

                //Debug.Log("div:" + div);

                //var dict = new Dictionary<int, List<GameObject>>();

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
                    Debug.Log("subNewPos1");
                    GameObject subPos1_ = Instantiate(SubPosObj, subNewPos1, Quaternion.identity);
                    num++;
                    div_ += (1f / RangeNums);

                }


                key_ += 4;

                div += (1f / RangeNums);
            }

        }

        //Debug.Log("key : value = ", dict_[0][0]); // 0 4 8 12 ...





    }





    // subRoads 를 그리기 위한 positon 추출
    public void DrawSubRoads_GioPos_Horizontal()
    {
        // get updated value - current
        Debug.Log("run getOriginGioPos()! ");
        getOriginGioPos();

        for (int k = 0; k < PosEachBlock.Count; k += 4 )
        {
            //Debug.Log("PosEachBlock.Count : " + PosEachBlock.Count);
            GameObject P1 = PosEachBlock[k];
            GameObject P2 = PosEachBlock[k + 1];
            GameObject P3 = PosEachBlock[k + 2];
            GameObject P4 = PosEachBlock[k + 3];

            // 점이 몇개가 생성될 것인지 결정
            //int RangeNums = Random.Range(2, 10);
            int RangeNums = 10;

            float div = 1f / RangeNums;

            int key_ = 0;
            for (int i = 0; i < RangeNums; i++)
            {

                //Debug.Log("k:" + k);
                // 블럭의 중간 생성지점 랜덤으로 포인트 생성
                //float div = Random.Range(0f, 1f);

                //Debug.Log("div:" + div);

                //var dict = new Dictionary<int, List<GameObject>>();

                if (!dict.ContainsKey(key_))
                {
                    //add
                    dict.Add(key_, new List<GameObject>());
                }

                //// Vertical 
                ////Vector3.Lerp(vec3 from, vec3 to, float time)
                //Vector3 newPos1 = Vector3.Lerp(P1.transform.position, P2.transform.position, div);
                //GameObject subPos1 = Instantiate(SubPosObj, newPos1, Quaternion.identity);

                //dict[key_].Add(subPos1);

                //Vector3 newPos4 = Vector3.Lerp(P3.transform.position, P4.transform.position, div);
                //GameObject subPos4 = Instantiate(SubPosObj, newPos4, Quaternion.identity);

                //dict[key_].Add(subPos4);

                // ---------------------------------------------------------------------------
                // Horizontal 
                Vector3 newPos3 = Vector3.Lerp(P1.transform.position, P3.transform.position, div);
                GameObject subPos3 = Instantiate(SubPosObj, newPos3, Quaternion.identity);

                dict[key_].Add(subPos3);

                Vector3 newPos2 = Vector3.Lerp(P2.transform.position, P4.transform.position, div);
                GameObject subPo2 = Instantiate(SubPosObj, newPos2, Quaternion.identity);

                dict[key_].Add(subPo2);

                key_ += 4;

                div += (1f / RangeNums); 
            }

        }

    }



    // 생성한 subRoads posion을 이용하여 subRoads Line 생성!
    public void GenerateNewLine()
    {
        GameObject[] allPoints = GameObject.FindGameObjectsWithTag("gioPoint_sub");
        Vector3[] allPointPositions = new Vector3[allPoints.Length];

        if (allPoints.Length >= 2)
        {
            for (int i = 0; i < allPoints.Length; i++)
            {
                allPointPositions[i] = allPoints[i].transform.position;
            }

            SpawnLineGenerator(allPointPositions);
        }
        else
        {
            Debug.Log("Need 2 or more points to draw a line.");
        }
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




    private void SpawnLineGenerator(Vector3[] linePoints)
    {
        GameObject newLineGen = Instantiate(lineGeneratorPrefab);
        LineRenderer lRend = newLineGen.GetComponent<LineRenderer>();

        lRend.positionCount = linePoints.Length;
        lRend.SetPositions(linePoints);
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