using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
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
    [SerializeField] public float DEF;


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

        
    }
    public IEnumerator Buffs(float value, Stat st, float Time)
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

    private void OnDrawGizmos()
    {
        // Draw a wire sphere to represent the overlap area
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}
