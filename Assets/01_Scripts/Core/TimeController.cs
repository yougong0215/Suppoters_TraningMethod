using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : Singleton<TimeController>
{
    float _worldTime = 1;
    public float Timer => _worldTime;

    public void SetTime(float x)
    {
        _worldTime = x;
    }
}
