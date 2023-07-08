using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public interface IDamageAble
{
    void TakeDamage(int value, Vector3 position, float Crit = 1, bool critical = false, bool nuck =false);
}

public class DamageCaster : PoolAble
{
    public LayerMask _enemy;
    public GetTypeShape shape;
    [Header("시간 ( 만약 한번만 공격할꺼면 frequency > lifeTIme )")]
    public float lifeTime = 0;           // Destroy
    [System.NonSerialized]public float onLife;
    [Header("공격 속도")]
    public float Attackfrequency = 0.4f; // 공격 속도
    float MaxAttackfrequency = 0.4f;


    [Header("공격 빈도")]
    public int AttackCount = 1;
    public float times = 1f;
    [Header("데미지 SO연결 필요")]
    public int AttackDamage = 10;
    public float CriticalDamage = 2;
    public float Critical = 0;
    [Header("VFX")]
    public VisualEffect vfx;
    public ParticleSystem ps;
    public VisualEffect vfxHit;
    public ParticleSystem effectHit;
    VisualEffect Veffect;
    ParticleSystem Peffect;
    VisualEffect VeffectHit;
    ParticleSystem PeffectHit;
    bool vfxcast = false;
    protected bool init = false;
    bool b;
    public bool NuckBackAttack;
    public bool DonotPool;
    public bool justCast;

    [Header("Sound")]
    [SerializeField] AudioClip FireSound;
    [SerializeField] AudioClip HitSound;
    [SerializeField] AudioSource ad;
    private void Awake()
    {
        onLife = lifeTime;
        MaxAttackfrequency = Attackfrequency;
        ad = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        init = false;
    }

    public void Init(int damage, float cir, float crid)
    {
        if(FireSound!=null)
            ad.PlayOneShot(FireSound);

        Attackfrequency = MaxAttackfrequency;
        lifeTime = onLife;
        if (vfx != null)
        {
            GameObject obj = Instantiate(vfx.gameObject, transform);


            Veffect = obj.gameObject.GetComponent<VisualEffect>();
            Veffect.transform.rotation = transform.rotation;
        }

        if (ps != null)
        {
            GameObject obj = Instantiate(ps.gameObject, transform);

            Peffect = obj.gameObject.GetComponent<ParticleSystem>();
        }
        Debug.Log($"CRIIN { crid}");
        CriticalDamage += crid;
        Critical += cir;
        AttackDamage += damage;
        init = true;
    }

    protected void FixedUpdate()
    {
        if (justCast == true)
            return;
        if (Time.timeScale == 0)
        {
            b = true;
            if (Peffect)
            {
                Peffect.Pause();
            }
            if (Veffect)
            {
                Veffect.pause = true;
            }
            if (PeffectHit)
            {
                PeffectHit.Pause();
            }
            if (VeffectHit)
            {
                VeffectHit.pause = true;
            }
        }
        else
        {
            if (b == true)
            {
                b = false;
                if (PeffectHit)
                {
                    PeffectHit.Play();
                }
                if (VeffectHit)
                {
                    VeffectHit.pause = false;
                }
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

        if (init == true)
        {
            lifeTime -= Time.deltaTime;
            Attackfrequency += Time.deltaTime;
            if (Attackfrequency >= MaxAttackfrequency && lifeTime > 0)
            {
                Collider[] colliders = null;

                switch (shape)
                {
                    case GetTypeShape.Sphere:
                        colliders = Physics.OverlapSphere(transform.position, GetColliderRadius(), _enemy);
                        break;
                    case GetTypeShape.Box:
                        colliders = Physics.OverlapBox(transform.position, GetColliderSize() / 2f, transform.rotation, _enemy);
                        break;
                    case GetTypeShape.Polygon:
                        break;
                }

                foreach (Collider collider in colliders)
                {
                    IDamageAble damageable;
                    
                    {
                        Attackfrequency = 0;
                        Debug.Log($"적 : {collider.name}");
                        if (collider.TryGetComponent<IDamageAble>(out damageable))
                        {
                            StartCoroutine(Attack(damageable, times / AttackCount, AttackCount - 1, collider));
                        }
                    }
                    //ustCast = true;
                   
                }
            }

            if (lifeTime <= 0)
            {
                if(Veffect)
                    Destroy(Veffect.gameObject);
                if (Peffect)
                    Destroy(Peffect.gameObject);
                foreach(GetBuff buf in GetComponentsInChildren<GetBuff>())
                {
                    buf.OnEnd();
                }
                if(DonotPool == false)
                    PoolManager.Instance.Push(this); // 재생 시간이 끝났을 때 오브젝트를 파괴합니다.
            }
        }


    }

    IEnumerator Attack(IDamageAble able, float timed, int count, Collider enemyCollider)
    {
        Bounds bounds = enemyCollider.bounds;

        Vector3 randomPosition = new Vector3(
    Random.Range(bounds.min.x, bounds.max.x),
    Random.Range(bounds.min.y, bounds.max.y),
    Random.Range(bounds.min.z, bounds.max.z));
        randomPosition.y += 1;



        if (Random.Range(0, 100f) <= Critical)
        {
            ad.PlayOneShot(HitSound);
            able.TakeDamage(AttackDamage, randomPosition, CriticalDamage, true, NuckBackAttack);
        }
        else
        {
            ad.PlayOneShot(HitSound);
            able.TakeDamage(AttackDamage, randomPosition, 1, false, NuckBackAttack);
        }

        if (vfxHit != null)
        {
            GameObject obj = Instantiate(vfxHit.gameObject, null);
            obj.transform.position = enemyCollider.gameObject.transform.position;
            obj.AddComponent<VFXCancers>();
        }

        if (effectHit != null)
        {
            GameObject obj = Instantiate(effectHit.gameObject, null);
            obj.transform.position = enemyCollider.gameObject.transform.position;
            obj.AddComponent<VFXCancers>();
        }

        yield return new WaitForSeconds(timed);

        if(count != 0)
        {

            StartCoroutine(Attack(able,timed,count-1, enemyCollider));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        switch (shape)
        {
            case GetTypeShape.Sphere:
                Gizmos.DrawWireSphere(transform.position, GetColliderRadius());
                break;
            case GetTypeShape.Box:
                Gizmos.DrawWireCube(transform.position, GetColliderSize());
                break;
            case GetTypeShape.Polygon:
                break;
        }
    }

    protected float GetColliderRadius()
    {

        Vector3 colliderSize = GetColliderSize();
        return Mathf.Max(colliderSize.x, colliderSize.y, colliderSize.z) / 2;

    }
    protected Vector3 GetColliderSize()
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = true;
        switch (shape)
        {
            case GetTypeShape.Box:
                BoxCollider boxCollider = (BoxCollider)collider;
                boxCollider.enabled = false;
                return Vector3.Scale(boxCollider.size, transform.localScale);
            case GetTypeShape.Sphere:
                SphereCollider sphereCollider = (SphereCollider)collider;
                float radius = sphereCollider.radius * Mathf.Max(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                sphereCollider.enabled = false;
                return new Vector3(radius, radius, radius);
        }

        return Vector3.zero;
    }
}
