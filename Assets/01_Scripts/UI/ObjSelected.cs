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
        // ���� ī�޶� ����
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // ���콺 ���� ��ư Ŭ�� ��
        if (Input.GetMouseButtonDown(1))
        {
            // Ŭ�� ��ġ�� Ray ����
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Raycast �����Ͽ� �浹 �˻�
            if (Physics.Raycast(ray, out hit))
            {
                // �浹�� ��ü�� Collider �˻�
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
