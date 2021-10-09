using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_Vector_Cal : MonoBehaviour
{
    //public GameObject p1;
    //public GameObject p2;
    //public GameObject p3;
    //public GameObject p4;

    //public GameObject S1;

    // Start is called before the first frame update
    void Start()
    {
        VectorCal();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VectorCal()
    {
        Vector3 p1 = new Vector3(0f, 0f, 0f);
        Vector3 p1_ = new Vector3(-10f, 0f, 0.5f);

        Vector3 p2 = new Vector3(0f, 0f, 1f);
        Vector3 p2_ = new Vector3(0f, 0f, 10f);

        Vector3 p3 = new Vector3(1f, 0f, 0f);
        Vector3 p3_ = new Vector3(1.3f, 0f, 0f);

        Vector3 p4 = new Vector3(1f, 0f, 1f);
        Vector3 p4_ = new Vector3(1.5f, 0f, 10f);

        // 네 지점의 변화로 방향벡터

        Vector3 newP = p1_ - p1;
        //Debug.Log("newP :" + newP);
        Vector3 newP2 = p2_ - p2;
        Vector3 newP3 = p3_ - p3;
        Vector3 newP4 = p4_ - p4;

        // 변화의 크기(길이)계산하기
        float DistP1 = Vector3.Distance(p1_, p1);
        //Debug.Log("DistP1 :" + DistP1);
        float DistP2 = Vector3.Distance(p2_, p2);
        float DistP3 = Vector3.Distance(p3_, p3);
        float DistP4 = Vector3.Distance(p4_, p4);

        // 기존의 위치
        Vector3 S1 = new Vector3(0f, 0f, 0f);
        //Debug.Log("기존의 위치: " + S1);


        // 도착위치 = 현재위치 + 방향벡터 * 이동할거리
        Vector3 F_S1 = (S1 + newP * DistP1) * 0.01f;
        Vector3 F_S2 = (S1 + newP2 * DistP2) * 0.01f;
        Vector3 F_S3 = (S1 + newP3 * DistP3) * 0.01f;
        Vector3 F_S4 = (S1 + newP4 * DistP4) * 0.01f;

        Vector3 Final_S = F_S1 + F_S2 + F_S3 + F_S4;

        //Debug.Log("도착위치 = 현재위치 + 방향벡터 * 이동할거리 :" + Final_S);


        //// 새로운 이동 위치
        //Vector3 S2 = (newP4 - (newP3 -(newP2 - (newP - S1))));
        //Debug.Log("새로운 이동위치 :" + S2);

        //Vector3 S3 = (newP + S1) * 0.1f;
        //Vector3 S4 = (newP2 + S1) * 0.1f;
        //Vector3 S5 = (newP3 + S1) * 0.1f;
        //Vector3 S6 = (newP4 + S1) * 0.1f;

        //Vector3 S7 = S3 + S4 + S5 + S6;
        //Debug.Log("새로운 이동위치 2 :" + S7);
        // 10% 적용전 --> 새로운 이동위치 2 :(-9.2, 0.0, 18.5)
        // 10% 적용 후 -> 새로운 이동위치 2 :(-0.9, 0.0, 1.9)


        // 변화거리를 무시한 값 적용 -------- " 채택 ! "
        //Vector3 S8 = (p1_ + S1) * 0.1f;
        //Vector3 S9 = (p2_ + S1) * 0.1f;
        //Vector3 S10 = (p3_ + S1) * 0.1f;
        //Vector3 S11 = (p4_ + S1) * 0.1f;

        //Vector3 S12 = S8 + S9 + S10 + S11;
        //Debug.Log("새로운 이동위치 3 :" + S12);

        // 새로운 위치
        // 네 지점 벡터의 합 - 기존의 위치
        //Vector3 sumP = p1 + p2 + p3 + p4;
        //Debug.Log("네지점 벡터의 합: " + sumP);

        //float newDistS1 = Vector3.Distance(sumP,S1);
        //Debug.Log("이동해야할 거리?: " + newDistS1);

        //Vector3 newS1 = sumP - S1;
        //Debug.Log("벡터간 빼기:" + newS1);
    }
}
