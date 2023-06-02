using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrnszitonCamra : MonoBehaviour
{
    public players PL;
    public CinemachineVirtualCamera cam;

    private void Start()
    {
        CameraController.Instance.Submit(PL, cam);
    }

    public void Click()
    {
        CameraController.Instance.SetCam(PL);
        Debug.Log("Å¬¸¯");
    }
}
