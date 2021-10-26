using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyTerrain : MonoBehaviour
{
    public Terrain terrain;
    public float strength = 0.01f;
    private int heightmapWidth;
    private int heightmapHeight;
    private float[,] heights;
    private TerrainData terrainData;

    // Start is called before the first frame update
    void Start()
    {
        terrainData = terrain.terrainData;
        heightmapWidth = terrainData.heightmapResolution;
        heightmapHeight = terrainData.heightmapResolution;
        heights = terrainData.GetHeights(0,0, heightmapWidth, heightmapHeight);

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Raise Terrain
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                RaiseTerrain(hit.point);
            }
        }

        // Lower Terrain
        if (Input.GetMouseButton(1))
        {
            if (Physics.Raycast(ray, out hit))
            {
                LowerTerrain(hit.point);
            }
        }
    }

    private void RaiseTerrain(Vector3 point) {
        {
            int mouseX = (int)((point.x / terrainData.size.x) * heightmapWidth);
            int mouseZ = (int)((point.z / terrainData.size.z) * heightmapHeight);

            float[,] modifiedHeights = new float[1,1];
            float y = heights[mouseX, mouseZ];
            y += strength * Time.deltaTime;

            if (y > terrainData.size.y)
            {
                y = terrainData.size.y;
            }

            modifiedHeights[0,0] = y;
            heights[mouseX, mouseZ] = y;
            terrainData.SetHeights(mouseX, mouseZ, modifiedHeights);
        }
    }

    private void LowerTerrain(Vector3 point) {
    {
        int mouseX = (int)((point.x / terrainData.size.x) * heightmapWidth);
        int mouseZ = (int)((point.z / terrainData.size.z) * heightmapHeight);

        float[,] modifiedHeights = new float[1,1];
        float y = heights[mouseX, mouseZ];
        y -= strength * Time.deltaTime;

        if (y < 0)
        {
            y = 0;
        }

        modifiedHeights[0,0] = y;
        heights[mouseX, mouseZ] = y;
        terrainData.SetHeights(mouseX, mouseZ, modifiedHeights);
    }
    }
}
