using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ResetBuilding : MonoBehaviour
{


    public void ResetAllBuilding()
    {
        foreach (GameObject buildings in GameObject.FindGameObjectsWithTag("building"))
        {
            buildings.SetActive(false);
            
        }

        Debug.Log("Buildings DeActivated");

    }

}
