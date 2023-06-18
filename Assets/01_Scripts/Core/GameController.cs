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
    [SerializeField] List<SkillUIList> con = new List<SkillUIList>();

    private void Awake()
    {
        _con = this;
    }

    int stCount = 0;
    void Start()
    {
        Render = GameObject.Find("Render");

        cam.Priority = 100;
        TimeController.Instance.SetTime(0);
    }

    public void StartGame()
    {
        TimeController.Instance.SetTime(1);
        cam.Priority = 0;
        for (int i = 0; i < players.Count; i++)
        {
            Render.SetActive(false);
            cav.SetActive(true); ;
            CO(i);
        }
    }

    private void Update()
    {
        if(TimeController.Instance.Timer == 1)
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
                con[i].tmpCost.text = $"Cost : {con[i].Cost} / {con[i].MaxCost}";
        }

        if (stCount == players.Count)
        {
            stCount = 0;
            cam.Priority = 100;
            Render.SetActive(true);
            cav.SetActive(false);
            TimeController.Instance.SetTime(0);
        }
    }

    public void StopPlayer()
    {
        stCount++;
    }

    void CO(int i)
    {

        players[i].SetUseing(con[i].ReturnInfo());

    }

}
