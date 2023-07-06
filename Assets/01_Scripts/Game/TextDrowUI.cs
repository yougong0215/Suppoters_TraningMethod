using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextDrowUI : PoolAble
{
    [SerializeField] TextMeshProUGUI tmp;
    Transform ps;
    float LifeTime = 3;
    float curtime;
    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        ps = GameObject.Find("CharUIS").transform;
    }
    public void Init(string text, float LifeTIme = 3)
    {
        LifeTime = LifeTIme;
        curtime = LifeTime;
        tmp.text = text;
        transform.position = Input.mousePosition;
        transform.parent=ps;
    }

    private void Update()
    {
        tmp.transform.position += new Vector3(0, 30, 0) * Time.deltaTime;
        curtime -= Time.deltaTime;
        tmp.color = new Color(1, 0.1f, 0.1f, curtime/ LifeTime);
        if(curtime <= 0)
        {
            PoolManager.Instance.Push(this);
        }
    }
}
