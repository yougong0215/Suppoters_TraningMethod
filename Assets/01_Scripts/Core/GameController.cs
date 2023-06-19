using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class GameController : MonoBehaviour
{
    static GameController _con;

    public CinemachineVirtualCamera cam;
    public GameObject cav;
    public Camera main;
    public Camera sub;

    float curtime = 0;

    public static GameController Contorller
    {
        get
        {

            return _con;
        }
    }
    GameObject Render;
    Button startBtn;
    [SerializeField] List<FSM> players = new List<FSM>();
    [SerializeField] Dictionary<players, bool> dic = new();
    [SerializeField] List<SkillUIList> con = new List<SkillUIList>();
    CinemachineVirtualCamera cans;

    private void Awake()
    {
        _con = this;
    }

    int stCount = 0;
    void Start()
    {
        Render = GameObject.Find("Render");
        cans = GameObject.Find("MiniCams").GetComponent<CinemachineVirtualCamera>();
        for (int i = 0; i <= players.Count; i++)
        {
            dic.Add((players)i, false);
        }
        cam.Priority = 100;
        GameManager.Instance.Cam.depth = 1;
        TimeController.Instance.SetTime(0);
    }

    public void StartGame()
    {
        for (int i = 0; i <= players.Count; i++)
        {
            dic[(players)i] = false;
        }
        CameraController.Instance.EndGame();
        TimeController.Instance.SetTime(1);
        stCount = 0;
        cam.Priority = 0;
        cans.Priority = -100;
        GameManager.Instance.Cam.depth = 4;
        for (int i = 0; i < players.Count; i++)
        {
            Render.SetActive(false);
            cav.SetActive(true); ;
            CO(i);
        }
    }

    private void Update()
    {
        Debug.Log($"stC  : {stCount}");
        if (stCount >= players.Count)
        {
            stCount = 0;
            cans.Priority = 100;
            GameManager.Instance.Cam.depth = 1;
            Render.SetActive(true);
            cav.SetActive(false);
            TimeController.Instance.SetTime(0);
        }
        if (TimeController.Instance.Timer == 1)
        {
            curtime += Time.deltaTime;
            if(curtime >= 1)
            {
                for (int i = 0; i < con.Count; i++)
                {
                    con[i].Cost++;
                }
                curtime = 0;
            }
        }
        else
        {
            for (int i = 0; i < con.Count; i++)
            {

                con[i].Cost = Mathf.Clamp(con[i].Cost, 0, con[i].MaxCost);
                con[i].tmpCost.text = $"Cost : {con[i].Cost} / {con[i].MaxCost}";
            }
        }
        
    }

    public void StopPlayer(players pl)
    {
        if(dic[pl] == false)
        {
            stCount++;
            Debug.Log($"stC {pl.ToString()} : {stCount}");
            dic[pl] = true;
        }
        
    }

    void CO(int i)
    {

        players[i].SetUseing(con[i].ReturnInfo());

    }

}
