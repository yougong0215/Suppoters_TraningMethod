using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraSeeing : MonoBehaviour
{
    [SerializeField] players st;
    AgentStatus _info;
    [Header("UI")]
    [SerializeField] Image HPBAR;
    [SerializeField] Image MPBar;
    [SerializeField] TextMeshProUGUI tmp;
    // Update is called once per frame
    private void Awake()
    {
        _info = GameObject.Find(st.ToString()).GetComponent<AgentStatus>();
        tmp.text = st.ToString();
    }
    void Update()
    {
        HPBAR.fillAmount = (float)_info.HP / (float)_info.stat.HP;
        MPBar.fillAmount = (float)_info.Cost / (float)_info.stat.Cost;

        Quaternion rot = GameManager.Instance.Cam.transform.rotation;
        //rot.z = 0;
        transform.rotation = rot;
        transform.position = _info.transform.position + new Vector3(0,1.2f,0);
    }
}
