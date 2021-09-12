using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DistanceToogleBtn : MonoBehaviour
{

    public GameObject toggler;

    // Start is called before the first frame update
    void Start()
    {
        print(toggler.GetComponent<Toggle>().isOn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DistanceTogBtn(bool tog)
    {
        print(tog);
    }
}
