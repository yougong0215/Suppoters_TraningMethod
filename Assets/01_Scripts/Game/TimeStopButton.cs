using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeStopButton : MonoBehaviour
{
    bool btnstop = false;
    [SerializeField] Image img;
    [SerializeField] Sprite s;
    [SerializeField] Sprite p;
    public void TimeStop()
    {
        Time.timeScale = Time.timeScale == 1 ? 0.05f : 1;
        if (btnstop)
        {
            img.sprite = s;
        }
        else
        {
            img.sprite = p;
        }

        btnstop = btnstop == false ? true : false;
    }

    public void SelectStop()
    {
        if(btnstop==false)
        {
            Time.timeScale = 0.05f;
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
