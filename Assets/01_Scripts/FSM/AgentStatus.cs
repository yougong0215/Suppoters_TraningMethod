using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;

public class AgentStatus : MonoBehaviour, IDamageAble
{
    [SerializeField] CharacterStatues stat;
    [SerializeField] int HP;
    [SerializeField] int MaxHP;
    [SerializeField] TextDamageCast tmp;
    private void Awake()
    {
        MaxHP = stat.HP;
        HP = stat.HP;
    }
    public void TakeDamage(int value, Vector3 position, float cirt, bool critical)
    {
        TextDamageCast damageCast = PoolManager.Instance.Pop("TMP") as TextDamageCast;
        damageCast.Init((int)(100 / (100 + stat.DEF) * (value + Random.Range(-(value * 0.1f), (value * 0.1f)))  * cirt), critical, position);
        HP -= (int)(100 / (100 + stat.DEF) * (value + Random.Range(-(value * 0.1f), (value * 0.1f))) * cirt);
    }


    public void Update()
    {
        if(HP > MaxHP)
        {
            HP = MaxHP;
        }
        if(HP <=0)
        {
            GetComponent<FSM>().ChangeState(FSMState.Death);
        }
    }
}
