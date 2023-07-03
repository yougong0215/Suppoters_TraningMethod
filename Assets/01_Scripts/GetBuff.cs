using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniJSON;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.VFX;

public enum GetTypeShape
{
    Box,
    Sphere,
    Polygon
}

public class GetBuff : PoolAble
{
    [SerializeField] Dictionary<players, bool> pled = new();
    public LayerMask _player;
    public float LifeTime = 0;
    public float BufTime = 5;
    float curtime = 0;

    bool b;
    public float PlusValue;
    public float himsValue;

    public Stat st;
    
    [Header("VFX")]
    public VisualEffect vfx;
    public ParticleSystem ps;
    public GetTypeShape shape;
    VisualEffect Veffect;
    ParticleSystem Peffect;
    public bool oneBuf;
    bool use  =false;

    public players pl;

    protected bool init = false;
    [SerializeField] List<Collider> cols = new List<Collider>();
    [SerializeField] public List<Coroutine> corutime = new List<Coroutine>();

    public void OnEnable()
    {
        transform.localPosition = Vector3.zero;
        //LifeTime = transform.parent.GetComponent<DamageCaster>().onLife;
        if (vfx != null)
        {
            GameObject obj = Instantiate(vfx.gameObject, transform);


            Veffect = obj.gameObject.GetComponent<VisualEffect>();
        }
        pled.Clear();

        if (ps != null)
        {
            GameObject obj = Instantiate(ps.gameObject, transform);

            Peffect = obj.gameObject.GetComponent<ParticleSystem>();
        }
        if(pl != players.None)
        {
            if (init == true)
            {

                StartCoroutine(GameObject.Find(pl.ToString()).GetComponent<AgentStatus>().Buffs(PlusValue, himsValue, st, BufTime));
                Debug.Log("ㅇㅁㄹㅇㅇㅇ");
            }
            else
            {
                Debug.Log("ㅇㅁㄹㅇ");
            }
        }
        init = true;
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

    public void OnEnd()
    {
        if (Veffect)
            Destroy(Veffect);
        if (Peffect)
            Destroy(Peffect);
    }


    private void FixedUpdate()
    {
        curtime += Time.deltaTime;
        Collider[] colliders = null;
        
        colliders = Physics.OverlapSphere(transform.position, GetColliderRadius(), _player);
        if (use == true)
            return;
        if (oneBuf == true && use == false)
        {
            use = true;

            if (colliders.Length > 0)
            {


                foreach (Collider collider in colliders)
                {
                    if (collider.GetComponent<AgentStatus>())
                    {
                        AgentStatus at = collider.GetComponent<AgentStatus>();
                        Debug.Log(gameObject.name);

                        if (!pled.ContainsKey(at.pl))
                        {
                            pled.Add(at.pl, true);
                            StartCoroutine(at.Buffs(PlusValue, himsValue, st, BufTime));
                            Debug.Log("작동22");
                        }
                        else
                        {
                            Debug.Log("작동");
                        }

                    }



                }
            }
            return;
        }

        if(curtime > LifeTime)
        {
            return;
            //PoolManager.Instance.Push(this);
        }
        if (colliders.Length > 0)
        {

            foreach (Collider collider in colliders)
            {
                if(collider.GetComponent<AgentStatus>())
                {
                    AgentStatus at = collider.GetComponent<AgentStatus>();
                    for (int i = 0; i < cols.Count; i++)
                    {
                        if (collider == cols[i])
                        {
                            StopCoroutine(corutime[i]);
                            switch (st)
                            {
                                case Stat.ATK:
                                    at.AddDamage -= PlusValue;
                                    at.himsDamage -= himsValue;
                                    break;
                                case Stat.DEF:
                                    at.DEF -= (int)PlusValue;
                                    break;
                                case Stat.CRIT:
                                    at.Cirt -= PlusValue;
                                    break;
                                case Stat.CRITDAM:
                                    at.CirtDAM -= PlusValue;
                                    break;
                            }
                            corutime.RemoveAt(i);
                            cols.RemoveAt(i--);
                        }
                    }
                    cols.Add(collider);
                    corutime.Add(StartCoroutine(at.Buffs(PlusValue, himsValue, st, BufTime)));

                }



            }
        }
    }


    protected float GetColliderRadius()
    {

        Vector3 colliderSize = GetColliderSize();
        return Mathf.Max(colliderSize.x, colliderSize.y, colliderSize.z) / 2f;

    }
    protected Vector3 GetColliderSize()
    {
        Collider collider = GetComponent<Collider>();


        BoxCollider boxCollider = (BoxCollider)collider;
        return Vector3.Scale(boxCollider.size, transform.localScale);
    }
}
