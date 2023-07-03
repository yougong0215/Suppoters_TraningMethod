using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopButton : MonoBehaviour
{
    public void TimeStop()
    {
        Time.timeScale = Time.timeScale == 1 ? 0.1f : 1;
    }
}
