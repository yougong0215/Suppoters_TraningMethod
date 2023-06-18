using System.Collections;
using System.Collections.Generic;
using UniJSON;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.VFX;

public class GetBuff : PoolAble
{
    
    public LayerMask _player;
    public float LifeTime = 0;
    public float BufTime = 5;
    float curtime = 0;

    bool b;
    public float value;
    public Stat st;
    
    [Header("VFX")]
    public VisualEffect vfx;
    public ParticleSystem ps;
    public GetTypeShape shape;
    VisualEffect Veffect;
    ParticleSystem Peffect;
    public bool SelfBuf;

    public players pl;

    protected bool init = false;
    [SerializeField] List<Collider> cols = new List<Collider>();
    [SerializeField] public List<Coroutine> corutime = new List<Coroutine>();

    public void OnEnable()
    {
        transform.localPosition = Vector3.zero;
        LifeTime = transform.parent.GetComponent<DamageCaster>().onLife;
        if (vfx != null)
        {
            GameObject obj = Instantiate(vfx.gameObject, transform);


            Veffect = obj.gameObject.GetComponent<VisualEffect>();
        }

        if (ps != null)
        {
            GameObject obj = Instantiate(ps.gameObject, transform);

            Peffect = obj.gameObject.GetComponent<ParticleSystem>();
        }
        init = false;
        if(pl != players.None)
        {
            StartCoroutine(GameObject.Find(pl.ToString()).GetComponent<AgentStatus>().Buffs(value, st, BufTime));
        }
        Debug.Log("Èú½ÃÄö½º±âµ¿");
    }

    private void Update()
    {
        if(Time.timeScale== 0)
        {
            b = true;
            if (Peffect)
            {
                Peffect.Pause();
            }
            if(Veffect)
            {
                Veffect.pause =true ;
            }
        }
        else
        {
            if(b==true)
            {
                b = false; 
                if (Peffect)
                {
                    Peffect.Play();
                }
                if (Veffect)
                {
                    Veffect.pause = false;
                }
            }
        }

    }

    private void FixedUpdate()
    {
        curtime += Time.deltaTime;
        if(curtime > LifeTime)
        {
            if (Veffect)
                Destroy(Veffect);
            if (Peffect)
                Destroy(Peffect);
            //PoolManager.Instance.Push(this);
        }
        Collider[] colliders = null;
        


        colliders = Physics.OverlapSphere(transform.position, 2, _player);
        if (colliders.Length > 0)
        {

            foreach (Collider collider in colliders)
            {
                if(collider.GetComponent<AgentStatus>())
                {
                    AgentStatus at = collider.GetComponent<AgentStatus>();
                    for (int i = 0; i < corutime.Count; i++)
                    {
                        if (collider == cols[i])
                        {
                            StopCoroutine(corutime[i]);
                            at.AddDamage = 0;
                            at.Cirt = 0;
                            at.CirtDAM = 0;
                            at.DEF = 0;
                            corutime.RemoveAt(i);
                            cols.RemoveAt(i--);
                        }
                    }
                    cols.Add(collider);
                    corutime.Add(StartCoroutine(at.Buffs(value, st, LifeTime)));

                }



            }
        }
    }
   


}
