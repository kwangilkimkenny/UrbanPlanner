using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_in_out : MonoBehaviour
{

    public GameObject[] prefabs;

    public GameObject SelectedPrefab;

    private List<GameObject> interSecPos = new List<GameObject>();


    public int maxBuilding = 15;
    public int buildingCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        // 반지름 입력
        float Radious_input = 2f;

        // 4 지점의 중심값 입력
        Vector3 spPosition = new Vector3 (0.5f, 0.0f, 0.5f);


        // 0,0,0   0,0,1  1,0,0  1,0,1


        // GioPos 태그를 가진 GameObject를 모두 찾아서 새로운 리스트에 하나씩 담는다. 이것은 위치값을 추적하기 위함이다.
        foreach (GameObject spawnp in GameObject.FindGameObjectsWithTag("GioPos_test"))
        {
            interSecPos.Add(spawnp);
            //Debug.Log("get GameObject of GioPos_test!");
        }

        int k = 0;
        // 그 위치가 4각형 안에 있는지 체크하기
        Vector3[] interSecpos_ = new Vector3[4];

        interSecpos_[0] = new Vector3(interSecPos[k].transform.position.x, interSecPos[k].transform.position.y, interSecPos[k].transform.position.z);
        //Debug.Log("interSecpos_[0]: " + interSecpos_[0]);

        interSecpos_[1] = new Vector3(interSecPos[k + 1].transform.position.x, interSecPos[k+1].transform.position.y, interSecPos[k+1].transform.position.z);
        //Debug.Log("interSecpos_[1]: " + interSecpos_[1]);

        interSecpos_[2] = new Vector3(interSecPos[k + 2].transform.position.x, interSecPos[k+2].transform.position.y, interSecPos[k+2].transform.position.z);
        //Debug.Log("interSecpos_[2]: " + interSecpos_[2]);

        interSecpos_[3] = new Vector3(interSecPos[k + 3].transform.position.x, interSecPos[k+3].transform.position.y, interSecPos[k+3].transform.position.z);
        //Debug.Log("interSecpos_[3]: " + interSecpos_[3]);

        int SpawnedBD = 0;
        while (SpawnedBD < maxBuilding) // 10개의 빌딩이 10개가 생성될때까지 계속 반복
        {

            if (SpawnedBD == 10) break;

            // 중심점과 4지점의 거리를 비교해서 가장 큰 거리로 원을 만들어 그 안에서 랜덤으로 위치값을 추출
            Vector2 randPos = Random.insideUnitCircle * Radious_input;
            Vector3 rangePos = spPosition + new Vector3(randPos.x, 0, randPos.y);

            if (IsPointInPolygon(rangePos, interSecpos_) == true) // 생성값이 4 지점의 중심에 있다면, 즉 폴리곤 안에 있다면 빌딩 생성  ---------------???? 수정해야 
            {

                GameObject SpawnInstance = Instantiate(SelectedPrefab, rangePos, Quaternion.identity);

                //Debug.Log("BD is spawned in Block");

                // Move new object to the calculated spawn location
                SpawnInstance.transform.position = rangePos;

                SpawnedBD += 1;

            }
            else
            {
                //Debug.Log("BD is not spawned in Block");

                if (SpawnedBD == 10) break;

            }

        }




    }


    public bool IsPointInPolygon(Vector3 p, Vector3[] polygon)
    {
        double minX = polygon[0].x;
        double maxX = polygon[0].x;
        double minZ = polygon[0].z;
        double maxZ = polygon[0].z;
        for (int i = 1; i < polygon.Length; i++)
        {
            Vector3 q = polygon[i];
            minX = System.Math.Min(q.x, minX);
            maxX = System.Math.Max(q.x, maxX);
            minZ = System.Math.Min(q.z, minZ);
            maxZ = System.Math.Max(q.z, maxZ);
        }

        if (p.x < minX || p.x > maxX || p.z < minZ || p.z > maxZ)
        {
            return false;
        }

        bool inside = false;
        for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
        {
            if ((polygon[i].z > p.z) != (polygon[j].z > p.z) &&
                 p.x < (polygon[j].x - polygon[i].x) * (p.z - polygon[i].z) / (polygon[j].z - polygon[i].z) + polygon[i].z)
            {
                inside = !inside;
            }
        }

        return inside;
    }
}
