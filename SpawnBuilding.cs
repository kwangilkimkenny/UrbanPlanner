using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuilding : MonoBehaviour

{

    public GameObject[] spawnPrefab;

    private Transform lineStart, lineEnd;

    // public Transform lineStart;
    public bool spawning = false;
    public float spawnFrequency = 3f;
    public float spawnTimer = 2f;

    // Gizmos
    public Color gizmoColor = Color.green;
    public bool meshPreview, showIcon;
    public float iconFloatingHeight = 0.5f;


    // 불러올 게임오브젝트 리스트 선언
    private List<GameObject> spawnPrefabs = new List<GameObject>();

    // 생성한 빌딩의 게임오브젝트를 추적하시 위한 리스트 선언
    public List<GameObject> buildingPrebs = new List<GameObject>();

    int maxBuilding = 500;
    int buildingCount = 0;

    // building이 스폰되어 있는가? 기본값은 안되어있다. 
    public bool isSpawn = false;


    public void StartSpawn()
    {
        //Debug.Log("Start spawn building");
        if (!isSpawn) // 즉, 스폰되어 있지 않았다면 스폰한다.
        {
            Debug.Log("Start spawn building");
            for (int j = 0; j <= maxBuilding; j++)
            {
                spawnTimer += Time.deltaTime;

                if (spawnTimer <= spawnFrequency)
                {
                    Spawn();
                    spawnTimer = 0f; // Reset
                }
            }
            // 스폰이 되면 true로 하고, else에서 ReSpawn을 실행하면 생성한 건물을 껐다 켰다 할 수 있음(재활용가능)
            isSpawn = true;
        }
        else // true 라면, 즉, 스폰되었다면 
        {
            ReSpawn();
        }

    }


    private void ReSpawn()
    {
        
        
        Debug.Log("Building Activated");

        for (int i = 0; i < buildingPrebs.Count; i++)
        {


            buildingPrebs[i].transform.position = SpawnPosition();
            // 꺼져있는 빌딩을 켜줌
            buildingPrebs[i].SetActive(true);
        }

        Debug.Log("Building Activated all!");


    }


    private void Spawn()
    {

        int selection = Random.Range(0, spawnPrefab.Length);

        GameObject SelectedPrefab = spawnPrefab[selection];

        if (buildingCount <= maxBuilding)
        {
            Vector3 spawnPos = SpawnPosition();

            GameObject SpawnInstance = Instantiate(SelectedPrefab, spawnPos, Quaternion.identity);


            // Move new object to the calculated spawn location
            SpawnInstance.transform.position = spawnPos;


            // 생성한 빌딩을 리스트에 등록해서 추척관리할 거임
            buildingPrebs.Add(SpawnInstance);

            buildingCount += 1;
        }

    }

    private Vector3 SpawnPosition()
    {

        // GioPos 태그를 가진 GameObject를 모두 찾아서 새로운 리스트에 하나씩 담는다. 이것은 위치값을 추적하기 위함이다.
        foreach (GameObject spawnp in GameObject.FindGameObjectsWithTag("GioPos"))
        {
            spawnPrefabs.Add(spawnp);
        }

        lineStart = spawnPrefabs[0].transform;
        lineEnd = spawnPrefabs[99].transform;


        // Get Value Ranges along the line (x, y, z)
        float xRange = lineEnd.position.x - lineStart.position.x;
        float yRange = lineEnd.position.y - lineStart.position.y;
        float zRange = lineEnd.position.z - lineStart.position.z;

        Vector3 spawnLocation = new Vector3(lineStart.position.x + (xRange * UnityEngine.Random.value),
                                            lineStart.position.y + (yRange * UnityEngine.Random.value),
                                            lineStart.position.z + (zRange * UnityEngine.Random.value));
        return spawnLocation;
    }


}
