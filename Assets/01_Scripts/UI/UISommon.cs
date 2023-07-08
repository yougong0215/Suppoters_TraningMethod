using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;
using Cinemachine;

public class UISommon : MonoBehaviour
{
    public List<Button> bList = new();

    private void Awake()
    {
        bList = transform.GetComponentsInChildren<Button>().ToList();
        foreach(Button btn in bList)
        {
            btn.interactable = false;
        }
    }
    void Start()
    {
        DOTween.Init();
        // transform �� scale ���� ��� 0.1f�� �����մϴ�.
        transform.localScale = Vector3.one * 0.1f;
        // ��ü�� ��Ȱ��ȭ �մϴ�.
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);


        // DOTween �Լ��� ���ʴ�� �����ϰ� ���ݴϴ�.
        var seq = DOTween.Sequence();

        // DOScale �� ù ��° �Ķ���ʹ� ��ǥ Scale ��, �� ��°�� �ð��Դϴ�.
        seq.Append(transform.DOScale(1.1f, 0.2f));
        seq.Append(transform.DOScale(1f, 0.1f));

        seq.Play().OnComplete(() =>
        {
            foreach (Button btn in bList)
            {
                btn.interactable = true;
            }

        });
    }

    public void Hide()
    {

        foreach (Button btn in bList)
        {
            btn.interactable = false;
        }

        var seq = DOTween.Sequence();

        transform.localScale = Vector3.one * 0.2f;

        seq.Append(transform.DOScale(1.1f, 0.1f));
        seq.Append(transform.DOScale(0.2f, 0.2f));

        // OnComplete �� seq �� ������ �ִϸ��̼��� �÷��̰� �Ϸ�Ǹ�
        // { } �ȿ� �ִ� �ڵ尡 ����ȴٴ� �ǹ��Դϴ�.
        // ���⼭�� �ݱ� �ִϸ��̼��� �Ϸ�� �� ��ÿ�� ��Ȱ��ȭ �մϴ�.
        seq.Play().OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void FastHid()
    {
        transform.localScale = new Vector3(0.2f, 0.2f);
        foreach (Button btn in bList)
        {
            btn.interactable = false;
        }
        gameObject.SetActive(false);
    }



}
