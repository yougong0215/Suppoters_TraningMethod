using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clear : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshPro t;
    [SerializeField] TextMeshPro p;
    void Start()
    {

            gameObject.SetActive(GameManager.Instance.GameClear);
        t.text = $"Ŭ���� Ƚ�� : {GameManager.Instance.clearcnt}";
        p.text = $"�й� Ƚ�� : {GameManager.Instance.diecnt}";

    }


}
