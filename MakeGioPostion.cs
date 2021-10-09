using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Button에 적용하는 코드


// 혹은, 도로생성 버튼 클릭, 클릭포인트 > 클릭포인트 자동으로 도로 생성
// 생성한 도로 삭제하기 : 리스트에 담긴 클릭포인트 Destroy.



public class MakeGioPostion : MonoBehaviour
{

    GameObject AddGioPosFunc;

    private void Awake()
    {
        Get_mouse_input(false);
        GameObject.Find("AddGioPnt").transform.Find("AddGioPntObj").gameObject.SetActive(false);
    }


    public void Get_mouse_input(bool isOn)
    {

        
        if (isOn) // on
        {
            Debug.Log("Turn On. Get_mouse_input");
            GameObject.Find("AddGioPnt").transform.Find("AddGioPntObj").gameObject.SetActive(true);

        }else
        {
            Debug.Log("Turn Off. get new GioPosition.");
            //AddGioPosFunc = GameObject.Find("AddGioPntObj");
            //AddGioPosFunc.SetActive(false);

            GameObject.Find("AddGioPnt").transform.Find("AddGioPntObj").gameObject.SetActive(false);

            foreach (GameObject gitm_ in GameObject.FindGameObjectsWithTag("GioPos_"))
            {
                Destroy(gitm_);
            }
        }

    }


}
