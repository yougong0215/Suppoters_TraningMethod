using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public static class MinimapClickHandler
{


    public static Vector2 ConvertMinimapToMap(Vector2 minimapCoord, RectTransform rectTransform, Camera mainCamera)
    {
        // RawImage ũ��� ī�޶� ����Ʈ ũ�⸦ ���Ͽ� ������ ���� ���
        float scaleX = rectTransform.rect.width / Screen.width;
        float scaleY = rectTransform.rect.height / Screen.height;

        // RawImage ������ Ŭ�� ��ġ�� ��ȯ
        Vector2 localCursor;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, minimapCoord, null, out localCursor))
            return Vector2.zero;

        // RawImage ��ǥ�� Ŭ�� ��ġ�� ��ȯ
        Vector2 normalizedCursor = new Vector2((localCursor.x + rectTransform.pivot.x * rectTransform.rect.width) / rectTransform.rect.width,
                                               (localCursor.y + rectTransform.pivot.y * rectTransform.rect.height) / rectTransform.rect.height);

        // ī�޶� ����Ʈ�� �������� Ŭ�� ��ġ�� ���� ���� ���������� ��ȯ
        Vector2 mapCoord = new Vector2(normalizedCursor.x * scaleX, normalizedCursor.y * scaleY);
        mapCoord = mainCamera.ViewportToWorldPoint(new Vector3(mapCoord.x, mapCoord.y, mainCamera.nearClipPlane));

        return mapCoord;
    }
}