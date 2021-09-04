using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using System;

// GioPos에서 개별 위치값을 추출한 후, 이 값의 포지션값을 토대로 매쉬를 그린다. 이 메쉬에만 빌딩을 생성하면된다. 

public class MeshGenerator : MonoBehaviour
{
    // Define  our world size
    public int Worldx;
    public int Worldz;

    //메쉬생성을 위한 선언
    private Mesh mesh;

    private int[] triangles;
    private Vector3[] verticies;



//// Find the intersection of two lines - 1
//    static PointF FindIntersection(PointF s1, PointF e1, PointF s2, PointF e2)
//    {
//        float a1 = e1.Y - s1.Y;
//        float b1 = s1.X - e1.X;
//        float c1 = a1 * s1.X + b1 * s1.Y;

//        float a2 = e2.Y - s2.Y;
//        float b2 = s2.X - e2.X;
//        float c2 = a2 * s2.X + b2 * s2.Y;

//        float delta = a1 * b2 - a2 * b1;
//        //If lines are parallel, the result will be (NaN, NaN).
//        return delta == 0 ? new PointF(float.NaN, float.NaN)
//            : new PointF((b2 * c1 - b1 * c2) / delta, (a1 * c2 - a2 * c1) / delta);
//    }










    void Start()
    {
        // 메쉬 생성 
        mesh = new Mesh();

        // 메쉬의 메쉬필터 속성에 생한 메를 넣고 
        GetComponent<MeshFilter>().mesh = mesh;

        // 메쉬 생성하고 
        GenerateMesh(); 

        // 반복해서 그려주면 됨
        UpdateMesh();
    }

    void GenerateMesh()
    {
        //// Find the intersection of two lines--2 >>> spawnBuilding에 이하 코드를 적용해보자
        //// 두 선이 교차하는 지점 추출, 이 지점이 spawn base position이 된다. 그리고 네 지점의 면적을 구해야 한다. 그 안에 스폰이 되는지 체크하여 안에 있으면 스폰하고, 밖이면 스폰X
        //Func<float, float, PointF> p = (x, y) => new PointF(x, y);
        //Debug.Log("FindIntersection : " + FindIntersection(p(4f, 0f), p(6f, 10f), p(0f, 3f), p(10f, 7f)));
        //// 평행성선일 경우는 nan, nan 출력 
        ////Debug.Log("FindIntersection : " + FindIntersection(p(0f, 0f), p(1f, 1f), p(1f, 2f), p(4f, 5f)));
        //// ---------------





        triangles = new int[Worldx * Worldz * 6]; // 메쉬를 그리는데 삼각형 2개가 필요하고 2개의 삼각형을 그리는데 필요한 정수값은 6개임.  
        verticies = new Vector3[(Worldx + 1) * (Worldz + 1)]; // 버텍스는 추가로 1개씩 축마다 필요함. 

        for (int i = 0, z = 0; z <= Worldz; z++)
        {
            for (int x = 0; x <= Worldx; x++)
            {
                verticies[i] = new Vector3(x, 0, z);
                i++;
            }
        }

        int tris = 0;
        int verts = 0;

        for (int z = 0; z < Worldz; z++)
        {
            for(int x = 0; x < Worldx; x++)
            {
                // 메쉬 생성을 위한 삼각형 데이터 생성
                triangles[tris + 0] = verts + 0;
                triangles[tris + 1] = verts + Worldz + 1;
                triangles[tris + 2] = verts + 1;

                // 두 번재 삼각형 데이터 생성
                triangles[tris + 3] = verts + 1;
                triangles[tris + 4] = verts + Worldz + 1;
                triangles[tris + 5] = verts + Worldz + 2;

                verts++;
                tris += 6;

            }
         verts++;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        // vertices 우선 생성
        mesh.vertices = verticies;
        // 점끼리 연결할 선 생성하고 삼각형 생성하여 메쉬생
        mesh.triangles = triangles;
        // 추출한 값에서 메수의 normal을 다시 계산 
        mesh.RecalculateNormals();
    }

}
