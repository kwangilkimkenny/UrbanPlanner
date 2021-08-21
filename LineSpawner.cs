using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSpawner : MonoBehaviour
{

    public GameObject spawnPrefab;
    public Transform lineStart, lineEnd;
    // public Transform lineStart;
    public bool spawning = false;
    public float spawnFrequency = 3f;
    public float spawnTimer = 2f;

    // Gizmos
    public Color gizmoColor = Color.green;
    public bool meshPreview, showIcon;
    public float iconFloatingHeight = 0.5f;



    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer <= spawnFrequency)
        {
            Spawn();
            spawnTimer = 0f; // Reset
        }
    }

    private void Spawn()
    {
        // Get Value Ranges along the line (x, y, z)
        float xRange = lineEnd.position.x - lineStart.position.x;
        float yRange = lineEnd.position.y - lineStart.position.y;
        float zRange = lineEnd.position.z - lineStart.position.z;

        Vector3 spawnLocation = new Vector3(lineStart.position.x + (xRange * UnityEngine.Random.value),
                                            lineStart.position.y + (yRange * UnityEngine.Random.value),
                                            lineStart.position.z + (zRange * UnityEngine.Random.value));
        GameObject SpawnInstance = Instantiate(spawnPrefab);

        // Move new object to the calculated spawn location
        SpawnInstance.transform.position = spawnLocation;
                                            
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawLine(lineStart.position, lineEnd.position);

        if (meshPreview)
        {
            if(spawnPrefab == null)
            {
                Gizmos.DrawCube(lineStart.position, Vector3.one);
                Gizmos.DrawCube(lineEnd.position, Vector3.one);
            } else
            {
                MeshFilter modelMeshFilter = spawnPrefab.GetComponentInChildren<MeshFilter>();
                Mesh modelMesh = modelMeshFilter.sharedMesh;
                Vector3 modelBounds = modelMesh.bounds.size;

                Gizmos.DrawMesh(modelMesh, 0, lineStart.position, modelMeshFilter.transform.rotation, Vector3.one / modelBounds.x);
                Gizmos.DrawMesh(modelMesh, 0, lineEnd.position, modelMeshFilter.transform.rotation, Vector3.one / modelBounds.x);

            }
        }
    }
}
