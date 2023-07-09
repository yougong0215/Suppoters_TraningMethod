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
    [SerializeField] bool ending = false;
    Vector3 vec;
    float curtime = 0;
    private void Start()
    {
        if(tmp == null)
        {
            tmp = GameObject.Find("tmp").GetComponent<TextMeshProUGUI>();
        }
        vec = tmp.transform.position;

        tmp.color = new Color(1, 1, 1, 0);

        if(ending== false)
        {
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
            if (GameManager.Instance._savedata.GameClear == false)
            {
                GetComponent<TextMeshProUGUI>().text = BadEnd3;
                tmp.text = three;
                return;
            }

            GetComponent<TextMeshProUGUI>().text = TruEnd;
            tmp.text = fo;
        }
        
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
            Debug.Log("¹¹³ó");
            curtime += Time.deltaTime /2;
            tmp.color = new Color(1, 1, 1, curtime);
        }
        tmp.transform.position = vec;
    }
}
