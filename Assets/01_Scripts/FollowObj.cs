using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObj : MonoBehaviour
{
    public players pl;
    public Transform tls;

    
    private void Awake()
    {
        tls = GameObject.Find(pl.ToString()).transform;    
    }

    private void Update()
    {
        transform.position = tls.position;
    }
}
