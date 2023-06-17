using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class SeeCamera : MonoBehaviour
{
    private void Update()
    {
        transform.localEulerAngles = new Vector3(GameManager.Instance.Cam.transform.localEulerAngles.x, GameManager.Instance.Cam.transform.localEulerAngles.y, 0);
    
        
    }
}
