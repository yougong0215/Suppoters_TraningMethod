using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FollowPlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public float FollowTime = 2f;
    public float Speed = 2f;
    Transform vec;
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player1");
        players = players.OrderBy(player => Vector3.Distance(transform.position, player.transform.position)).ToArray();
        vec = players[0].transform;

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,vec.position) > 0.2f && GetComponent<AttackBush>().AttackTime - GetComponent<AttackBush>().curtime > FollowTime)
        {
            transform.position += (vec.position+ new Vector3(0,0.1f,0) - transform.position).normalized * Speed * Time.deltaTime;
            
        }
    }
}
