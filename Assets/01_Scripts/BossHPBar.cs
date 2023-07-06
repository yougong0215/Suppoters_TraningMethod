using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmped;
    [SerializeField] TextMeshProUGUI tmp;
    [SerializeField] Image img;
    [SerializeField] AgentStatus ag;

    private void Awake()
    {
        
    }

    private void Update()
    {
        
        tmp.text = $"{ag.HP}/{ag.stat.HP}";
        img.fillAmount = Mathf.Lerp(0f, 1f, (float)ag.HP / (float)ag.stat.HP);
    }




}
