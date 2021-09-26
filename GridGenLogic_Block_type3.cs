using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenLogic_Block_type3 : MonoBehaviour
{
    // 불러올 게임오브젝트 리스트 선언
    private List<GameObject> GiposSpawnPrefabs = new List<GameObject>();
    // 1차 계산
    public List<GameObject> interSecObj = new List<GameObject>();
    // 총 81개의 블for (int i = 0; i < spawnPrefabs.Count + 1; i++)
    public List<GameObject> interSecPos_ = new List<GameObject>();
    public List<Vector3> startPosEachBlock = new List<Vector3>();
    // 2차 블럭의 4 지점의 위치를 저장한 리스트
    public List<GameObject> PosEachBlock = new List<GameObject>();

    public GameObject SubPosObj;


    public void DrawSubRoads()
    {
        // get updated value - current
        getOriginGioPos();

        int k = 0;
        foreach (GameObject psn in PosEachBlock)
        {
            if (k < PosEachBlock.Count )
            {

                GameObject P1 = PosEachBlock[k];
                GameObject P2 = PosEachBlock[k + 1];
                GameObject P3 = PosEachBlock[k + 2];
                GameObject P4 = PosEachBlock[k + 3];

                int RangeNums = Random.Range(2, 4);

                for (int i = 0; i < RangeNums; i++)
                {

                    Debug.Log("k:" + k);
                    // 블럭의 중간 생성지점 랜덤으로 포인트 생성
                    float div = Random.Range(0f, 1f);
                    //Debug.Log("div:" + div);

                    //Vector3.Lerp(vec3 from, vec3 to, float time)
                    Vector3 newPos1 = Vector3.Lerp(P1.transform.position, P2.transform.position, div);
                    GameObject subPos1 = Instantiate(SubPosObj, newPos1, Quaternion.identity);

                    //Vector3 newPos2 = Vector3.Lerp(P2.transform.position, P4.transform.position, div);
                    //GameObject subPo2 = Instantiate(SubPosObj, newPos2, Quaternion.identity);

                    Vector3 newPos3 = Vector3.Lerp(P1.transform.position, P3.transform.position, div);
                    GameObject subPos3 = Instantiate(SubPosObj, newPos3, Quaternion.identity);

                    //Vector3 newPos4 = Vector3.Lerp(P3.transform.position, P4.transform.position, div);
                    //GameObject subPos4 = Instantiate(SubPosObj, newPos4, Quaternion.identity);
                    // ㄱ 자 부분 생성

                    if (k == 32 || k == 68 || k == 104 || k == 140 || k == 176 || k == 212 || k == 248 || k == 284|| k == 320)                           
                    {
                        Debug.Log("div:" + div);
                         
                        Vector3 newPos2 = Vector3.Lerp(P2.transform.position, P4.transform.position, div);
                        GameObject subPo2 = Instantiate(SubPosObj, newPos2, Quaternion.identity);


                        Vector3 newPos4 = Vector3.Lerp(P3.transform.position, P4.transform.position, div);
                        GameObject subPos4 = Instantiate(SubPosObj, newPos4, Quaternion.identity);
                    }
                }
            } 
            k += 4;
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
        Debug.Log("Reset! and regenerate sub_gio_position again!");
        foreach (GameObject gitm_ in GameObject.FindGameObjectsWithTag("gioPoint_sub"))
        {
            //gitm_.SetActive(false);
            Destroy(gitm_);
        }


        interSecPos_.Clear();
        startPosEachBlock.Clear();
        GiposSpawnPrefabs.Clear();
        PosEachBlock.Clear();
        interSecObj.Clear();
        Destroy(SubPosObj);

        //if (gridPrefab)
        //    GenerateGrid();
        //else print("missing gridprefab, please assign.");

        //// 생성된 프리팹의 모든 위치값을 추출 하여 저장한다. --> 이 값을 이제 lineRenderer로 보내서 도로를 그려주면된다.
        //getPosOfPrefabs();
    }


}