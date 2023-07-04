using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopButton : MonoBehaviour
{
    bool btnstop = false;
    public void TimeStop()
    {
        Time.timeScale = Time.timeScale == 1 ? 0.01f : 1;
        btnstop = btnstop == false ? true : false;
    }

    public void SelectStop()
    {
        if(btnstop==false)
        {
            Time.timeScale = 0.01f;
        }
    }

    public void SelectEnd()
    {
        if (btnstop == false)
        {
            Time.timeScale = 1;
        }
    }
}
