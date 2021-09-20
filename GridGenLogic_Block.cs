using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// GridPos 의 생성 위치를 할당하고
// 생성 prefabs 간의 길이를 해당 블럭의 크기에 맞게 비율로 조절한다.

public class GridGenLogic_Block : MonoBehaviour
{
    // GridGenLogic 함수에서 실행한 값을 가져온다.
    public GridGenLogic get_PreGetOriginGioPos;

    public int rows = 10;
    public int columns = 10;
    // 각 지점간의 크기를 1/10으로 정함. 나중에 이 값은 블럭의 크기에서 정해야 함. 현재 블럭크기는 1로 되어 있음. 
    public float scale = 0.1f;
    public GameObject gridPrefab;

    // first set position of block  - 어떻게 블럭생성 위치값을 갱산하면서 할당할 것인가?
    // building 생성 코드에서 가져오면 됨. ㅠ

    public Vector3 leftBottomLocation;

    // 생성된 gioPoint를 props 리스트에 등록해주기 위한 리스트
    public List<GameObject> props_ = new List<GameObject>();
    public List<Vector3> prebPosAll_ = new List<Vector3>();

    // 총 81개의 블for (int i = 0; i < spawnPrefabs.Count + 1; i++)
    public List<Vector3> interSecPos_ = new List<Vector3>();

    // 1차 계산
    public List<Vector3> interSecPos__ = new List<Vector3>();

    public List<Vector3> startPosEachBlock = new List<Vector3>();

    // 2차 블럭의 4 지점의 위치를 저장한 리스트
    public List<Vector3> PosEachBlock = new List<Vector3>();

    //Post_PosEachBlock
    public List<Vector3> Post_PosEachBlock = new List<Vector3>();

    // 불러올 게임오브젝트 리스트 선언
    private List<GameObject> GiposSpawnPrefabs = new List<GameObject>();

    // GiposSpawnPrefabs_
    private List<GameObject> GiposSpawnPrefabs_ = new List<GameObject>();



   //void Awake()
   // {
   //     // 코드가 샐행되면 1차 계산, // get post value
   //     PreGetOriginGioPos();

    //     // 값 확인 테스트
    //     foreach (Vector3 ps in Post_PosEachBlock)
    //     {
    //         Debug.Log("ps: " + ps);
    //     }
    // }


    public void getGioPosInBlock()
    {
        Debug.Log("generate sub_gio_position!");


        // get post value
        //PreGetOriginGioPos();

        // get updated value - current
        getOriginGioPos();

        if (gridPrefab)
        {
            int k = 0;
            foreach (Vector3 StPos in startPosEachBlock)
            {
                // 작은 블럭단위의 시작 위치값 추출하여 반복 생성
                //Debug.Log("StPos" + StPos);

                leftBottomLocation = new Vector3(StPos.x, StPos.y, StPos.z);

                // 업데이트 이전 지점의 위치
                Vector3 postP1 = Post_PosEachBlock[k];
                Debug.Log("postP1 :" + postP1);
                Vector3 postP2 = Post_PosEachBlock[k + 1];
                Vector3 postP3 = Post_PosEachBlock[k + 2];
                Vector3 postP4 = Post_PosEachBlock[k + 3];


                // 업데이트한 네 지점의 위치값
                Vector3 updatedP1 = PosEachBlock[k];
                Debug.Log("updatedP1 :" + updatedP1);
                Vector3 updatedP2 = PosEachBlock[k + 1];
                Vector3 updatedP3 = PosEachBlock[k + 2];
                Vector3 updatedP4 = PosEachBlock[k + 3];


                // 네 지점의 변화 방향 벡터
                Vector3 P1 = postP1 - updatedP1;
                Vector3 P2 = postP2 - updatedP2;
                Vector3 P3 = postP3 - updatedP3;
                Vector3 P4 = postP4 - updatedP4;


                // 네 지점의 위치변화 크기(길이)
                float DistP1 = Vector3.Distance(postP1, updatedP1);
                //Debug.Log("DistP1 :" + DistP1);
                float DistP2 = Vector3.Distance(postP2, updatedP2);
                float DistP3 = Vector3.Distance(postP3, updatedP3);
                float DistP4 = Vector3.Distance(postP4, updatedP4);


               float GetDistance(Vector3 a, Vector3 b)
                {
                    Vector3 c = a - b;
                    float dist = Vector3.Magnitude(c);
                    return dist;
                }



                for (int i = 0; i < columns; i++)
                {
                    for (int j = 0; j < rows; j++)
                    {
                        GameObject obj = Instantiate(gridPrefab, new Vector3(leftBottomLocation.x + scale * i, leftBottomLocation.y, leftBottomLocation.z + scale * j), Quaternion.identity);

                        //print("Instantiate1");
                        obj.transform.SetParent(gameObject.transform);

                        //print("Instantiate2");
                        obj.GetComponent<GridStat>().x = i;

                        //print("Instantiate3");
                        obj.GetComponent<GridStat>().y = j;

                        // 이동 전의 내점과 각 지점간의 거리 계산, 비율을 계산하기 위
                        float disttP1 = GetDistance(obj.transform.position, postP1);
                        float disttP2 = GetDistance(obj.transform.position, postP2);
                        float disttP3 = GetDistance(obj.transform.position, postP3);
                        float disttP4 = GetDistance(obj.transform.position, postP4);

                        // 거리 비율 계산
                        float R1 = disttP1 / (disttP1 + disttP2 + disttP3 + disttP4);
                        float R2 = disttP2 / (disttP1 + disttP2 + disttP3 + disttP4);
                        float R3 = disttP3 / (disttP1 + disttP2 + disttP3 + disttP4);
                        float R4 = disttP4 / (disttP1 + disttP2 + disttP3 + disttP4);

                        // 가장 큰 비율값 계산 순서대로
                        float[] RArray = new float[4];
                        RArray[0] = R1;
                        RArray[1] = R2;
                        RArray[2] = R3;
                        RArray[3] = R4;

                        // 정렬(큰값 순서대로)
                        Array.Reverse(RArray);

                        float R1_ = RArray[0] * -1.6f;
                        float R2_ = RArray[1] * -0.8f;
                        float R3_ = RArray[2] * -0.2f;
                        float R4_ = RArray[3] * -0.1f;

                        // 값 확인
                        //foreach(float value in RArray)
                        //{
                        //    Debug.Log("Array Value : " + value);
                        //}

                        // 가중치 적용
                        Vector3 S1 = P1 * R1_;
                        Vector3 S2 = P2 * R2_;
                        Vector3 S3 = P3 * R3_;
                        Vector3 S4 = P4 * R4_;


                        obj.transform.position = obj.transform.position + S1 + S2 + S3 + S4;
                        //Debug.Log("sub_Gio_pos 위치 :" + obj);

                        props_.Add(obj);

                    }

                }
             k += 4;
            }
        }
        else print("missing gridprefab, please assign.");

        // 생성된 프리팹의 모든 위치값을 추출 하여 저장한다. --> 이 값을 이제 lineRenderer로 보내서 도로를 그려주면된다.
        getPosOfPrefabs();
    }


    public void ResetSubGioPosition()
    {
        Debug.Log("Reset! and regenerate sub_gio_position again!");
        foreach (GameObject gitm_ in GameObject.FindGameObjectsWithTag("gioPoint_sub"))
        {
           //gitm_.SetActive(false);
            Destroy(gitm_);
        }

        props_.Clear();
        interSecPos_.Clear();
        prebPosAll_.Clear();
        startPosEachBlock.Clear();
        GiposSpawnPrefabs.Clear();
        GiposSpawnPrefabs_.Clear();
        PosEachBlock.Clear();
        Post_PosEachBlock.Clear();
        interSecPos__.Clear();

        //if (gridPrefab)
        //    GenerateGrid();
        //else print("missing gridprefab, please assign.");

        //// 생성된 프리팹의 모든 위치값을 추출 하여 저장한다. --> 이 값을 이제 lineRenderer로 보내서 도로를 그려주면된다.
        //getPosOfPrefabs();
    }


    void GenerateGrid()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject obj = Instantiate(gridPrefab, new Vector3(leftBottomLocation.x + scale * i, leftBottomLocation.y, leftBottomLocation.z + scale * j), Quaternion.identity);

                //print("Instantiate1");
                obj.transform.SetParent(gameObject.transform);

                //print("Instantiate2");
                obj.GetComponent<GridStat>().x = i;

                //print("Instantiate3");
                obj.GetComponent<GridStat>().y = j;


                //////// obj 의 위치를 변경해야 함

                props_.Add(obj);


            }
        }
    }





    // 생성된 오브젝트의 위치를 추출해주는 함수를 만든다.
    public void getPosOfPrefabs()
    {
        for (int i = 0; i < props_.Count; i++)
        {
            Vector3 getPosPreb = props_[i].transform.position;
            // 생성된 프리팹의 위치값을 모두 저장한다.
            prebPosAll_.Add(getPosPreb);
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

            interSecPos_.Add(GiposSpawnPrefabs[i].transform.position);
            interSecPos_.Add(GiposSpawnPrefabs[i + 1].transform.position);
            interSecPos_.Add(GiposSpawnPrefabs[i + 10].transform.position);
            interSecPos_.Add(GiposSpawnPrefabs[i + 11].transform.position);

        }

        int k = 0;
        for (int i = 0; i <= interSecPos_.Count / 4 - 1; i++)
        {
            //Debug.Log("interSecPos[k]");
            //브럭의 시작점 위치 추출
            startPosEachBlock.Add(interSecPos_[k]);
            //블럭의 1~4번째 위치 추
            PosEachBlock.Add(interSecPos_[k]);
            PosEachBlock.Add(interSecPos_[k+1]);
            PosEachBlock.Add(interSecPos_[k+2]);
            PosEachBlock.Add(interSecPos_[k+3]);

            k += 4;
        }

    }



    public void PreGetOriginGioPos()
    {
        // GioPos 태그를 가진 GameObject를 모두 찾아서 새로운 리스트에 하나씩 담는다. 이것은 위치값을 추적하기 위함이다.
        foreach (GameObject spawnp in GameObject.FindGameObjectsWithTag("GioPos"))
        {
            GiposSpawnPrefabs_.Add(spawnp);
            //Debug.Log("check 1 !");
        }

        // 4 지점의 조합 리스트값이 필요함. ---> Blocks로 계산해야 ㅎ
        for (int i = 0; i < GiposSpawnPrefabs_.Count - 11; i++)
        {

            if (i == 9 || i == 19 || i == 29 || i == 39 || i == 49 || i == 59 || i == 69 || i == 79 || i == 89 || i == 99 || i == 109)
            {
                continue;
            }

            interSecPos__.Add(GiposSpawnPrefabs_[i].transform.position);
            interSecPos__.Add(GiposSpawnPrefabs_[i + 1].transform.position);
            interSecPos__.Add(GiposSpawnPrefabs_[i + 10].transform.position);
            interSecPos__.Add(GiposSpawnPrefabs_[i + 11].transform.position);

        }

        int k = 0;
        for (int i = 0; i <= interSecPos__.Count / 4 - 1; i++)
        {
            //Debug.Log("interSecPos[k]");
            //브럭의 시작점 위치 추출
            //startPosEachBlock.Add(interSecPos_[k]);
            //블럭의 1~4번째 위치 추
            Post_PosEachBlock.Add(interSecPos__[k]);
            Post_PosEachBlock.Add(interSecPos__[k + 1]);
            Post_PosEachBlock.Add(interSecPos__[k + 2]);
            Post_PosEachBlock.Add(interSecPos__[k + 3]);

            k += 4;
        }

    }


}



















//// backup code
///


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//// GridPos 의 생성 위치를 할당하고
//// 생성 prefabs 간의 길이를 해당 블럭의 크기에 맞게 비율로 조절한다.

//public class GridGenLogic_Block : MonoBehaviour
//{
//    // GridGenLogic 함수에서 실행한 값을 가져온다.
//    public GridGenLogic get_PreGetOriginGioPos;

//    public int rows = 10;
//    public int columns = 10;
//    // 각 지점간의 크기를 1/10으로 정함. 나중에 이 값은 블럭의 크기에서 정해야 함. 현재 블럭크기는 1로 되어 있음. 
//    public float scale = 0.1f;
//    public GameObject gridPrefab;

//    // first set position of block  - 어떻게 블럭생성 위치값을 갱산하면서 할당할 것인가?
//    // building 생성 코드에서 가져오면 됨. ㅠ

//    public Vector3 leftBottomLocation;

//    // 생성된 gioPoint를 props 리스트에 등록해주기 위한 리스트
//    public List<GameObject> props_ = new List<GameObject>();
//    public List<Vector3> prebPosAll_ = new List<Vector3>();

//    // 총 81개의 블for (int i = 0; i < spawnPrefabs.Count + 1; i++)
//    public List<Vector3> interSecPos_ = new List<Vector3>();

//    // 1차 계산
//    public List<Vector3> interSecPos__ = new List<Vector3>();

//    public List<Vector3> startPosEachBlock = new List<Vector3>();

//    // 2차 블럭의 4 지점의 위치를 저장한 리스트
//    public List<Vector3> PosEachBlock = new List<Vector3>();

//    //Post_PosEachBlock
//    public List<Vector3> Post_PosEachBlock = new List<Vector3>();

//    // 불러올 게임오브젝트 리스트 선언
//    private List<GameObject> GiposSpawnPrefabs = new List<GameObject>();

//    // GiposSpawnPrefabs_
//    private List<GameObject> GiposSpawnPrefabs_ = new List<GameObject>();



//    //void Awake()
//    // {
//    //     // 코드가 샐행되면 1차 계산, // get post value
//    //     PreGetOriginGioPos();

//    //     // 값 확인 테스트
//    //     foreach (Vector3 ps in Post_PosEachBlock)
//    //     {
//    //         Debug.Log("ps: " + ps);
//    //     }
//    // }


//    public void getGioPosInBlock()
//    {
//        Debug.Log("generate sub_gio_position!");


//        // get post value
//        //PreGetOriginGioPos();

//        // get updated value - current
//        getOriginGioPos();

//        if (gridPrefab)
//        {
//            int k = 0;
//            foreach (Vector3 StPos in startPosEachBlock)
//            {
//                // 작은 블럭단위의 시작 위치값 추출하여 반복 생성
//                //Debug.Log("StPos" + StPos);

//                leftBottomLocation = new Vector3(StPos.x, StPos.y, StPos.z);

//                // 업데이트 이전 지점의 위치
//                Vector3 postP1 = Post_PosEachBlock[k];
//                Debug.Log("postP1 :" + postP1);
//                Vector3 postP2 = Post_PosEachBlock[k + 1];
//                Vector3 postP3 = Post_PosEachBlock[k + 2];
//                Vector3 postP4 = Post_PosEachBlock[k + 3];


//                // 업데이트한 네 지점의 위치값
//                Vector3 updatedP1 = PosEachBlock[k];
//                Debug.Log("updatedP1 :" + updatedP1);
//                Vector3 updatedP2 = PosEachBlock[k + 1];
//                Vector3 updatedP3 = PosEachBlock[k + 2];
//                Vector3 updatedP4 = PosEachBlock[k + 3];

//                // 네 지점의 변화 방향 벡터
//                Vector3 P1 = postP1 - updatedP1;
//                Vector3 P2 = postP2 - updatedP2;
//                Vector3 P3 = postP3 - updatedP3;
//                Vector3 P4 = postP4 - updatedP4;

//                // 네 지점의 위치변화 크기(길이)
//                float DistP1 = Vector3.Distance(postP1, updatedP1);
//                //Debug.Log("DistP1 :" + DistP1);
//                float DistP2 = Vector3.Distance(postP2, updatedP2);
//                float DistP3 = Vector3.Distance(postP3, updatedP3);
//                float DistP4 = Vector3.Distance(postP4, updatedP4);

//                // -------------------------------------------------
//                //float GetDistance(Vector3 a, Vector3 b)
//                //{
//                //    Vector3 c = a - b;
//                //    float dist = Vector3.Magnitude(c);
//                //    return dist;
//                //}


//                //float disttP1 = GetDistance(postP1, updatedP1);
//                //float disttP2 = GetDistance(postP2, updatedP2);
//                //float disttP3 = GetDistance(postP3, updatedP3);
//                //float disttP4 = GetDistance(postP4, updatedP4);
//                //--------------------------------------------------


//                for (int i = 0; i < columns; i++)
//                {
//                    for (int j = 0; j < rows; j++)
//                    {
//                        GameObject obj = Instantiate(gridPrefab, new Vector3(leftBottomLocation.x + scale * i, leftBottomLocation.y, leftBottomLocation.z + scale * j), Quaternion.identity);

//                        //print("Instantiate1");
//                        obj.transform.SetParent(gameObject.transform);

//                        //print("Instantiate2");
//                        obj.GetComponent<GridStat>().x = i;

//                        //print("Instantiate3");
//                        obj.GetComponent<GridStat>().y = j;

//                        // obj 의 위치를 변경해야 함
//                        // 10% 만 적용하여 위치값 재조정
//                        Vector3 S1 = (P1 * DistP1 + obj.transform.position) * 0.25f;
//                        Vector3 S2 = (P2 * DistP2 + obj.transform.position) * 0.25f;
//                        Vector3 S3 = (P3 * DistP3 + obj.transform.position) * 0.25f;
//                        Vector3 S4 = (P4 * DistP4 + obj.transform.position) * 0.25f;


//                        // -------------------------------------------------
//                        //Vector3 S1 = (P1 * disttP1 + obj.transform.position) * 0.25f;
//                        //Vector3 S2 = (P2 * disttP2 + obj.transform.position) * 0.25f;
//                        //Vector3 S3 = (P3 * disttP3 + obj.transform.position) * 0.25f;
//                        //Vector3 S4 = (P4 * disttP4 + obj.transform.position) * 0.25f;
//                        // -------------------------------------------------


//                        obj.transform.position = S1 + S2 + S3 + S4;
//                        //Debug.Log("sub_Gio_pos 위치 :" + obj);

//                        props_.Add(obj);

//                    }

//                }
//                k += 4;
//            }
//        }
//        else print("missing gridprefab, please assign.");

//        // 생성된 프리팹의 모든 위치값을 추출 하여 저장한다. --> 이 값을 이제 lineRenderer로 보내서 도로를 그려주면된다.
//        getPosOfPrefabs();
//    }


//    public void ResetSubGioPosition()
//    {
//        Debug.Log("Reset! and regenerate sub_gio_position again!");
//        foreach (GameObject gitm_ in GameObject.FindGameObjectsWithTag("gioPoint_sub"))
//        {
//            //gitm_.SetActive(false);
//            Destroy(gitm_);
//        }

//        props_.Clear();
//        interSecPos_.Clear();
//        prebPosAll_.Clear();
//        startPosEachBlock.Clear();
//        GiposSpawnPrefabs.Clear();
//        GiposSpawnPrefabs_.Clear();
//        PosEachBlock.Clear();
//        Post_PosEachBlock.Clear();
//        interSecPos__.Clear();

//        //if (gridPrefab)
//        //    GenerateGrid();
//        //else print("missing gridprefab, please assign.");

//        //// 생성된 프리팹의 모든 위치값을 추출 하여 저장한다. --> 이 값을 이제 lineRenderer로 보내서 도로를 그려주면된다.
//        //getPosOfPrefabs();
//    }


//    void GenerateGrid()
//    {
//        for (int i = 0; i < columns; i++)
//        {
//            for (int j = 0; j < rows; j++)
//            {
//                GameObject obj = Instantiate(gridPrefab, new Vector3(leftBottomLocation.x + scale * i, leftBottomLocation.y, leftBottomLocation.z + scale * j), Quaternion.identity);

//                //print("Instantiate1");
//                obj.transform.SetParent(gameObject.transform);

//                //print("Instantiate2");
//                obj.GetComponent<GridStat>().x = i;

//                //print("Instantiate3");
//                obj.GetComponent<GridStat>().y = j;


//                //////// obj 의 위치를 변경해야 함

//                props_.Add(obj);


//            }
//        }
//    }





//    // 생성된 오브젝트의 위치를 추출해주는 함수를 만든다.
//    public void getPosOfPrefabs()
//    {
//        for (int i = 0; i < props_.Count; i++)
//        {
//            Vector3 getPosPreb = props_[i].transform.position;
//            // 생성된 프리팹의 위치값을 모두 저장한다.
//            prebPosAll_.Add(getPosPreb);
//        }
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

//            interSecPos_.Add(GiposSpawnPrefabs[i].transform.position);
//            interSecPos_.Add(GiposSpawnPrefabs[i + 1].transform.position);
//            interSecPos_.Add(GiposSpawnPrefabs[i + 10].transform.position);
//            interSecPos_.Add(GiposSpawnPrefabs[i + 11].transform.position);

//        }

//        int k = 0;
//        for (int i = 0; i <= interSecPos_.Count / 4 - 1; i++)
//        {
//            //Debug.Log("interSecPos[k]");
//            //브럭의 시작점 위치 추출
//            startPosEachBlock.Add(interSecPos_[k]);
//            //블럭의 1~4번째 위치 추
//            PosEachBlock.Add(interSecPos_[k]);
//            PosEachBlock.Add(interSecPos_[k + 1]);
//            PosEachBlock.Add(interSecPos_[k + 2]);
//            PosEachBlock.Add(interSecPos_[k + 3]);

//            k += 4;
//        }

//    }



//    public void PreGetOriginGioPos()
//    {
//        // GioPos 태그를 가진 GameObject를 모두 찾아서 새로운 리스트에 하나씩 담는다. 이것은 위치값을 추적하기 위함이다.
//        foreach (GameObject spawnp in GameObject.FindGameObjectsWithTag("GioPos"))
//        {
//            GiposSpawnPrefabs_.Add(spawnp);
//            //Debug.Log("check 1 !");
//        }

//        // 4 지점의 조합 리스트값이 필요함. ---> Blocks로 계산해야 ㅎ
//        for (int i = 0; i < GiposSpawnPrefabs_.Count - 11; i++)
//        {

//            if (i == 9 || i == 19 || i == 29 || i == 39 || i == 49 || i == 59 || i == 69 || i == 79 || i == 89 || i == 99 || i == 109)
//            {
//                continue;
//            }

//            interSecPos__.Add(GiposSpawnPrefabs_[i].transform.position);
//            interSecPos__.Add(GiposSpawnPrefabs_[i + 1].transform.position);
//            interSecPos__.Add(GiposSpawnPrefabs_[i + 10].transform.position);
//            interSecPos__.Add(GiposSpawnPrefabs_[i + 11].transform.position);

//        }

//        int k = 0;
//        for (int i = 0; i <= interSecPos__.Count / 4 - 1; i++)
//        {
//            //Debug.Log("interSecPos[k]");
//            //브럭의 시작점 위치 추출
//            //startPosEachBlock.Add(interSecPos_[k]);
//            //블럭의 1~4번째 위치 추
//            Post_PosEachBlock.Add(interSecPos__[k]);
//            Post_PosEachBlock.Add(interSecPos__[k + 1]);
//            Post_PosEachBlock.Add(interSecPos__[k + 2]);
//            Post_PosEachBlock.Add(interSecPos__[k + 3]);

//            k += 4;
//        }

//    }


//}
