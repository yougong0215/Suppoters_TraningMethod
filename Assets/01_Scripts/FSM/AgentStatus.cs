using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public enum Stat
{
    ATK,
    HP,
    DEF,
    CRIT,
    CRITDAM
}

public class AgentStatus : MonoBehaviour, IDamageAble
{
    [SerializeField] public CharacterStatues stat;
    [SerializeField] int HP;
    [SerializeField] int MaxHP;
    [SerializeField] TextDamageCast tmp;
    [SerializeField] Image hpbar;
    [SerializeField] public float AddDamage;
    [SerializeField] public float Cirt;
    [SerializeField] public float CirtDAM;
    [SerializeField] float DEF;
    [SerializeField] List<Coroutine> corutime = new List<Coroutine>();


    public LayerMask _player;
    private void Awake()
    {
        MaxHP = stat.HP;
        HP = stat.HP;
        //hpbar = transform.Find("HP").GetComponent<Image>();
    }
    public void TakeDamage(int value, Vector3 position, float cirt, bool critical)
    {
        TextDamageCast damageCast = PoolManager.Instance.Pop("TMP") as TextDamageCast;
        damageCast.Init((int)(100 / (100 + stat.DEF) * (value + Random.Range(-(value * 0.1f), (value * 0.1f))) * cirt), critical, position);
        HP -= (int)(100 / (100 + stat.DEF) * (value + Random.Range(-(value * 0.1f), (value * 0.1f))) * cirt);
    }


    public void Update()
    {
        if (HP > MaxHP)
        {
            HP = MaxHP;
        }
        if (HP <= 0)
        {
            GetComponent<FSM>().ChangeState(FSMState.Death);
        }
        Debug.Log(hpbar);
        if (hpbar)
        {
            Debug.Log(HP / MaxHP);
            hpbar.fillAmount = ((float)HP / (float)MaxHP);
        }
        else
        {
            Debug.Log(gameObject.name);

        }

        Collider[] colliders = null;


        colliders = Physics.OverlapSphere(transform.position, GetColliderRadius(), _player);

        for(int i =0; i< corutime.Count; i++)
        {
            StopCoroutine(corutime[i]);
            AddDamage = 0;
            Cirt = 0;
            CirtDAM = 0;
            DEF = 0;
        }
        foreach (Collider collider in colliders)
        {
            GetBuff stat;
            if (collider.TryGetComponent<GetBuff>(out stat))
            {
                corutime.Add(StartCoroutine(Buffs(stat.value, stat.st, stat.LifeTime)));
            }
        }
    }
    IEnumerator Buffs(float value, Stat st, float Time)
    {
        switch (st)
        {
            case Stat.ATK:
                AddDamage = value;
                break;
            case Stat.HP:
                HP += (int)value;
                break;
            case Stat.DEF:
                DEF += (int)value;
                break;
            case Stat.CRIT:
                Cirt += value;
                break;
            case Stat.CRITDAM:
                CirtDAM += value;
                break;
        }
        yield return new WaitForSeconds(Time);
        AddDamage = 0;
        Cirt = 0;
        CirtDAM = 0;
        DEF = 0;

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
}