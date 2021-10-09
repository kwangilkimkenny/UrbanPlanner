using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeGioPnt_Add : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;

    private Vector3 mousePos;
    //private bool OnOff = false;


    // 생성할 GioPos 종류 선택기능 추가해야 함
    public GameObject BlockPosPrep;
    public GameObject SubBlockPosPrep;
    private Transform BlockPosParent;
    private Transform SubBlockPosParent;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click! to instantiate.");
            Function_Instantiate();
        }
        if (Input.GetMouseButtonDown(1))
        {
            Function_Destroy();
        }
    }



    private void Function_Destroy()
    {
        foreach (GameObject gitm_ in GameObject.FindGameObjectsWithTag("GioPos_"))
        {
            Destroy(gitm_);
        }
    }


    private void Function_Instantiate()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask);

        mousePos = raycastHit.point;
        Debug.Log("instantiate here : " + mousePos);

        Vector3 mousepos_ = new Vector3(mousePos.x, mousePos.y, mousePos.z);

        GameObject inst = Instantiate(BlockPosPrep, mousepos_, Quaternion.identity);
        inst.transform.position = mousepos_;
    }
}
