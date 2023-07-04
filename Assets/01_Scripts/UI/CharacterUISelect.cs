using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUISelect : MonoBehaviour
{
    [SerializeField] List<InGameCharacterBar> _list = new();
    [SerializeField] InGameCharacterBar SelectUI;
    [Header("Position")]
    [SerializeField] RectTransform View;
    [SerializeField] RectTransform Scroll;
    private void Awake()
    {
        foreach (InGameCharacterBar ch in _list)
        {
            if (ch.Select == true)
            {
                SelectUI = ch;
                SelectUI.transform.parent = View.transform;
                SelectUI.GetComponent<RectTransform>().position = View.position;
                ch.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                ch.transform.parent = Scroll;
                ch.transform.localScale = new Vector3(1, 1, 1) * 0.5f;
            }
        }
    }
    private void Update()
    {


        if(SelectUI.Select == false)
        {
            foreach(InGameCharacterBar ch in _list)
            {
                if(ch.Select==true)
                {
                    SelectUI = ch;
                    SelectUI.transform.parent = View.transform;
                    SelectUI.GetComponent<RectTransform>().position = View.position;
                    ch.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    ch.transform.parent = Scroll;
                    ch.transform.localScale = new Vector3(1, 1, 1) * 0.5f;
                }
            }
        }
    }

    public void Select()
    {
        foreach (InGameCharacterBar ch in _list)
        {
            ch.Select = false;
        }
    }

    public void ObjectActive(bool b)
    {

        Scroll.gameObject.SetActive(b);

    }



}
