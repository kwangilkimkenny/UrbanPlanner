using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridGenerator_type2 : MonoBehaviour
{
    public GameObject line;
    private float timeOffset;
    public int lineNum = 10;
    public float lineLength = 100.0f;
    public float speed = 1.0f;
    //public float offset = 2.0f;
    private LineRenderer[] lines;
    private LineRenderer renderLine;
    private Vector3 pos;

    void Start()
    {
        lines = new LineRenderer[lineNum];
        pos = this.transform.position;

        horizonLines();
        verticalLines();
    }

    private void Update()
    {
        lineMoveToUP();
    }

    //first line genarte
    public void instLine()
    {
        var Obj = Instantiate(line, pos, Quaternion.identity);
        Obj.transform.SetParent(transform);
        renderLine = Obj.GetComponent<LineRenderer>();
        renderLine.useWorldSpace = false;
    }
    //vertical lines genarte
    private void verticalLines()
    {
        for (int i = 0; i < lineNum; i++)
        {
            instLine();
            for (int j = 0; j < 2; j++)
            {
                renderLine.SetPosition(j, new Vector3(pos.x + (i * (lineLength / lineNum)),
                                                      pos.y,
                                                      pos.z + (j * lineLength)));
            }
        }
    }
    //horizontal lines genarate
    private void horizonLines()
    {
        for (int i = 0; i < lineNum; i++)
        {
            instLine();
            for (int j = 0; j < 2; j++)
            {
                renderLine.SetPosition(j, new Vector3(pos.x + (j * lineLength),
                                                      pos.y,
                                                      pos.z + (i * (lineLength / lineNum))));
            }
            lines[i] = renderLine;
        }
    }
    //line move to up
    private void lineMoveToUP()
    {
        timeOffset += Time.deltaTime * speed;
        for (int i = 0; i < lineNum; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                lines[i].SetPosition(j, new Vector3(pos.x + (j * lineLength),
                                                    pos.y,
                                                    pos.z + ((i * (lineLength / lineNum) + timeOffset) % lineLength)));
            }
        }
    }
}
