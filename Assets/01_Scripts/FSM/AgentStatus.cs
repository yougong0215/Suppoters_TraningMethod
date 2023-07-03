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
    [SerializeField] public players pl;
    [SerializeField] public CharacterStatues stat;
    [SerializeField] public int HP;
    [SerializeField] public int MaxHP;
    [SerializeField] TextDamageCast tmp;
    [SerializeField] Image hpbar;
    [SerializeField] public float AddDamage;
    [SerializeField] public float himsDamage;
    [SerializeField] public float Cirt;
    [SerializeField] public float CirtDAM;
    [SerializeField] public float DEF;



    public LayerMask _player;
    private void Awake()
    {
        MaxHP = stat.HP;
        HP = stat.HP;
        himsDamage = 1;
        //hpbar = transform.Find("HP").GetComponent<Image>();
    }
    public void TakeDamage(int value, Vector3 position, float cirt, bool critical, bool nuck)
    {
        TextDamageCast damageCast = PoolManager.Instance.Pop("TMP") as TextDamageCast;
        
        if(nuck==true)
        {
            if(GetComponent<FSM>().NowState() != FSMState.WakeUp)
            GetComponent<FSM>().ChangeState(FSMState.nuckdown);
        }

        float valued = (value + Random.Range(-(value * 0.1f), (value * 0.1f)));

        //Debug.Log($"DMG : {value} => {(int)(100 / (100 + stat.DEF))} * { (value + Random.Range(-(value * 0.1f), (value * 0.1f)))} * {cirt}" +
        //    $"= {(int)(100 / (100 + stat.DEF) * (value + Random.Range(-(value * 0.1f), (value * 0.1f))) * cirt)}"); 

        damageCast.Init((int)(100 / (100 + stat.DEF) * valued * cirt), critical, position);
        HP -= (int)((100 / (100 + stat.DEF) * valued) * cirt);
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
            if(HP > 0)
            {
                Debug.Log(HP / MaxHP);
                hpbar.fillAmount = ((float)HP / (float)MaxHP);
            }
            else
            {
                hpbar.fillAmount = 0;
            }

        }
        else
        {
            Debug.Log(gameObject.name);

        }

        
    }
    public IEnumerator Buffs(float value, float hims, Stat st, float Time)
    {
        switch (st)
        {
            case Stat.ATK:
                AddDamage += value;
                himsDamage += hims;
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
        Debug.Log($"{st.ToString()} {gameObject.name} Ω√¿€ : {value}|{hims}");
        
        yield return new WaitForSeconds(Time);
        Debug.Log($"{st.ToString()} ≥° : {value}");
        switch (st)
        {
            case Stat.ATK:
                AddDamage -= value;
                himsDamage -= hims;
                break;
            case Stat.DEF:
                DEF -= (int)value;
                break;
            case Stat.CRIT:
                Cirt -= value;
                break;
            case Stat.CRITDAM:
                CirtDAM -= value;
                break;
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a wire sphere to represent the overlap area
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}
