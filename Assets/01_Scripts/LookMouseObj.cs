using UnityEngine;

public class LookMouseObj : MonoBehaviour
{
    void Update()
    {
        // ���콺�� ��ũ�� ��ǥ�� ������
        Vector3 mousePos = Input.mousePosition;

        // ���콺 ��ǥ�� ���� ��ǥ�� ��ȯ
        Vector3 worldMousePos = GameManager.Instance.Cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));

        // ī�޶� �Ǵ� ������Ʈ�� ���콺 ��ġ�� �ٶ󺸵��� ����

        worldMousePos.y = 0;
        transform.LookAt(worldMousePos);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }
}