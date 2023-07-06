using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextDamageCast : PoolAble
{
    TextMeshPro tmp;

    private void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
    }
    public void Init(int Damage, bool crit, Vector3 position)
    {
        transform.position = position;
        //transform.position += 
        tmp.text = $"{Damage}";
        if (crit == true)
        {
            tmp.fontSize = 8;
            tmp.color = Color.yellow;
        }
        else
        {
            tmp.fontSize = 5;
            tmp.color = Color.white;
        }
        StartCoroutine(die());
    }

    IEnumerator die()
    {
        yield return new WaitForSecondsRealtime(2f);
        PoolManager.Instance.Push(this);
    }
    private void Update()
    {
        transform.localEulerAngles = new Vector3(GameManager.Instance.Cam.transform.localEulerAngles.x, GameManager.Instance.Cam.transform.localEulerAngles.y, 0);
        tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, tmp.color.a - Time.deltaTime);
    }
}
