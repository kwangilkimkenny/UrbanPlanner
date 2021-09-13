using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




// Distance on/off toggle button
public class UI_DistanceToogleBtn : MonoBehaviour
{
    [SerializeField]
    private Text distanceText;


    GameObject Distance;

    private void Awake()
    {
        Toggle_Changed(false);
    }

    public void Toggle_Changed(bool isOn)
    {
        

        if (isOn) // 켜지면, 
        {
            //Debug.Log("Distance togllel is on");

            GameObject.Find("DistanceCheck").transform.Find("DistanceChecker").gameObject.SetActive(true);
            
        }else // 꺼지거나 꺼져있다면,
        {
            Distance = GameObject.Find("DistanceChecker");
            //Debug.Log("Distance togllel is off");

            Distance.SetActive(false);

            // 꺼지면 기본 거리측정 메시지로 변환
            distanceText.text = "Distance";
        }

    }
}
