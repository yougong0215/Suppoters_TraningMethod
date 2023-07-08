using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIterrective : MonoBehaviour
{
    public UISommon uis;


    public void Inter()
    {
        uis.Show();
    }

    public void Hid()
    {
        uis.FastHid();
    }
}
