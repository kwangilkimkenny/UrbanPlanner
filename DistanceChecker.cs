using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceChecker : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;


    private LineRenderer lineRend;
    private Vector3 mousePos;
    private Vector3 StartMousePos;


    [SerializeField]
    private Text distanceText;

    private float distance;


    // Start is called before the first frame update
    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask);

            StartMousePos = raycastHit.point;


        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask);

            mousePos = raycastHit.point;

            lineRend.SetPosition(0, new Vector3(StartMousePos.x, StartMousePos.y, StartMousePos.z));
            lineRend.SetPosition(1, new Vector3(mousePos.x, mousePos.y, mousePos.z));
            distance = (mousePos - StartMousePos).magnitude;
            distanceText.text = distance.ToString("F2") + " km"; // 소수점 두자리까지 계산 F2

        }

    }


}
