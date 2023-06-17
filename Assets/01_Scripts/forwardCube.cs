using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forwardCube : MonoBehaviour
{
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 50;
    }
}
