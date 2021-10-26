using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridGenLogic : MonoBehaviour
{
    //public GridGenLogic_Block pre_get_origins_giopos;

    public int rows = 10;
    public int columns = 10;
    public int scale = 1;
    public GameObject gridPrefab;
    public Vector3 leftBottomLocation = new Vector3(0, 30, 0);
    //public Vector3 leftBottomLocation = new Vector3(0, 0, 0);

    // 생성된 gioPoint를 props 리스트에 등록해주기 위한 리스트
    public List<GameObject> props = new List<GameObject>();
    public List<Vector3> prebPosAll = new List<Vector3>();

    public string Post_PosEachBlock { get; private set; }

    public RaycastItemAligner rayAli;

    public int raycastDis = 100;

    public Text earthVolumeText;
    public Text post_earth_volume;
    public Text getEarVolText;


    private bool get_e_volume = true;

    public float resultOfEarthVolume;
    public float re_post_e_volume;


    // Start is called before the first frame update
    void Awake()
    {
        //// 사전 GioPos위치값 추출을 위한 함수 실행
        //this.gameObject.GetComponent<GridGenLogic_Block>().PreGetOriginGioPos();

        //Debug.Log("GioPos의 사전 위치값 생성 체크!");


        if (gridPrefab)
            GenerateGrid();
        else print("missing gridprefab, please assign.");

        // 생성된 프리팹의 모든 위치값을 추출 하여 저장한다. --> 이 값을 이제 lineRenderer로 보내서 도로를 그려주면된다.
        getPosOfPrefabs();
    }

    // GioPos 생성 - 버튼에 적용하는 실행함수
    public void getGioPos()
    {
        //    // 리스트 변수 삭제, 다음 코드에서 새로 생성할거임
        //    this.gameObject.GetComponent<GridGenLogic_Block>().Post_PosEachBlock.Clear();

        if (gridPrefab)
        {
            //// 사전 GioPos위치값 추출을 위한 함수 실행
            //pre_get_origins_giopos = this.gameObject.GetComponent<GridGenLogic_Block>();
            //pre_get_origins_giopos.GetComponent<GridGenLogic_Block>().PreGetOriginGioPos();

            GenerateGrid();
        }
        else print("missing gridprefab, please assign.");

        // 생성된 프리팹의 모든 위치값을 추출 하여 저장한다. --> 이 값을 이제 lineRenderer로 보내서 도로를 그려주면된다.
        getPosOfPrefabs();
    }


    private int check_cal_num = 1;

    // GioPos 파되 - 버튼에 적용하는 실행함
    public void resetGioPosition()
    {
        foreach (GameObject gitm in GameObject.FindGameObjectsWithTag("GioPos"))
        {
            //gitm.SetActive(false);
            Destroy(gitm);
        }

        props.Clear();
        prebPosAll.Clear();

        // 토공량 값 초기
        rayAli.GetComponent<RaycastItemAligner>().earthVolume = 0f;

        // 토공량 두번째 계산 이후에 초기화 ========!!!!! 코드 수정할 것!!!
        if (check_cal_num % 2 == 0)
        {
            resultOfEarthVolume = 0;
            earthVolumeText.text = "Pre Earth Volume : " + resultOfEarthVolume.ToString();

            re_post_e_volume = 0;
            post_earth_volume.text = "Post Earth Volume : " + re_post_e_volume.ToString();

            float get_earth_vol_value = 0; ;
            getEarVolText.text = "Get Earth Volume : " + get_earth_vol_value.ToString();

            Debug.Log("reset check : " + check_cal_num);
        }
        check_cal_num += 1;
        //Debug.Log("reset check : " + check_cal_num);
    }

    private int checkNum = 1;

    public void GenerateGrid()
    {
        if ( (checkNum % 2) != 0 )
        {
            Debug.Log("토공 전 지형의 부피 계산!");
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    GameObject obj = Instantiate(gridPrefab, new Vector3(leftBottomLocation.x + scale * i, leftBottomLocation.y, leftBottomLocation.z + scale * j), Quaternion.identity);

                    obj.transform.SetParent(gameObject.transform);

                    // class RaycastItemAligner 에서 위치값 obj를 입력하면 터레인에 레이캐스트를 하여 위치정보값을 추출 후 반환 
                    rayAli.GetComponent<RaycastItemAligner>().PositionRaycast(obj);

                    obj.transform.position = rayAli.itmPos;

                    //Debug.Log("obj posiiton : " + obj.transform.position);

                    // 생성된 obj를 리스트에 등록해준다. 그러면 생성된 obj들을 모두 추적할 수 있다.
                    props.Add(obj);


                    // 토공량 계산하기 : 처음값 추출
                    if (get_e_volume == true)
                    {
                        rayAli.GetComponent<RaycastItemAligner>().EarthVolume(obj);
                        resultOfEarthVolume = rayAli.earthVolume;
                        earthVolumeText.text = "Pre Earth Volume : " + resultOfEarthVolume.ToString();

                    }
                    //else
                    //{
                    //    rayAli.GetComponent<RaycastItemAligner>().Post_EarthVolume(obj);
                    //    re_post_e_volume = rayAli.earthVolume_;
                    //}
                }
            }
            get_e_volume = false;
            checkNum += 1;
        }
        else
        {
            Debug.Log("토공 후 지형의 부피 계산!");
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    GameObject obj = Instantiate(gridPrefab, new Vector3(leftBottomLocation.x + scale * i, leftBottomLocation.y, leftBottomLocation.z + scale * j), Quaternion.identity);

                    obj.transform.SetParent(gameObject.transform);

                    // class RaycastItemAligner 에서 위치값 obj를 입력하면 터레인에 레이캐스트를 하여 위치정보값을 추출 후 반환 
                    rayAli.GetComponent<RaycastItemAligner>().PositionRaycast(obj);

                    obj.transform.position = rayAli.itmPos;

                    //Debug.Log("obj posiiton : " + obj.transform.position);

                    // 생성된 obj를 리스트에 등록해준다. 그러면 생성된 obj들을 모두 추적할 수 있다.
                    props.Add(obj);

                    rayAli.GetComponent<RaycastItemAligner>().Post_EarthVolume(obj);
                    re_post_e_volume = rayAli.earthVolume_;
                    post_earth_volume.text = "Post Earth Volume : " + re_post_e_volume.ToString();

                }
            }

            get_re_e_volume();
        }

    }


    // 토공량 계산, 버튼으로 작동하기
    public void get_re_e_volume()
    {

        float get_earth_vol_value = resultOfEarthVolume - re_post_e_volume;
        getEarVolText.text = "Earth Volume : " + get_earth_vol_value.ToString();

    }




    // 생성된 오브젝트의 위치를 추출해주는 함수를 만든다.
    public void getPosOfPrefabs()
    {
        for (int i = 0; i < props.Count; i++)
        {
            Vector3 getPosPreb = props[i].transform.position;
            // 생성된 프리팹의 위치값을 모두 저장한다.
            prebPosAll.Add(getPosPreb);
        }
    }

    
}