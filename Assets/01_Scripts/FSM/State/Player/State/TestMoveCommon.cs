using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveCommon : CommonAction
{
    protected override void OnEndFunc()
    {
        Debug.Log("�� ^^�� �̰Ե�");

    }

    protected override void OnEventFunc()
    {
        Debug.Log("�� ^^�� �ߵ�");
        //�غ��� Effect ���� ����
    }

    protected override void OnUpdateFunc()
    {
        Debug.Log("�� �����̺v");
    }
}
