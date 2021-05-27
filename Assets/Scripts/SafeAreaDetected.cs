using UnityEngine;

class SafeAreaDetected
{
    private RectTransform Panel;
    private Rect LastSafeArea = new Rect(0, 0, 0, 0);

    public SafeAreaDetected(RectTransform panel)
    {
        Panel = panel;
        Update();
    }
    public void Update()
    {
        Rect safeArea = Screen.safeArea;
        if (safeArea != LastSafeArea) ApplySafeArea(safeArea);
    }

    private void ApplySafeArea(Rect r)
    {
        LastSafeArea = r;
        Vector2 anchorMin = r.position;
        //anchorMin.y *= 1.6f; // iPhone: safeArea Up Plus 
        Vector2 anchorMax = anchorMin + r.size;
        //anchorMin.y *= 0.3f; // iPhone: safeArea Down Plus 

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        //anchorMin.y = 0f; // iPhone: safeArea Down = 0;

        Panel.anchorMin = anchorMin;
        Panel.anchorMax = anchorMax;

        Debug.LogFormat($"New safe area applied: x={r.x}, y={r.y}, w={r.width}, h={r.height} on full extents w={Screen.width}, h={Screen.height}");
    }
}
