using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forwardCube : MonoBehaviour
{
    [SerializeField]float speed = 50;
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }
}
