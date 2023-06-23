using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    static GameController _con;

    public CinemachineVirtualCamera cam;
    public CharUIController cav;
    public TextMeshProUGUI tmp;

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
    bool notStart = false;
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

    public void TMPSetMessage(string tmp)
    {
        this.tmp.text = tmp;
    }

    public void StartGame()
    {
        int die = 0;
        bool bossdie = false;
        for (int i =0; i < con.Count;i++)
        {
            if (SkillUIList.count > con[i].SettingCount && players[i].NowState() != FSMState.Death)
            {
                notStart = true;
            }
            if(players[i].NowState() == FSMState.Death)
            {
                die++;
            }
            if (GameObject.Find("Boss").GetComponent<FSM>().NowState() == FSMState.Death)
            {
                bossdie = true;
            }
        }
        if (bossdie)
        {
            tmp.transform.parent.GetComponent<Button>().onClick.RemoveAllListeners();
            tmp.transform.parent.GetComponent<Button>().onClick.AddListener(() => { SceneManager.LoadScene("Start"); });
            return;
        }
        if (die==players.Count)
        {
            tmp.transform.parent.GetComponent<Button>().onClick.RemoveAllListeners();
            tmp.transform.parent.GetComponent<Button>().onClick.AddListener(() => { SceneManager.LoadScene("Start"); });
            return;
        }
        if(notStart==true)
        {
            tmp.text = "NOT Ready";
            notStart = false;
            return;
        }

        for (int i = 0; i <= players.Count; i++)
        {
            dic[(players)i] = false;
        }
        TimeController.Instance.SetTime(1);
        stCount = 0;
        cam.Priority = 0;
        cans.Priority = -100;
        GameManager.Instance.Cam.depth = 4;
        cav.Active(true);
        CameraController.Instance.SetCam(global::players.None);
        for (int i = 0; i < players.Count; i++)
        {
            Render.SetActive(false);
            CO(i);
        }
    }

    private void Update()
    {
        int die = 0;
        int te = 0;
        bool bossdie = false;
        if (stCount >= players.Count)
        {
            stCount = 0;
            cans.Priority = 100;
            GameManager.Instance.Cam.depth = 1;
            Render.SetActive(true);
            cav.Active(false);
            TimeController.Instance.SetTime(0);
            CameraController.Instance.EndGame();
        }

        for (int i = 0; i < con.Count; i++)
        {
            if (SkillUIList.count == con[i].SettingCount)
            {
                te++;
            }
            if (players[i].NowState() == FSMState.Death)
            {
                die++;
            }
            if(GameObject.Find("Boss").GetComponent<FSM>().NowState() == FSMState.Death)
            {
                bossdie = true;
            }
        }
        if(bossdie)
        {
            tmp.text = "당신은 죽였다 보스";
            return;
        }

        if (die == players.Count)
        {
            tmp.text = "Game Over";
            return;
        }

        if (te == con.Count)
        {
            
            tmp.text = "Start";
        }


        Debug.Log($"stC  : {stCount}");

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
