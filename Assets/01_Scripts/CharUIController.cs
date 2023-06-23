using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct UIInfo
{
    public GameObject obj;
    public players pl;
}

public class CharUIController : MonoBehaviour
{
    [SerializeField] List<UIInfo> obj;
    Dictionary<players, GameObject> gm = new Dictionary<players, GameObject>();

    private void Awake()
    {
        for(int i = 0;i < obj.Count; i++)
        {
            gm.Add(obj[i].pl, obj[i].obj);
            gm[obj[i].pl].gameObject.SetActive(false);
        }
        Active(false);
        
    }

    public void Click(players pl)
    {
        for (int i = 0; i <= System.Enum.GetValues(typeof(players)).Length; i++)
        {
            if (gm.ContainsKey((players)i))
                if ((players)i == pl)
                {
                    gm[(players)i].SetActive(true);
                }
                else
                {
                    gm[(players)i].SetActive(false);
                }
        }
    }

    public void Active(bool  b)
    {
        if(b == false)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
        }
    }

}
