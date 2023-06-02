using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveCommon : CommonAction
{
    protected override void OnEndFunc()
    {
        Debug.Log("와 ^^발 이게됨");

    }

    protected override void OnEventFunc()
    {
        Debug.Log("와 ^^발 잘됨");
        //해봤자 Effect 정도 넣음
    }

    protected override void OnUpdateFunc()
    {
        Debug.Log("와 업데이틍");
    }
}
