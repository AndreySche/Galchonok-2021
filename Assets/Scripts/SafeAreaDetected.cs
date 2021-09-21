using UnityEngine;

class SafeAreaDetected
{
    private RectTransform _panel;
    private Rect _lastSafeArea = new Rect(0, 0, 0, 0);
    [HideInInspector] public bool hole = false;

    public SafeAreaDetected(RectTransform panel)
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate; // 120;
        _panel = panel;
    }

    public void Update()
    {
        Rect safeArea = Screen.safeArea;
        if (safeArea != _lastSafeArea) ApplySafeArea(safeArea);
    }

    private void ApplySafeArea(Rect rect)
    {
        _lastSafeArea = rect;
        if (rect.y <= 0) return;

        hole = true;
        Vector2 anchorMin = rect.position;
        Vector2 anchorMax = anchorMin + rect.size;

#if UNITY_IPHONE || UNITY_EDITOR
        // iPhone down plus
        var plus = anchorMin.y / 4f;
        anchorMin.y -= plus;
        anchorMax.y += plus;
#endif

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        //anchorMin.y = 0f; // iPhone: safeArea Down = 0;

        _panel.anchorMin = anchorMin;
        _panel.anchorMax = anchorMax;

        Debug.LogFormat(
            $"New safe area applied: x={rect.x}, y={rect.y}, w={rect.width}, h={rect.height} on full extents w={Screen.width}, h={Screen.height}");
    }
}

public static partial class Unilites
{
    public static GameObject IphoneMove(this GameObject gameObject, bool hole)
    {
#if UNITY_IPHONE || UNITY_EDITOR
        //var hole = GetComponentInParent<Main>()._safeArea.hole;
        if (!hole) return gameObject;

        //var move = _backButton.GetComponent<RectTransform>();
        var move = gameObject.GetComponent<RectTransform>();
        move.offsetMin += new Vector2(0, 60);
        move.offsetMax -= new Vector2(0, -60);
        return gameObject;
#endif
        return null;
    }
}