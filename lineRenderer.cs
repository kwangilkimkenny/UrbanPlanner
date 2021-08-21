// 두점을 연결하하고 도로재질로 연결

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class lineRenderer : MonoBehaviour
{

    private LineRenderer lr;
    public Material roadMaterial;
    public float textureTiling = 1;

    GameObject GioPosition;

    private int i;

    public List<GameObject> spawnPrefabs = new List<GameObject>();

    //public GameObject[,,] gridArray;


    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.material = new Material(roadMaterial);

        // Set some positions
        Vector3[] positions = new Vector3[3];
        positions[0] = new Vector3(-2.0f, -2.0f, 0.0f);
        positions[1] = new Vector3(0.0f, 2.0f, 0.0f);
        positions[2] = new Vector3(2.0f, -2.0f, 0.0f);
        lr.positionCount = positions.Length;
        lr.SetPositions(positions);
    }

    private void Update()
    {
        // GioPosition 리스트의 프리팹에서 모든 위치를 추출한다. 배열 형태로 추출 --> FindGameObjectWithTag
        GioPosition = GameObject.FindGameObjectWithTag("GioPos");
        //print("GioPosition : " + GioPosition);

        // GioPosition 리스트의 프리팹에서 모든 위치를 추출한다. 
  

        // 그리고 포지션기리 라인랜더러로 라인을 그리면 된다.

        // Vector3[] positions_ = new Vector3[GioPosition.Length];
        // print("positions_ :" + positions_);

        //for (i = 0; i < GioPosition.Length; i++)
        //{
        //    positions_[i] = new Vector3(GioPosition[i].Transform.Position.x, GioPosition[i].Transform.Position.y, GioPosition[i].Transform.Position.z);

        //}
    }

}






//public class lineRenderer : MonoBehaviour
//{

//    public LineRenderer line;
//    public GameObject getPnt;





//    //public Transform pos1;
//    //public Transform pos2;

//    // Start is called before the first frame update

//    void Start()
//    {
//        line.positionCount = 2;

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //마우스로 클릭하여 생성한, 오브젝트를 가져오기, 태그를 이용해서 가져오기
//        getPnt = GameObject.FindWithTag("GioPos");
//        Debug.Log("getPnt:  " + getPnt);

//        // Set some positions
//        //line.SetPosition(0, getPnt.transform.position);
//        //line.SetPosition(1, getPnt.transform.position);
//    }
//}
