using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSize : MonoBehaviour
{
    [SerializeField]
    // Budilding area 별도 입력 가능
    public float BD_area = 0;
    // Start is called before the first frame update

    //void Start()
    //{
    //    Cal_BD_area();
    //}

    //// Update is called once per frame
    //public void Cal_BD_area()
    //{
    //    Vector3 buildingColliderSize = GetComponent<Collider>().bounds.size;
    //    //Debug.Log("BuildingColliderSize x: " + buildingColliderSize.x);
    //    ////Debug.Log("BuildingSize y: " + buildingColliderSize.y); // only bottom area. except for z axies.
    //    //Debug.Log("BuildingColliderSize z: " + buildingColliderSize.z);


    //    //Vector3 buildingTransformSize = GetComponent<Transform>().localScale;
    //    //Debug.Log("buildingTransformSize x :" + buildingTransformSize.x);
    //    //Debug.Log("buildingTransformSize z :" + buildingTransformSize.z);


    //    //float BD_area_x = buildingColliderSize.x * buildingTransformSize.x;
    //    //float BD_ares_z = buildingColliderSize.z * buildingTransformSize.z;


    //    // 빌딩 면적 계산 공식 -- 하지만 별도로 입력할 것임(실제 빌딩면적 입력을 위해서)

    //    float BD_area_x = buildingColliderSize.x;
    //    float BD_ares_z = buildingColliderSize.z;

    //    BD_area = BD_area_x * BD_ares_z;
    //    //Debug.Log("BD_area : " + BD_area);
    //}
}


