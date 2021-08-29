// 두점을 연결하하고 도로재질로 연결

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class LineRenderer_Hori : MonoBehaviour
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

    // 전체 값 담기
    private List<Vector3> spawnPrefabs_ = new List<Vector3>();

    //라인별 값 별도 저// 전체 값 담기
    public List<Vector3> spawnPrefabs_1, spawnPrefabs_2, spawnPrefabs_3, spawnPrefabs_4, spawnPrefabs_5, spawnPrefabs_6, spawnPrefabs_7, spawnPrefabs_8, spawnPrefabs_9, spawnPrefabs_10 = new List<Vector3>();

    public List<Vector3> temp, temp2, temp4, temp6, temp8, temp10 = new List<Vector3>();


    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.material = new Material(roadMaterial);

        // GioPos 태그를 가진 GameObject를 모두 찾아서 새로운 리스트에 하나씩 담는다.
        foreach (GameObject spawnpreb in GameObject.FindGameObjectsWithTag("GioPos"))
            {
            spawnPrefabs.Add(spawnpreb);
            }


        // spawnPrefabs의 배열을 spawnPrefabs_로의 가로구조로 재정렬한다.

        for (int i = 0; i < spawnPrefabs.Count + 1; i++)
        { 
            if (i == 0 || i == 10 || i == 20 ||i == 30 || i == 40 || i == 50 || i == 60 || i == 70 || i == 80 || i == 90)
            {
                spawnPrefabs_1.Add(spawnPrefabs[i].transform.position); // first horizontal road
            }
            else if (i == 91 || i == 81 || i == 71 || i == 61 || i == 51 || i == 41 || i == 31 || i == 21 || i == 11 || i == 1)
            {
                spawnPrefabs_2.Add(spawnPrefabs[i].transform.position); // 추출한 후 배열값을 reverse 해야함
            }

            else if (i == 2 || i == 12 || i == 22 || i == 32 || i == 42 || i == 52 || i == 62 || i == 72 || i == 82 || i == 92)
            {
                spawnPrefabs_3.Add(spawnPrefabs[i].transform.position);
            }
            else if (i == 93 || i == 83 || i == 73 || i == 63 || i == 53 || i == 43 || i == 33 || i == 23 || i == 13 || i == 3)
            {
                spawnPrefabs_4.Add(spawnPrefabs[i].transform.position); // 추출한 후 배열값을 reverse 해야함
            }
            else if (i == 4 || i == 14 || i == 24 || i == 34 || i == 44 || i == 54 || i == 64 || i == 74 || i == 84 || i == 94)
            {
                spawnPrefabs_5.Add(spawnPrefabs[i].transform.position);
            }
            else if (i == 95 || i == 85 || i == 75 || i == 65 || i == 55 || i == 45 || i == 35 || i == 25 || i == 15 || i == 5)
            {
                spawnPrefabs_6.Add(spawnPrefabs[i].transform.position); // 추출한 후 배열값을 reverse 해야함
            }
            else if (i == 6 || i == 16 || i == 26 || i == 36 || i == 46 || i == 56 || i == 66 || i == 76 || i == 86 || i == 96)
            {
                spawnPrefabs_7.Add(spawnPrefabs[i].transform.position);
            }
            else if (i == 97 || i == 87 || i == 77 || i == 67 || i == 57 || i == 47 || i == 37 || i == 27 || i == 17 || i == 7)
            {
                spawnPrefabs_8.Add(spawnPrefabs[i].transform.position); // 추출한 후 배열값을 reverse 해야함
            }
            else if (i == 8 || i == 18 || i == 28 || i == 38 || i == 48 || i == 58 || i == 68 || i == 78 || i == 88 || i == 98)
            {
                spawnPrefabs_9.Add(spawnPrefabs[i].transform.position);
            }
            else if (i == 99 || i == 89 || i == 79 || i == 69 || i == 59 || i == 49 || i == 39 || i == 29 || i == 19 || i == 9)
            {
                spawnPrefabs_10.Add(spawnPrefabs[i].transform.position); // 추출한 후 배열값을 reverse 해야함
            }
        }

        // 리스트 정렬 실행
        ReverseList2();
        ReverseList4();
        ReverseList6();
        ReverseList8();
        ReverseList10();



        // 도로 생성을 위한 최종 배열 sum
        spawnPrefabs_.AddRange(spawnPrefabs_1);
        spawnPrefabs_.AddRange(temp2);
        spawnPrefabs_.AddRange(spawnPrefabs_3);
        spawnPrefabs_.AddRange(temp4);
        spawnPrefabs_.AddRange(spawnPrefabs_5);
        spawnPrefabs_.AddRange(temp6);
        spawnPrefabs_.AddRange(spawnPrefabs_7);
        spawnPrefabs_.AddRange(temp8);
        spawnPrefabs_.AddRange(spawnPrefabs_9);
        spawnPrefabs_.AddRange(temp10);


        // 도로생성
        Vector3[] positions = new Vector3[spawnPrefabs_.Count];

        for (int i = 0; i < spawnPrefabs_.Count; i++)
        {
            positions[i] = spawnPrefabs_[i];
        }

        lr.positionCount = spawnPrefabs_.Count;
        lr.SetPositions(positions);

    }



    // 배열 reverse
    public void ReverseList2()
    {
        spawnPrefabs_2.Reverse(); // 순서를 바꿔준다.

        foreach (Vector3 j in spawnPrefabs_2)
        {
            temp2.Add(j);
        }
    }

    // 배열 reverse
    public void ReverseList4()
    {
        spawnPrefabs_4.Reverse(); // 순서를 바꿔준다.

        foreach (Vector3 j in spawnPrefabs_4)
        {
            temp4.Add(j);
        }
    }

    // 배열 reverse
    public void ReverseList6()
    {
        spawnPrefabs_6.Reverse(); // 순서를 바꿔준다.

        foreach (Vector3 j in spawnPrefabs_6)
        {
            temp6.Add(j);
        }
    }

    // 배열 reverse
    public void ReverseList8()
    {
        spawnPrefabs_8.Reverse(); // 순서를 바꿔준다.

        foreach (Vector3 j in spawnPrefabs_8)
        {
            temp8.Add(j);
        }
    }

    // 배열 reverse
    public void ReverseList10()
    {
        spawnPrefabs_10.Reverse(); // 순서를 바꿔준다.

        foreach (Vector3 j in spawnPrefabs_10)
        {
            temp10.Add(j);
        }
    }




    private void Update()
    {

    }



}
