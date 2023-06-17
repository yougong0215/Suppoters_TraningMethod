using System.Collections;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;
using UnityEngine.VFX;

public class GetBuff : PoolAble
{
    
    public LayerMask _player;
    public float LifeTime = 3;
    float curtime = 0;

    public float bufTime = 5;

    public float value;
    public Stat st;
    
    [Header("VFX")]
    public VisualEffect vfx;
    public ParticleSystem ps;
    public GetTypeShape shape;
    GameObject effect;


    protected bool init = false;
    public void Init()
    {
        if (vfx != null)
        {
            GameObject obj = Instantiate(vfx.gameObject, transform);


            effect = obj.gameObject;
        }

        if (ps != null)
        {
            GameObject obj = Instantiate(ps.gameObject, transform);

            effect = obj.gameObject;
        }
        init = false;
        

    }
    private void Update()
    {
        if(curtime > LifeTime)
        {
            PoolManager.Instance.Push(this);
        }
    }


}
