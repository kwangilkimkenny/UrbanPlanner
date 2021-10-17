using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastItemAligner : MonoBehaviour
{

    public float raycastDis = 100f;
    //public GameObject GioPrefab;
    public Vector3 itmPos;

    public object PositionRaycast(GameObject obj_)
    {
        RaycastHit hit;

        Physics.Raycast(obj_.transform.position, Vector3.down, out hit, raycastDis);

        itmPos = hit.point;

        //Debug.Log("itmPos : " + itmPos);

        return itmPos;

    }


}
