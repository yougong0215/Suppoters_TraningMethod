using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public interface IDamageAble
{
    void TakeDamage(int value, Vector3 position, float Crit = 1, bool critical = false);
}

public class DamageCaster : PoolAble
{
    public LayerMask _enemy;
    public GetTypeShape shape;
    [Header("시간 ( 만약 한번만 공격할꺼면 frequency > lifeTIme )")]
    public float lifeTime = 0;           // Destroy
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
    GameObject effect;
    bool vfxcast = false;
    protected bool init = false;

    private void OnEnable()
    {
        MaxAttackfrequency = Attackfrequency;
    }

    public void Init(int damage, float cird, float cri)
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

        CriticalDamage += cird;
        Critical += cri;
        AttackDamage += damage;
        init = true;
    }

    protected void Update()
    {
        if(init == true)
        {
            lifeTime -= Time.deltaTime;
            Attackfrequency += Time.deltaTime;
            if (Attackfrequency > MaxAttackfrequency && lifeTime > 0)
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
                    Attackfrequency = 0;
                    if (collider.TryGetComponent<IDamageAble>(out damageable))
                    {
                        StartCoroutine(Attack(damageable, times / AttackCount, AttackCount-1, collider));
                    }
                }
            }

            if (lifeTime <= 0)
            {
                Destroy(effect);
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
    Random.Range(bounds.min.z, bounds.max.z)
       
);
        randomPosition.y += 3;

        if (Random.Range(0, 100f) >= Critical)
        {
            able.TakeDamage(AttackDamage, randomPosition, CriticalDamage, true);
        }
        else
        {
            able.TakeDamage(AttackDamage, randomPosition);
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
        return Mathf.Max(colliderSize.x, colliderSize.y, colliderSize.z) / 2f;

    }
    protected Vector3 GetColliderSize()
    {
        Collider collider = GetComponent<Collider>();


        BoxCollider boxCollider = (BoxCollider)collider;
        return Vector3.Scale(boxCollider.size, transform.localScale);
    }
}
