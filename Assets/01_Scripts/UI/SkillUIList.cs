using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

[System.Serializable]
public struct skillinfo
{
    public FSMState state;
    public Sprite spi;
    public SkillSO _skill;
     public Vector3 dir;
     public Vector3 pos;
    public bool Fire;
    public int Cost;
    public float WaitTime;

}

public class SkillUIList : MonoBehaviour
{
    public static int count = 4;
    [SerializeField] SimpleImg imgs;
    [SerializeField] GameObject UIs;
    [SerializeField] public TextMeshProUGUI tmpCost;
    [SerializeField] List<skillinfo> SkillInfo = new List<skillinfo>();
    [SerializeField] List<Image> img = new List<Image>();
    [SerializeField] List<SimpleImg> WorldUI = new List<SimpleImg>();

    public int SettingCount = 0;

    [Header("Cost")]
    public int Cost  = 20;
    public int MaxCost = 20;

    public int ReturnCount()
    {
        return SettingCount;
    }

    public void Setting(skillinfo info, Vector3 pos, Transform pl)
    {
        Cost -= info.Cost;
        info.pos = pos;

        if (info.state == FSMState.Move || info.state == FSMState.Dash || info.state == FSMState.Telpo)
        {

            info.pos = pos;
        }
        else
        {
            if(SkillInfo.Count > 0)
            {
                info.pos = SkillInfo[SkillInfo.Count-1].pos;
            }
            else
            {
                info.pos = pl.position;
            }
        }
        info.dir = pos;

        
        SkillInfo.Add(info);
        
        WorldUI.Add(Instantiate(imgs));

        if (SkillInfo[SkillInfo.Count - 1].state != FSMState.Move || SkillInfo[SkillInfo.Count - 1].state != FSMState.Dash || SkillInfo[SkillInfo.Count - 1].state == FSMState.Telpo)
        {
            Debug.Log(SkillInfo[SettingCount].pos);
            WorldUI[SettingCount].transform.position = SkillInfo[SettingCount].pos + new Vector3(-0.2f, 0.1f, 0.2f);
        }
        else
        {
            WorldUI[SettingCount].transform.position = SkillInfo[SettingCount].pos;
        }
        WorldUI[SkillInfo.Count - 1].Seting(SkillInfo[SkillInfo.Count - 1].spi, SkillInfo.Count - 1 + 1);

        if(info.spi != null)
        {
            WorldUI[SettingCount].Seting(info.spi, SettingCount + 1);
            img[SettingCount].sprite = info.spi;

        }

        img[SettingCount].GetComponent<ClickSettingUI>().bSet();

        
        SettingCount++;
        
    }    


    public void RemoveNum(int t)
    {
        Transform parent = transform;
        int childCount = parent.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            if (img.Count > 0)
                img.RemoveAt(i);
            Transform child = parent.GetChild(i);
            // �ڽ� ������Ʈ�� ��� �����մϴ�.
            DestroyImmediate(child.gameObject);
        }
        int cnt = SettingCount;

        for (int i = 0; i < cnt; i++)
        {
            if (t <= i)
            {
                SettingCount--;
                Destroy(WorldUI[SettingCount].gameObject);
                WorldUI.RemoveAt(SettingCount);
                Cost += SkillInfo[SettingCount].Cost;
                SkillInfo.RemoveAt(SettingCount);
            }
        }
        for (int i = 0; i < count; i++)
        {


            img.Add(Instantiate(UIs, transform).GetComponent<Image>());
            img[i].GetComponent<ClickSettingUI>().SetNums(i);
            if (SkillInfo.Count > i && SkillInfo[i].spi != null)
            {
                img[i].GetComponent<ClickSettingUI>().bSet();
                img[i].sprite = SkillInfo[i].spi;
            }

        }

    }
    private void OnEnable()
    {
        ClickSecond(count);
    }

    public void ClickSecond(int c)
    {
        count = c;
        Transform parent = transform;
        int childCount = parent.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            if(img.Count > 0)
                img.RemoveAt(i);
            Transform child = parent.GetChild(i);
            // �ڽ� ������Ʈ�� ��� �����մϴ�.
            DestroyImmediate(child.gameObject);
        }
        int cnt = SettingCount;

        for(int i =0; i < cnt; i++)
        {
            if (count-1 < i)
            {
                SettingCount--;
                Destroy(WorldUI[SettingCount].gameObject);
                WorldUI.RemoveAt(SettingCount);
                SkillInfo.RemoveAt(SettingCount);
            }
        }
        for (int i =0; i < count; i++)
        {
            

            img.Add(Instantiate(UIs, transform).GetComponent<Image>());
            img[i].GetComponent<ClickSettingUI>().SetNums(i);
            if (SkillInfo.Count > i && SkillInfo[i].spi != null)
            {
                img[i].GetComponent<ClickSettingUI>().bSet();
                img[i].sprite = SkillInfo[i].spi;
            }

        }
    }

    public Vector3 returnpos()
    {
        if(SkillInfo.Count > 0)
        {
            print(SettingCount);
            for(int i = SettingCount-1; i >=0; i--)
            {
                if(SkillInfo[i].state == FSMState.Move || SkillInfo[i].state == FSMState.Dash)
                {
                    return SkillInfo[i].dir;
                }
            }
            return SkillInfo[0].pos;
        }
        else
        {
            return Vector3.zero;
        }
    }
    public List<skillinfo> ReturnInfo()
    {

        for(int i = 0; i < WorldUI.Count; i ++)
        { 
            Destroy(WorldUI[i].gameObject);
        }
        WorldUI.Clear();
        SettingCount = 0;
        return SkillInfo;
    }
    
    

}
