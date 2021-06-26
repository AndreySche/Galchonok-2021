using UnityEngine;

class SafeAreaDetected
{
    private RectTransform _panel;
    private Rect _lastSafeArea = new Rect(0, 0, 0, 0);

    public SafeAreaDetected(RectTransform panel)
    {
        //Application.targetFrameRate = 120;
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
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
        Vector2 anchorMin = rect.position;
        //anchorMin.y *= 1.6f; // iPhone: safeArea Up Plus 
        Vector2 anchorMax = anchorMin + rect.size;
        //anchorMin.y *= 0.3f; // iPhone: safeArea Down Plus 

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        anchorMin.y = 0f; // iPhone: safeArea Down = 0;

        _panel.anchorMin = anchorMin;
        _panel.anchorMax = anchorMax;

        //Debug.LogFormat($"New safe area applied: x={rect.x}, y={rect.y}, w={rect.width}, h={rect.height} on full extents w={Screen.width}, h={Screen.height}");
    }
}
