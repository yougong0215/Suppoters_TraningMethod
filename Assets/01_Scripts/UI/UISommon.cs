using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;
using Cinemachine;
using UnityEngine.Video;

public class UISommon : MonoBehaviour
{
    public List<Button> bList = new();
    public VideoPlayer pl;
    private void Awake()
    {
        bList = transform.GetComponentsInChildren<Button>().ToList();
        foreach(Button btn in bList)
        {
            btn.interactable = false;
        }

        if(pl)
        pl.Stop();
    }
    void Start()
    {
        DOTween.Init();
        // transform 의 scale 값을 모두 0.1f로 변경합니다.
        transform.localScale = Vector3.one * 0.1f;
        // 객체를 비활성화 합니다.
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);


        if (pl)
            pl.Play();

        // DOTween 함수를 차례대로 수행하게 해줍니다.
        var seq = DOTween.Sequence();

        // DOScale 의 첫 번째 파라미터는 목표 Scale 값, 두 번째는 시간입니다.
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
        if (pl)
            pl.Stop();
        foreach (Button btn in bList)
        {
            btn.interactable = false;
        }

        var seq = DOTween.Sequence();

        transform.localScale = Vector3.one * 0.2f;

        seq.Append(transform.DOScale(1.1f, 0.1f));
        seq.Append(transform.DOScale(0.2f, 0.2f));

        // OnComplete 는 seq 에 설정한 애니메이션의 플레이가 완료되면
        // { } 안에 있는 코드가 수행된다는 의미입니다.
        // 여기서는 닫기 애니메이션이 완료된 후 객첼르 비활성화 합니다.
        seq.Play().OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void FastHid()
    {
        if (pl)
            pl.Stop();
        transform.localScale = new Vector3(0.2f, 0.2f);
        foreach (Button btn in bList)
        {
            btn.interactable = false;
        }
        gameObject.SetActive(false);
    }



}
