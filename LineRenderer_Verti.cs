// 두점을 연결하하고 도로재질로 연결

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class LineRenderer_Verti : MonoBehaviour
{

    private LineRenderer lr;
    public Material roadMaterial;

    // 불러올 게임오브젝트 리스트 선언
    public List<GameObject> spawnPrefabs = new List<GameObject>();

    // 위치값을 저장하는 리스트 선언
    public List<Vector3> GioPoses = new List<Vector3>();

    // 위치값 저장(계산용)
    public List<Vector3> firstPos = new List<Vector3>();
    // 위치값 저장(계산용)
    public List<Vector3> SecondPos = new List<Vector3>();

    // 생성된 GioPos의 지점간 거리
    private float Dist, Dist1, Dist2, Dist3, Dist4;

    // 생성된 GioPos의 지점간 거리 2
    private float Dist_;

    // 지점간 거리를 모은 리스트 선언으로 여기의 리스트에는 3개의 값이 저장되어야 함 GioPos1, GioPos2, Distance
    private List<float> Dists = new List<float>();

    // 지점간 거리를 모은 리스트 선언으로 여기의 리스트에는 3개의 값이 저장되어야 함 GioPos1, GioPos2, Distance 두 번째 작은 거리값 저장
    private List<float> Dists_ = new List<float>();

    private List<float> second_Dists_list = new List<float>();

    // 생성된 지점의 오브젝트의 순서를 저장하기 위한 인덱스 리스트
    private List<GameObject> GioPosIndex = new List<GameObject>();

    // 생성된 지점의 오브젝트의 순서를 저장하기 위한 인덱스 두 번째 리스트
    private List<int> GioPosIndex_ = new List<int>();

    // GioPos의 2개 포지션 값 저장리스트
    private List<Vector3> TwoPos = new List<Vector3>();



    private List<Vector3> spawnPrefabs_ = new List<Vector3>();

    public List<Vector3> temp = new List<Vector3>();
    public List<Vector3> temp2 = new List<Vector3>();
    public List<Vector3> temp3 = new List<Vector3>();
    public List<Vector3> temp4 = new List<Vector3>();
    public List<Vector3> temp5 = new List<Vector3>();
    public List<Vector3> temp6 = new List<Vector3>();



    public void DrawRoadVerti()
    {
        lr = GetComponent<LineRenderer>();
        lr.material = new Material(roadMaterial);

        // GioPos 태그를 가진 GameObject를 모두 찾아서 새로운 리스트에 하나씩 담는다.
        foreach (GameObject spawnpreb in GameObject.FindGameObjectsWithTag("GioPos"))
        {
            spawnPrefabs.Add(spawnpreb);
        }




        for (int i = 0; i < spawnPrefabs.Count + 1; i++)
        {
            if (i < 10)
            {
                spawnPrefabs_.Add(spawnPrefabs[i].transform.position);

            }
            else if (10 <= i && i < 20)
            {
                //List<Vector3> temp = new List<Vector3>();
                temp.Add(spawnPrefabs[i].transform.position);
                // 위에서 저장한 값의 순서를 바꿔서 spawnPrefabs_ 에 저장


            }
            else if (20 <= i && i < 30)
            {

                if (i == 20)
                {
                    temp.Reverse(); // 순서를 바꿔준다.

                    foreach (Vector3 j in temp)
                    {
                        spawnPrefabs_.Add(j);
                        //Debug.Log("spawnPrefabs_ : " + j);
                    }
                }


                spawnPrefabs_.Add(spawnPrefabs[i].transform.position);
            }
            else if (30 <= i && i < 40)
            {
                //List<Vector3> temp2 = new List<Vector3>();
                temp2.Add(spawnPrefabs[i].transform.position);

            }
            else if (40 <= i && i < 50)
            {
                if (i == 40)
                {
                    temp2.Reverse(); // 순서를 바꿔준다.

                    foreach (Vector3 k in temp2)
                    {
                        spawnPrefabs_.Add(k);
                    }
                }


                spawnPrefabs_.Add(spawnPrefabs[i].transform.position);
            }
            else if (50 <= i && i < 60)
            {
                //List<Vector3> temp3 = new List<Vector3>();
                temp3.Add(spawnPrefabs[i].transform.position);

            }
            else if (60 <= i && i < 70)
            {
                if (i == 60)
                {
                    temp3.Reverse(); // 순서를 바꿔준다.

                    foreach (Vector3 k in temp3)
                    {
                        spawnPrefabs_.Add(k);
                    }
                }


                spawnPrefabs_.Add(spawnPrefabs[i].transform.position);
            }
            else if (70 <= i && i < 80)
            {
                //List<Vector3> temp4 = new List<Vector3>();
                temp4.Add(spawnPrefabs[i].transform.position);

            }
            else if (80 <= i && i < 90)
            {
                if (i == 80)
                {
                    temp4.Reverse(); // 순서를 바꿔준다.

                    foreach (Vector3 k in temp4)
                    {
                        spawnPrefabs_.Add(k);
                        //Debug.Log("spawnPrefabs_ 7 : " + k);
                    }
                }

                spawnPrefabs_.Add(spawnPrefabs[i].transform.position);
            }
            else if (90 <= i && i < 100)
            {
                //List<Vector3> temp5 = new List<Vector3>();
                temp5.Add(spawnPrefabs[i].transform.position);

            }
            else if (100 == i)
            {
                temp5.Reverse(); // 순서를 바꿔준다.

                foreach (Vector3 k in temp5)
                {
                    spawnPrefabs_.Add(k);
                    //Debug.Log("spawnPrefabs_ 9 : " + k);
                }


                // spawnPrefabs_.Add(spawnPrefabs[i].transform.position);

            }
            else
            {
                //Debug.Log("no more spawn prefabs!");
            }
        }




        // 도로생성
        Vector3[] positions = new Vector3[spawnPrefabs_.Count];

        for (int i = 0; i < spawnPrefabs_.Count; i++)
        {
            positions[i] = spawnPrefabs_[i];
        }

        lr.positionCount = spawnPrefabs_.Count;
        lr.SetPositions(positions);

    }


    // 삭제 후 도로 재생성 문제 해결 해야 함
    public void Reset()
    {
        
        LineRenderer lr = gameObject.GetComponent<LineRenderer>();
        lr.positionCount = 0;

        for(int i = 0; i < spawnPrefabs.Count; i++)
        {
            spawnPrefabs.RemoveAt(i);
        }
        for (int i = 0; i < spawnPrefabs_.Count; i++)
        {
            spawnPrefabs_.RemoveAt(i);
        }
        temp.Clear();
        temp2.Clear();
        temp3.Clear();
        temp4.Clear();
        temp5.Clear();
        temp6.Clear();


        spawnPrefabs.Clear();
        spawnPrefabs_.Clear();

    }


}
