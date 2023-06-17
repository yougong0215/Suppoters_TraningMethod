using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public static class MinimapClickHandler
{


    public static Vector2 ConvertMinimapToMap(Vector2 minimapCoord, RectTransform rectTransform, Camera mainCamera)
    {
        // RawImage 크기와 카메라 뷰포트 크기를 비교하여 스케일 팩터 계산
        float scaleX = rectTransform.rect.width / Screen.width;
        float scaleY = rectTransform.rect.height / Screen.height;

        // RawImage 내에서 클릭 위치로 변환
        Vector2 localCursor;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, minimapCoord, null, out localCursor))
            return Vector2.zero;

        // RawImage 좌표를 클릭 위치로 변환
        Vector2 normalizedCursor = new Vector2((localCursor.x + rectTransform.pivot.x * rectTransform.rect.width) / rectTransform.rect.width,
                                               (localCursor.y + rectTransform.pivot.y * rectTransform.rect.height) / rectTransform.rect.height);

        // 카메라 뷰포트를 기준으로 클릭 위치를 실제 맵의 포지션으로 변환
        Vector2 mapCoord = new Vector2(normalizedCursor.x * scaleX, normalizedCursor.y * scaleY);
        mapCoord = mainCamera.ViewportToWorldPoint(new Vector3(mapCoord.x, mapCoord.y, mainCamera.nearClipPlane));

        return mapCoord;
    }
}