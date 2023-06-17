using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    static GameController _con;

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
    }

    public void StartGame()
    {
        TimeController.Instance.SetTime(1);
        for (int i = 0; i < players.Count; i++)
        {
            Render.SetActive(false);
            CO(i);
        }
    }

    private void Update()
    {
        if(stCount == players.Count)
        {
            stCount = 0;
            TimeController.Instance.SetTime(0);
            Render.SetActive(true);
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
