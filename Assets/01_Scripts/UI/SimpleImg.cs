using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimpleImg : MonoBehaviour
{
    public TextMeshPro tmp;

    public void Seting(Sprite spi , int t)
    {
        if(spi != null)
        {
            GetComponent<SpriteRenderer>().sprite = spi;
        }
        tmp.text = t.ToString();
    }
}
