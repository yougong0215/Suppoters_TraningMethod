using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransUIandCamra : MonoBehaviour
{
    public players PL;
    public CinemachineVirtualCamera cam;
    public CharUIController con;
    

    private void Start()
    {
        CameraController.Instance.Submit(PL, cam);
    }

    public void Click()
    {
        CameraController.Instance.SetCam(PL);
        //con.Click(PL);
        Debug.Log("Å¬¸¯");
    }


}
