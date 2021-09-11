using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BDCounter : MonoBehaviour
{

    Text BuildingCounter;

    public void getBuildingCnt()
    {
        BuildingCounter = GameObject.Find("Buildings").GetComponent<Text>();
    }
}
