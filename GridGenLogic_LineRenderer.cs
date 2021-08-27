using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenLogic_LineRenderer : MonoBehaviour
{

    private LineRenderer lr;
    public Material roadMaterial;
    //public float textureTiling = 1;

    // 불러올 게임오브젝트 리스트 선언
    public List<GameObject> spawnPrefabs = new List<GameObject>();

    // 위치값을 저장하는 리스트 선언
    public List<Vector3> GioPoses = new List<Vector3>();


    //public GameObject[,,] gridArray;


    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.material = new Material(roadMaterial);

        // GioPos 태그를 가진 GameObject를 모두 찾아서 새로운 리스트에 하나씩 담는다.
        foreach (GameObject spawnpreb in GameObject.FindGameObjectsWithTag("GioPos"))
        {
            spawnPrefabs.Add(spawnpreb);
        }

        // 리스트에서 엘리먼트를 하나씩 꺼내서 Vector3의 값을 추출한 후 다시 새 리스트(GioPoses)에 저장

        foreach (GameObject i in spawnPrefabs)
        {
            GioPoses.Add(i.transform.position);
        }

        // 저정된 리스트 확인
        //Debug.Log("GioPoses : " + GioPoses);

        // 리스트의 위치값을 가지고 라인랜더링하기
        Vector3[] positions = new Vector3[GioPoses.Count];
        for (int i = 0; i < GioPoses.Count; i++)
        {
            positions[i] = GioPoses[i];
            // Debug.Log("giopos: " + positions[i]);
        }


        lr.positionCount = positions.Length;
        lr.SetPositions(positions);



        // getPosPreb();

        // Set some positions
        //Vector3[] positions_ = new Vector3[3];
        //positions_[0] = new Vector3(-2.0f, -2.0f, 0.0f);
        //positions_[1] = new Vector3(0.0f, 2.0f, 0.0f);
        //positions_[2] = new Vector3(2.0f, -2.0f, 0.0f);
        //lr.positionCount = positions_.Length;
        //lr.SetPositions(positions_);


    }

    private void Update()
    {


    }






}