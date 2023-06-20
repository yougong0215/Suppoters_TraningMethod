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
        else
        {
            tls = GameObject.Find("Boss").transform;
        }
    }

    private void Update()
    {
        transform.position = tls.position;
        transform.rotation = Quaternion.identity;
    }
}
