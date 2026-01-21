using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Billboard : MonoBehaviour
{
    [Header("따라갈 대상")]
    public Transform target;

    [Header("머리 위 오프셋")]
    public Vector3 offset = new Vector3(0, 0.5f, 0);

    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        // if (target == null) return;

        // transform.position = target.position + offset;

        // // ī�޶� ������ ���ϵ� Y�ุ ���
        // Vector3 targetPos = transform.position + cam.forward;
        // targetPos.y = transform.position.y; // ����(Y��) ����

        transform.LookAt(target);
    }
}



//public class Billboard : MonoBehaviour
//{
//    Transform myTr;
//    Transform mainCameraTr;

//    // Use this for initialization
//    void Start()
//    {

//        myTr = GetComponent<Transform>();

//        mainCameraTr = Camera.main.transform;

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        myTr.LookAt(mainCameraTr);
//    }
//}
