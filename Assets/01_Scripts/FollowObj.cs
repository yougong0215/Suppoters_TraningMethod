using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObj : MonoBehaviour
{
    public players pl;
    public Transform tls;

    
    private void Awake()
    {
        if(pl != players.None)
            tls = GameObject.Find(pl.ToString()).transform;

        //transform.localEulerAngles = new Vector3(0, 45, 0);

    }

    private void Update()
    {
        if(tls != null)
        {

            transform.position = tls.position;
        }
        else
        {
            transform.position = CameraController.Instance.NoneCameraPos;
        }
        transform.rotation = Quaternion.identity;
    }
}
