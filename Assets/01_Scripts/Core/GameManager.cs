using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool GameClear;
    public int HP;
    public int diecnt;
    public int clearcnt;


    public float percent;
    Camera _cam;

    public Camera Cam
    {
        get
        {
            if(_cam == null)
            {
                _cam = Camera.main;
            }
            return _cam;
        }
    }
}
