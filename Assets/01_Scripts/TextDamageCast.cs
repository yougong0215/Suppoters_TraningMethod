using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextDamageCast : PoolAble
{
    TextMeshPro tmp;
    
    public void Init(int Damage, bool crit, Vector3 position)
    {
        Destroy(this.gameObject, 2f);
        //transform.LookAt(GameManager.Instance.Cam.transform);
        if (tmp == null)
        {
            tmp = GetComponent<TextMeshPro>();
        }
        transform.position = position;
        //transform.position += 
        tmp.text = $"{Damage}";
        if(crit == true)
        {
            tmp.fontSize = 8;
            tmp.color = Color.yellow;
        }
        else
        {
            tmp.fontSize = 5;
            tmp.color = Color.white;
        }
    }
    private void Update()
    {
        transform.localEulerAngles = new Vector3(GameManager.Instance.Cam.transform.localEulerAngles.x, GameManager.Instance.Cam.transform.localEulerAngles.y, 0);
        tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, tmp.color.a - Time.deltaTime);
    }
}
