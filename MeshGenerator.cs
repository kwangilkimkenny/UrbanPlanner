using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using System;

// GioPos에서 개별 위치값을 추출한 후, 이 값의 포지션값을 토대로 매쉬를 그린다. 이 메쉬에만 빌딩을 생성하면된다. 

public class MeshGenerator : MonoBehaviour
{

    //메쉬생성을 위한 선언
    public Mesh mesh;


    public List<Mesh> meshes = new List<Mesh>();


    // spawnPrefabs는 실제 spawn BD에서 게산할 값들임
    public List<GameObject> spawnPrefabs = new List<GameObject>();
    public List<GameObject> blocks = new List<GameObject>();
    




    public void GetMeshGen()
    {
        // 메쉬 생성 
        mesh = new Mesh();
        //mesh2 = new Mesh();

        // 메쉬의 메쉬필터 속성에 생한 메를 넣고 
        GetComponent<MeshFilter>().mesh = mesh;
        //GetComponent<MeshFilter>().mesh = mesh2;

        // 메쉬 생성하고 
        GenerateMesh();

    }



    void GetBlokPos()
    {
        //GioPos 태그를 가진 GameObject를 모두 찾아서 새로운 리스트에 하나씩 담는다. 이것은 위치값을 추적하기 위함이다.
        foreach (GameObject spawnp in GameObject.FindGameObjectsWithTag("GioPos"))
        {
            spawnPrefabs.Add(spawnp);
        }

        Debug.Log("spawnPrefabs.Count :" + spawnPrefabs.Count); // 100


        // 4 지점의 조합 리스트값이 필요함. ---> Blocks로 계산해야 ㅎ
        for (int i = 0; i < spawnPrefabs.Count - 11; i++)
        {

            if (i == 9 || i == 19 || i == 29 || i == 39 || i == 49 || i == 59 || i == 69 || i == 79 || i == 89 || i == 99 || i == 109)
            {
                continue;
            }

            blocks.Add(spawnPrefabs[i]);
            blocks.Add(spawnPrefabs[i + 1]);
            blocks.Add(spawnPrefabs[i + 10]);
            blocks.Add(spawnPrefabs[i + 11]);

        }

        Debug.Log("blocks.Count :" + blocks.Count); // 324개로 4로 나누면 81개, 9X9의 블럭으로 구성되었기 때문

        
    }






    void GenerateMesh()
    {


        Vector3[] verticies = new Vector3[12];
        int[] triangles = new int[18];



        //Debug.Log("4의 값이 총 81개의 블럭으로 324가 나오는가? :" + blocks.Count);
        GetBlokPos();// 324개가 나옴!

        verticies[0] = new Vector3(blocks[0].transform.position.x, 0, blocks[0].transform.position.z);
        verticies[1] = new Vector3(blocks[1].transform.position.x, 0, blocks[1].transform.position.z);
        verticies[2] = new Vector3(blocks[2].transform.position.x, 0, blocks[2].transform.position.z);
        verticies[3] = new Vector3(blocks[3].transform.position.x, 0, blocks[3].transform.position.z);

        verticies[4] = new Vector3(blocks[4].transform.position.x, 0, blocks[4].transform.position.z);
        verticies[5] = new Vector3(blocks[5].transform.position.x, 0, blocks[5].transform.position.z);
        verticies[6] = new Vector3(blocks[6].transform.position.x, 0, blocks[6].transform.position.z);
        verticies[7] = new Vector3(blocks[7].transform.position.x, 0, blocks[7].transform.position.z);


        verticies[8] = new Vector3(blocks[8].transform.position.x, 0, blocks[8].transform.position.z);
        verticies[9] = new Vector3(blocks[9].transform.position.x, 0, blocks[9].transform.position.z);
        verticies[10] = new Vector3(blocks[10].transform.position.x, 0, blocks[10].transform.position.z);
        verticies[11] = new Vector3(blocks[11].transform.position.x, 0, blocks[11].transform.position.z);


        //Debug.Log("verticies! : " + verticies[0]);
        //Debug.Log("verticies 1! : " + verticies[1]);
        //Debug.Log("verticies 2! : " + verticies[2]);
        //Debug.Log("verticies 3! : " + verticies[3]);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 1;
        triangles[4] = 3;
        triangles[5] = 2;

        triangles[6] = 1;
        triangles[7] = 4;
        triangles[8] = 3;
        triangles[9] = 4;
        triangles[10] = 5;
        triangles[11] = 3;

        triangles[12] = 4;
        triangles[13] = 6;
        triangles[14] = 5;
        triangles[15] = 6;
        triangles[16] = 5;
        triangles[17] = 7;



        mesh.Clear();
        // vertices 우선 생성
        mesh.vertices = verticies;
        // 점끼리 연결할 선 생성하고 삼각형 생성하여 메쉬생
        mesh.triangles = triangles;
        // 추출한 값에서 메수의 normal을 다시 계산 
        mesh.RecalculateNormals();
    }


    public void DeleteMesh()
    {
        mesh.Clear();
    }

}