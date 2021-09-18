using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// GridPos 의 생성 위치를 할당하고
// 생성 prefabs 간의 길이를 해당 블럭의 크기에 맞게 비율로 조절한다.

public class GridGenLogic_Block : MonoBehaviour
{

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

    public List<Vector3> startPosEachBlock = new List<Vector3>();



    // 불러올 게임오브젝트 리스트 선언
    private List<GameObject> GiposSpawnPrefabs = new List<GameObject>();


    public void getGioPosInBlock()
    {
        Debug.Log("generate sub_gio_position!");

        getOriginGioPos();

        if (gridPrefab)
        {
            foreach (Vector3 StPos in startPosEachBlock)
            {
                // 작은 블럭단위의 시작 위치값 추출하여 반복 생성
                //Debug.Log("StPos" + StPos);

                leftBottomLocation = new Vector3(StPos.x, StPos.y, StPos.z);

                GenerateGrid();

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

                // 생성된 obj를 리스트에 등록해준다. 그러면 생성된 obj들을 모두 추적할 수 있다.
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
            //블럭의 2~4번째 위치 추출
            startPosEachBlock.Add(interSecPos_[k+1]);
            startPosEachBlock.Add(interSecPos_[k+2]);
            startPosEachBlock.Add(interSecPos_[k+3]);

            k += 4;
        }

    }
}
