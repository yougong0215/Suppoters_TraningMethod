 using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AttackBush : PoolAble
{
    [SerializeField] List<Image> img = new();
    [SerializeField] float AttackTime = 2;
    [SerializeField] float curtime = 0;

    bool atk = false;

    public void Init(int dmg, float Critical, float criDamage)
    {
        Damage += dmg;
        criDmg += criDamage;
        cri = Critical;
        atk = false;
        img = transform.GetChild(0).gameObject.GetComponentsInChildren<Image>().ToList();
    }

    private void Awake()
    {
        img = transform.GetChild(0).gameObject.GetComponentsInChildren<Image>().ToList();
    }

    [Header("Stat")]
    [SerializeField] int Damage = 500;
    [SerializeField] float criDmg;
    [SerializeField] float cri;


    private void Update()
    {
        curtime += Time.deltaTime;

        for(int i =0; i < img.Count; i++)
        {
            img[i].fillAmount = Mathf.Lerp(0f, 1f, curtime / AttackTime);
            img[i].color = new Color(img[i].color.r, img[i].color.g, img[i].color.b, Mathf.Lerp(0f, 1f, curtime / AttackTime));
        }
        if(AttackTime < curtime && atk == false)
        {
            atk = true;
            for(int i =0; i < img.Count; i++)
            {
                foreach (DamageCaster cast in img[i].GetComponentsInChildren<DamageCaster>().ToList())
                {
                    cast.Init(Damage, cri, criDmg);
                }
            }
        }
    }
}
