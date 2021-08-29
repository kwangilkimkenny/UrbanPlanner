using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenLogic : MonoBehaviour
{
    public int rows = 10;
    public int columns = 10;
    public int scale = 1;
    public GameObject gridPrefab;
    public Vector3 leftBottomLocation = new Vector3(0, 0, 0);

    // 생성된 gioPoint를 props 리스트에 등록해주기 위한 리스트
    public List<GameObject> props = new List<GameObject>();
    public List<Vector3> prebPosAll = new List<Vector3>();
    // added code
    //public GameObject[,] gridArray;
    //public int startX = 0;
    //public int startY = 0;
    //public int endX = 2;
    //public int endY = 2;
    //public List<GameObject> path = new List<GameObject>();

    //public int x = 0;
    //public int y = 0;

    // Start is called before the first frame update
    void Awake()
    {

        // added code
        // gridArray = new GameObject[columns, rows];


        if (gridPrefab)
            GenerateGrid();
        else print("missing gridprefab, please assign.");

        // 생성된 프리팹의 모든 위치값을 추출 하여 저장한다. --> 이 값을 이제 lineRenderer로 보내서 도로를 그려주면된다.
        getPosOfPrefabs();
    }

    // Update is called once per frame
    void Update()
    {

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
                props.Add(obj);


            }
        }
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