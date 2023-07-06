using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPosCheckLine : PoolAble
{
    [SerializeField] SpriteRenderer sp;

    float LifeTIme = 0;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    public void Init(Vector3 pos)
    {
        transform.position = pos + new Vector3(0, 0.1f, 0);
        LifeTIme = 0.6f;
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    private void Update()
    {
        LifeTIme -= Time.deltaTime;
        sp.color = new Color(1, 1, 1, Mathf.Sin(LifeTIme));

        transform.localScale *= 0.9f;

        if(LifeTIme <= -0.2f)
        {
            PoolManager.Instance.Push(this);
        }
    }

}
