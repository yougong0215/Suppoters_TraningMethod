using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum players
{
    None =0,
    DamVI,
    Haru,
    Hosino,
    ALO
}

public class CameraController : Singleton<CameraController>
{

    [SerializeField] Dictionary<players, CinemachineVirtualCamera> dic = new Dictionary<players, CinemachineVirtualCamera>();

    public void Submit(players pl, CinemachineVirtualCamera cam)
    {
        dic.Add(pl, cam);
    }

    public void SetCam(players pl)
    {
        for(int i = 0; i < System.Enum.GetValues(typeof(players)).Length; i++)
        {
            if (dic.ContainsKey((players)i))
            if((players)i == pl)
            {
                
                dic[(players)i].Priority = 11;
            }
            else
            {
                dic[(players)i].Priority = 10;
            }
        }
    }
}
