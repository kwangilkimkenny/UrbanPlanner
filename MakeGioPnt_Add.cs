using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MakeGioPnt_Add : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;

    private Vector3 mousePos;
    //private bool OnOff = false;

    //public GameObject SubPosObj_;


    // 생성할 GioPos 종류 선택기능 추가해야 함
    public GameObject BlockPosPrep;
    public GameObject SubBlockPosPrep;
    private Transform BlockPosParent;
    private Transform SubBlockPosParent;

    public GameObject BD_Prefab;

    List<GameObject> inst_All = new List<GameObject>();
    public Dictionary<int, List<GameObject>> dict_Add = new Dictionary<int, List<GameObject>>();

    List<GameObject> AddedBDs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("click! to instantiate.");
                Function_Instantiate();
            }
            if (Input.GetMouseButtonDown(1))
            {
                Function_Destroy();
            }
        }

    }



    private void Function_Destroy()
    {
        foreach (GameObject gitm_ in GameObject.FindGameObjectsWithTag("GioPos_"))
        {
            Destroy(gitm_);
        }
    }


    private void Function_Instantiate()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask);

        mousePos = raycastHit.point;
        Debug.Log("instantiate here : " + mousePos);

        Vector3 mousepos_ = new Vector3(mousePos.x, mousePos.y, mousePos.z);

        GameObject inst = Instantiate(BlockPosPrep, mousepos_, Quaternion.identity);
        inst.transform.position = mousepos_;

        inst_All.Add(inst);
    }



    // 4개씩 추출하여 블럭의 내부 포지션을 생성한다. 4개가 아니면 메시지 출력한다. 조건문 추가할 것!

    public void getNewAddedSubPos()
    {

        int key_ = 0;
        for (int k = 0; k < inst_All.Count; k += 4)
        {

            GameObject P1 = inst_All[k];
            GameObject P2 = inst_All[k + 1];
            GameObject P3 = inst_All[k + 2];
            GameObject P4 = inst_All[k + 3];


            int RangeNums = 10;
            int arrNumOfBlk = 10;
            float div = 1f / RangeNums;



            for (int i = 0; i < arrNumOfBlk; i++) //10 개만 생성 
            {

                if (!dict_Add.ContainsKey(key_))
                {
                    // 선언
                    dict_Add.Add(key_, new List<GameObject>());
                }

                // Vertical 
                Vector3 newPos1 = Vector3.Lerp(P1.transform.position, P2.transform.position, div);
                GameObject subPos1 = Instantiate(SubBlockPosPrep, newPos1, Quaternion.identity);

                dict_Add[key_].Add(subPos1);

                Vector3 newPos4 = Vector3.Lerp(P3.transform.position, P4.transform.position, div);
                GameObject subPos4 = Instantiate(SubBlockPosPrep, newPos4, Quaternion.identity);

                dict_Add[key_].Add(subPos4);


                // 위에서 생성한 subPos를 기반으로 블록 내부의 subGioPos 추가


                int num = 1;
                float div_ = 1f / RangeNums;
                while (num <= RangeNums)
                {
                    Vector3 subNewPos1 = Vector3.Lerp(subPos1.transform.position, subPos4.transform.position, div_);
                    //Debug.Log("subNewPos1");
                    GameObject subPos1_ = Instantiate(SubBlockPosPrep, subNewPos1, Quaternion.identity);

                    dict_Add[key_].Add(subPos1_);

                    num++;
                    div_ += (1f / RangeNums);
                }

                div += (1f / RangeNums);
            }
            key_ += 1;
        }

    }


    public void ClearAllSubRoadPoints_()
    {
        GameObject[] allRoadPonts = GameObject.FindGameObjectsWithTag("gioPoint_sub_");

        foreach (GameObject p in allRoadPonts)
        {
            //Destroy(p);
            p.SetActive(false);
        }

        //GameObject[] getSubRoadLine = GameObject.FindGameObjectsWithTag("subRoad");

        //foreach (GameObject q in getSubRoadLine)
        //{
        //    Destroy(q);
        //}

    }



    // 추가된 블럭안의 빌딩 생성
    public void SpawnBD()
    {
        if (dict_Add != null)
        {
            Debug.Log("추가빌딩 생성");
            int SpawnBD = 0;
            for (int i = 0; i <= dict_Add.Count - 1; i++)
            {

                foreach (GameObject obj in dict_Add[i])
                {
                    Debug.Log("obj" + obj);

                    GameObject SpBD = Instantiate(BD_Prefab, new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z), Quaternion.identity);

                    AddedBDs.Add(SpBD);
                    SpawnBD += 1;
                }
            }
        }
        else
        {
            Debug.Log("Before creating a building, the point of the block must be set.");
        }

    }



    public void ResetAllBDs_Added()
    {
        foreach (GameObject buildings in GameObject.FindGameObjectsWithTag("building"))
        {

            Destroy(buildings);
        }
    }



}
