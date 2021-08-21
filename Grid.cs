using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
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
        for(int i = 0; i < props.Count; i++)
        {
            Vector3 getPosPreb = props[i].transform.position;
            // 생성된 프리팹의 위치값을 모두 저장한다.
            prebPosAll.Add(getPosPreb);
        }
    }

    //void SetDistance()
    //{
    //    InitialSetUp();
    //    int x = startX;
    //    int y = startY;
    //    int[] testArray = new int[rows * columns];
    //    for(int step = 1; step < rows * columns; step++)
    //    {
    //        foreach(GameObject obj in gridArray)
    //        {
    //            if (obj.GetComponent<GridStat>().visited == step - 1)
    //                TestFourDirections(obj.GetComponent<GridStat>().x, obj.GetComponent<GridStat>().y, step);
    //        }
    //    }

    //}

    //void SetPath()
    //{
    //    int step;
    //    int x = endX;
    //    int y = endY;
    //    List<GameObject> tempList = new List<GameObject>();
    //    path.Clear();
    //    if (gridArray[endX, endY] && gridArray[endX,endY].GetComponent<GridStat>().visited > 0)
    //    {
    //        path.Add(gridArray[x, y]);
    //         step = gridArray[x, y].GetComponent<GridStat>().visited - 1;
    //    }
    //    else
    //    {
    //        print(" Can't reach the desired location");
    //        return;
    //    }
    //    for(int i = step; step > -1; step--)
    //    {
    //        if (TeststDirection(x, y, step, 1))
    //            tempList.Add(gridArray[x, y + 1]);
    //        if (TeststDirection(x, y, step, 2))
    //            tempList.Add(gridArray[x+1, y]);
    //        if (TeststDirection(x, y, step, 3))
    //            tempList.Add(gridArray[x, y - 1]);
    //        if (TeststDirection(x, y, step, 4))
    //            tempList.Add(gridArray[x - 1, y]);
    //        GameObject tempObj = FindClosest(gridArray[endX, endY].transform, tempList);
    //        path.Add(tempObj);
    //        x = tempObj.GetComponent<GridStat>().x;
    //        y = tempObj.GetComponent<GridStat>().y;
    //        tempList.Clear();

    //    }

    //}

    //// added code
    //void InitialSetUp()
    //{
    //    foreach(GameObject obj in gridArray)
    //    {
    //        obj.GetComponent<GridStat>().visited = -1; // Getcomponent로 속성값 가져오
    //    }
    //    gridArray[startX, startY].GetComponent<GridStat>().visited = 0;
    //}

    //bool TeststDirection(int x, int y, int step, int direction)
    //{
    //    // 1 up, 2 right, 3 down, 4 left
    //    switch(direction)
    //    {
    //        case 1: // up

    //            if (x -1 < rows && gridArray[x +1 ,y] && gridArray[x+1,y].GetComponent<GridStat>().visited == step)
    //                return true;
    //            else
    //                return false;

    //        case 2: // right

    //            if (x + 1 < columns && gridArray[x + 1, y] && gridArray[x+1, y].GetComponent<GridStat>().visited == step)
    //                return true;
    //            else
    //                return false;
    //        case 3: // down

    //            if (y - 1 < -1 && gridArray[x, y - 1] && gridArray[x,y-1].GetComponent<GridStat>().visited == step)
    //                return true;
    //            else
    //                return false;
    //        case 4: // left

    //            if (y - 1 < -1 && gridArray[x + 1, y] && gridArray[x + 1, y].GetComponent<GridStat>().visited == step)
    //                return true;
    //            else
    //                return false;
    //    }
    //    return false;
    //}

    //void TestFourDirections(int x, int y, int step)
    //{
    //    if (TeststDirection(x, y, -1, 1))
    //        SetVisited(x, y + 1, step);
    //    if (TeststDirection(x, y, -1, 2))
    //        SetVisited(x + 1, y, step);
    //    if (TeststDirection(x, y, -1, 3))
    //        SetVisited(x, y - 1, step);
    //    if (TeststDirection(x, y, -1, 4))
    //        SetVisited(x - 1, y, step);
    //}

    //void SetVisited (int x, int y, int step)
    //{
    //    if (gridArray[x, y])
    //        gridArray[x, y].GetComponent<GridStat>().visited = step;
    //}

    //GameObject FindClosest(Transform targetLocation, List<GameObject> list)
    //{
    //    float currentDistance = scale * rows * columns;
    //    int indexNumber = 0;
    //    for(int i = 0; i<list.Count; i++)
    //    {
    //        if (Vector3.Distance(targetLocation.position, list[i].transform.position) < currentDistance)
    //        {
    //            currentDistance = Vector3.Distance(targetLocation.position, list[i].transform.position);
    //            indexNumber = i;
    //        }
    //    }
    //    return list[indexNumber];
    //}
}













//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Grid : MonoBehaviour
//{
//    public int rows = 10;
//    public int columns = 10;
//    public int scale = 1;
//    public GameObject gridPrefab;
//    public Vector3 leftBottomLocation = new Vector3(0, 0, 0);


//    //public int x = 0;
//    //public int y = 0;

//    // Start is called before the first frame update
//    void Awake()
//    {
//        if (gridPrefab)
//            GenerateGrid();
//        else print("missing gridprefab, please assign.");
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    void GenerateGrid()
//    {
//        for (int i = 0; i < columns; i++)
//        {
//            for (int j = 0; j < rows; j++)
//            {
//                GameObject obj = Instantiate(gridPrefab, new Vector3(leftBottomLocation.x + scale * i, leftBottomLocation.y, leftBottomLocation.z + scale * j), Quaternion.identity);

//                print("Instantiate1");
//                obj.transform.SetParent(gameObject.transform);

//                print("Instantiate2");
//                obj.GetComponent<GridStat>().x = i;

//                print("Instantiate3");
//                obj.GetComponent<GridStat>().y = j;

//                print("hi");
//            }
//        }
//    }
//}