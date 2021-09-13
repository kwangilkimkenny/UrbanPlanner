using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


// block infor 깃발에 적용하는 코드

public class UI_Get_Info_Block : MonoBehaviour
{

    public SpawnBuilding Spawn;

    private int findIndex_int;
    public Text findIndexValue;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {

                // Debug.Log(hit.transform.name);

                //Select GameObject - Block indicator 
                if (hit.transform.gameObject.tag == "BlockIndicator")
                {
                    // Block의 id번호, 면적, 스폰된 빌딩 수, 빌딩 종류 추출하여 UI에 표시
                    Debug.Log("Get info of Block");

                    foreach (GameObject indi in Spawn.BlockIndicator_List)
                    {
                        Debug.Log("indi :" + indi);

                        // 클릭하여 레이케스트로 출돌시켜 선택한 게임오브젝트의 위치값과 생성한 인디케이터 배열의 개별 값을 각각 비교하여 같은것이 있다면, 해당 위치 값을 가져오면 됨
                        if (hit.transform.gameObject.transform.position == indi.transform.position)
                        {
                            findIndex_int = Spawn.BlockIndicator_List.FindIndex(x => x == indi);
                            findIndexValue.text= "Block ID :" + findIndex_int.ToString();
                        }

                    }
                   

                }
            }
        }
    }
}
