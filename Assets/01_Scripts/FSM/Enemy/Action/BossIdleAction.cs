using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossIdleAction : CommonAction
{
    public List<AttackBush> bus = new();
    public float ChangeTime = 3;
    public AttackBush Next;
    public List<AttackBush> Nextbus = new();
    
    [SerializeField] float curtime = 0;
    bool breakpatton = false;
    protected override void Init()
    {
        curtime = 0;
      
        if (com.FSMMain.ststed.HP < com.FSMMain.ststed.MaxHP * 0.25f)
        {
            if(breakpatton==false)
            {
                breakpatton = true;
                com.FSMMain.bus = Next;
                com.FSMMain.ChangeState(FSMState.Special);
                bus = Nextbus;
                ChangeTime *= 1.2f;
                com.FSMMain.Nexte = true;
            }
        }
    }

    protected override void OnEndFunc()
    {
    }

    protected override void OnEventFunc()
    {
        
    }

    protected override void OnUpdateFunc()
    {
        curtime += Time.deltaTime;
        if(curtime > ChangeTime)
        {
            if(com.FSMMain.Patton == false)
            {
                try
                {
                    curtime = 0;
                    com.FSMMain.bus = bus[0];
                    bus.Add(bus[0]);
                    bus.RemoveAt(0);
                    AttackBush temp;

                    GameObject[] players = GameObject.FindGameObjectsWithTag("Player1");
                    players = players.OrderBy(player => Vector3.Distance(com.FSMMain.transform.position, player.transform.position)).ToArray();
                    int t = Random.Range(0, players.Length);

                    com.FSMMain.LookRotations(players[t].transform.position);
                    com.FSMMain.SeeEnemy = players[t].transform.position;
                    com.FSMMain.ts = players[t].transform;


                    if (bus.Count > 2)
                    {
                        for (int i = 2; i < 30; i++)
                        {
                            int rand1 = Random.Range(2, bus.Count);
                            int rand2 = Random.Range(2, bus.Count);
                            temp = bus[rand1];
                            bus[rand1] = bus[rand2];
                            bus[rand2] = temp;
                        }

                    }

                    if (Vector3.Distance(com.FSMMain.transform.position, players[0].transform.position) > 3)
                    {

                        com.FSMMain.ChangeState(FSMState.Move);
                    }
                    else
                        com.FSMMain.ChangeState(FSMState.Skill);


                }
                catch
                {

                }


            }
            else
            {

            }
           
        }

    }

}
