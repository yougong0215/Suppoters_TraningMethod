using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class SaveData
{
    public bool GameClear = false;
    public int diecnt = 0;
    public int clearcnt= 0;
}


public class GameManager : Singleton<GameManager>
{
    public SaveData _savedata = new SaveData();
    public int HP;

    public float percent;
    Camera _cam;

    string path;
    public void JsonLoad()
    {
        path = Path.Combine(Application.dataPath, "database.json");

        SaveData saveData = new SaveData();


        Debug.Log(path);
        try
        {
            string loadJson = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            if (saveData != null)
            {
                _savedata.clearcnt = saveData.clearcnt;
                _savedata.GameClear = saveData.GameClear;
                _savedata.diecnt = saveData.diecnt;
            }
        }
        catch
        {
            JsonSave();
            Debug.LogError("로드 실패");
        }

        
    }

    public void JsonSave()
    {
        SaveData saveData = new SaveData();

        saveData.clearcnt  = _savedata.clearcnt;
        saveData.GameClear = _savedata.GameClear;
        saveData.diecnt = _savedata.diecnt;

        string json = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(path, json);
    }


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
