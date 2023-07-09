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
        GameManager.Instance.JsonLoad();

            gameObject.SetActive(GameManager.Instance._savedata.GameClear);
        t.text = $"Å¬¸®¾î È½¼ö : {GameManager.Instance._savedata.clearcnt}";
        p.text = $"ÆÐ¹è È½¼ö : {GameManager.Instance._savedata.diecnt}";

    }


}
