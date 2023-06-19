using UnityEngine;

public class LookMouseObj : MonoBehaviour
{
    void Update()
    {
        // 마우스의 스크린 좌표를 가져옴
        Vector3 mousePos = Input.mousePosition;

        // 마우스 좌표를 월드 좌표로 변환
        Vector3 worldMousePos = GameManager.Instance.Cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));

        // 카메라 또는 오브젝트를 마우스 위치로 바라보도록 설정

        worldMousePos.y = 0;
        transform.LookAt(worldMousePos);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }
}