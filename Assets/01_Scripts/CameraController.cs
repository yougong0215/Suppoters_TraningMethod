using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum players
{
    None =0,
    DamVI,
    Haru,
    Hosino,
    ALO,
    
}

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [SerializeField] Dictionary<players, CinemachineVirtualCamera> dic = new Dictionary<players, CinemachineVirtualCamera>();

    public void CamLock()
    {
        cammod = cammod == false ? true : false;
    }

    public bool GetLock()
    {
        return cammod;
    }

    players selected = players.None;
    players lasted = players.DamVI;
    public void Submit(players pl, CinemachineVirtualCamera cam)
    {
        dic.Add(pl, cam);
    }

    float x, y;

    bool cammod = true;

    private void Awake()
    {
        Instance = this;
        x = Screen.width;
        y = Screen.height;
    }

    public Vector3 NoneCameraPos { get; private set; } = Vector3.zero;

    private void Update()
    {
        if (dic.ContainsKey(selected))
            dic[selected].Priority = 100;

        if(selected != players.None)
        {
            NoneCameraPos = dic[selected].transform.parent.position;
            lasted = selected;
        }

        Debug.Log($"¸¶¿ì½º : {Input.mousePosition}");
        if(Input.GetKeyDown(KeyCode.Y))
        {
             cammod = cammod == false ? true : false;
        }

        
        if(Input.GetKey(KeyCode.Space))
        {
            SetCam(lasted);
            if(Input.GetKeyUp(KeyCode.Space))
            {
                if(cammod ==false)
                 SetCam(players.None);
            }
        }

        if (cammod==false)
        {
            if (Input.mousePosition.x < 20)
            {
                SetCam(players.None);
                NoneCameraPos += new Vector3(-10, 0, -10) * Time.unscaledDeltaTime;
            }

            if (Input.mousePosition.x > x - 20)
            {
                SetCam(players.None);
                NoneCameraPos += new Vector3(10, 0, 10) * Time.unscaledDeltaTime;
            }


            if (Input.mousePosition.y < 20)
            {
                SetCam(players.None);
                NoneCameraPos += new Vector3(10, 0, -10) * Time.unscaledDeltaTime;
            }

            if (Input.mousePosition.y > y - 20)
            {
                SetCam(players.None);
                NoneCameraPos += new Vector3(-10, 0, 10) * Time.unscaledDeltaTime;
            }
        }
        else { SetCam(lasted); }

    }

    public void SetCam(players pl)
    {
        selected = pl;
        for(int i = 0; i < System.Enum.GetValues(typeof(players)).Length; i++)
        {
            if (dic.ContainsKey((players)i))
                dic[(players)i].Priority = 10;
            
        }
        dic[selected].Priority = 100;
        
    }

    

    public void EndGame()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(players)).Length; i++)
        {
            if (dic.ContainsKey((players)i))
                    dic[(players)i].Priority = 10;
                
        }
    }
}
