using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransUIandCamra : MonoBehaviour
{
    public players PL;
    

    private void Start()
    {
        CameraController.Instance.Submit(PL, gameObject.GetComponent<CinemachineVirtualCamera>());
    }

    //public void Click()
    //{
    //    CameraController.Instance.SetCam(PL);
    //    //con.Click(PL);
    //    Debug.Log("Ŭ��");
    //}


}
