using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingCredit : MonoBehaviour
{
    [SerializeField][TextArea] string BadEnd1;
    [SerializeField] string one;
    [SerializeField][TextArea] string BadEnd2;
    [SerializeField] string otwo;
    [SerializeField][TextArea] string BadEnd3;
    [SerializeField] string three;
    [SerializeField][TextArea] string TruEnd;
    [SerializeField] string fo;

    [SerializeField] TextMeshProUGUI tmp;
    
    float curtime = 0;
    private void Awake()
    {
        tmp.color = new Color(1, 1, 1, 0);

        if (GameManager.Instance.percent > 0.75f)
        {
            GetComponent<TextMeshProUGUI>().text = BadEnd1;
            tmp.text = one;
            return;
        }
        if (GameManager.Instance.percent > 0.4f)
        {
            GetComponent<TextMeshProUGUI>().text = BadEnd2;
            tmp.text = otwo;
            return;
        }
        if(GameManager.Instance.GameClear==false)
        {
            GetComponent<TextMeshProUGUI>().text = BadEnd3;
            tmp.text = three;
            return;
        }

        GetComponent<TextMeshProUGUI>().text = TruEnd;
        tmp.text = fo;
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.Return))
        transform.position += new Vector3(0, 500, 0) * Time.deltaTime;
        else
        {
            transform.position += new Vector3(0, 30, 0) * Time.deltaTime;
        }

        if(GetComponent<RectTransform>().position.y > 13300)
        {

            curtime += Time.deltaTime /2;
            tmp.color = new Color(1, 1, 1, curtime);
        }
    }
}
