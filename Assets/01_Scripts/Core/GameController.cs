using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    static GameController _con;

    public CinemachineVirtualCamera cam;
    public GameObject cav;

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
        TimeController.Instance.SetTime(0);
        cam.Priority = 100;
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
        if(stCount == players.Count)
        {
            stCount = 0;
            TimeController.Instance.SetTime(0);
            cam.Priority = 100;
            Render.SetActive(true);
            cav.SetActive(false);
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
