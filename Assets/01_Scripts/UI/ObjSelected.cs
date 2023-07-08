using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjSelected : MonoBehaviour
{
    private Camera mainCamera;
    UIIterrective it;
    private void Start()
    {
        // 메인 카메라 참조
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // 마우스 왼쪽 버튼 클릭 시
        if (Input.GetMouseButtonDown(1))
        {
            // 클릭 위치의 Ray 생성
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Raycast 실행하여 충돌 검사
            if (Physics.Raycast(ray, out hit))
            {
                // 충돌한 객체의 Collider 검사
                SphereCollider sphereCollider = hit.collider.GetComponent<SphereCollider>();

                if (sphereCollider != null)
                {
                    if( sphereCollider.GetComponent<UIIterrective>())
                    {
                        if(it != null)
                        {
                            it.Hid();
                        }
                        it = sphereCollider.GetComponent<UIIterrective>();
                        it.Inter();
                    }
                }
            }
        }
    }
}
