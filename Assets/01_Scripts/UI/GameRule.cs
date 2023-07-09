using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRule : MonoBehaviour
{
    [SerializeField] AgentStatus pl1;
    [SerializeField] AgentStatus pl2;
    [SerializeField] AgentStatus pl3;
    [SerializeField] AgentStatus pl4;

    [SerializeField] AgentStatus BOSS;


    public void Update()
    {
        if(BOSS.HP <=0)
        {
            GameManager.Instance._savedata.GameClear = true;
            GameManager.Instance._savedata.clearcnt++;
            GameManager.Instance.JsonSave();
            GameManager.Instance.percent = 0;
            SceneManager.LoadScene("GameOver");

        }

        if(pl1.HP <=0)
            if (pl2.HP <= 0)
                if (pl3.HP <= 0)
                if (pl4.HP <= 0)
                    {
                        GameManager.Instance.percent = (float)BOSS.HP / (float)BOSS.MaxHP;
                        GameManager.Instance._savedata.diecnt++;
                        GameManager.Instance.JsonSave();
                        SceneManager.LoadScene("GameOver");
                    }
    }
}
