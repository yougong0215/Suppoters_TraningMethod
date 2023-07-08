using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamLockUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Image checkImg;
    [SerializeField] Sprite Check;
    [SerializeField] Sprite nonCehck;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(CameraController.Instance.CamLock);

    }
    private void Update()
    {
        if (CameraController.Instance.GetLock())
        {
            checkImg.sprite = Check;
        }
        else
        {
            checkImg.sprite = nonCehck;
        }
    }
}


