using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct skillinfo
{
    public FSMState state;
    public Sprite spi;
    public SkillSO _skill;
    [System.NonSerialized] public Vector3 dir;
    [System.NonSerialized] public Vector3 pos;

}

public class SkillUIList : MonoBehaviour
{
    public static int count = 4;
    [SerializeField] SimpleImg imgs;
    [SerializeField] GameObject UIs;
    [SerializeField] List<skillinfo> SkillInfo = new List<skillinfo>();
    [SerializeField] List<Image> img = new List<Image>();
    [SerializeField] List<SimpleImg> WorldUI = new List<SimpleImg>();

    public int SettingCount = 0;

    public int ReturnCount()
    {
        return SettingCount;
    }

    public void Setting(skillinfo info, Vector3 pos, Transform pl)
    {
        if(info.state == FSMState.Move)
        {

            info.pos = pos;
        }
        else
        {
            if(SkillInfo.Count > 0)
            {
                info.pos = SkillInfo[SettingCount - 1].pos;
            }
            else
            {
                info.pos = pl.position;
            }
        }
        info.dir = pos;

        
        SkillInfo.Add(info);
        
        WorldUI.Add(Instantiate(imgs));
        if (info.spi != null)
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
            // 자식 오브젝트를 즉시 삭제합니다.
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
            // 자식 오브젝트를 즉시 삭제합니다.
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
                if(SkillInfo[i].state == FSMState.Move)
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
        return SkillInfo;
    }
    

    private void Update()
    {

        if(WorldUI.Count > 0)
        {
            for(int i =0; i < WorldUI.Count; i++)
            {
                if(SkillInfo[i].state != FSMState.Move)
                    WorldUI[i].transform.position = SkillInfo[i].pos + new Vector3(-0.2f, 0+i, 0.2f);
                else
                {
                    WorldUI[i].transform.position = SkillInfo[i].pos;
                }
                WorldUI[i].Seting(SkillInfo[i].spi, i+1);
            }
        }
    }
}
