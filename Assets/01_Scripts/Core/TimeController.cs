using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : Singleton<TimeController>
{
    float _worldTime = 1;
    float times = 0;
    public float Timer => _worldTime;

    public void SetTime(float x)
    {
        _worldTime = x;
        times = 0;
        Time.timeScale = x;
    }

    private void Update()
    {
        if(_worldTime == 1)
        {
            print(times += Time.deltaTime);
        }
    }


}
