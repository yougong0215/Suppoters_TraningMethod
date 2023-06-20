 using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AttackBush : PoolAble
{
    [SerializeField] List<Image> img = new();
    [SerializeField] public float AttackTime = 2;
    [SerializeField] public float curtime = 0;
    [SerializeField] public AnimationClip anim;
    public bool FastNext = false;
    [SerializeField] public Vector3 posOffset;
    public AttackBush nextBush;

    public bool FrontPos;
    public float Angle = 0;
    public bool atk = true;

    bool att;

    public void Init(int dmg, float Critical, float criDamage, Vector3 pos)
    {
        transform.position = pos;
        curtime = 0;
        Damage += dmg;
        criDmg += criDamage;
        cri = Critical;
        transform.localEulerAngles += new Vector3(0, Angle, 0);
        atk = false;
        img = transform.GetChild(0).gameObject.GetComponentsInChildren<Image>().ToList();
        transform.position += posOffset;
        att = false;
        if(GetComponent<FollowPlayerAttack>())
        {
            atk = true;
        }
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
        if(AttackTime < curtime && att ==false)
        {
            att = true;
            atk = true;
            for(int i =0; i < img.Count; i++)
            {
                foreach (DamageCaster cast in img[i].GetComponentsInChildren<DamageCaster>().ToList())
                {
                    cast.Init(Damage, cri, criDmg);
                }
                
            }
            StartCoroutine(Push());
        }
    }

    IEnumerator Push()
    {
        yield return new WaitForSeconds(0.3f);
        PoolManager.Instance.Push(this);
    }
}
