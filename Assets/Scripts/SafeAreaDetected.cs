using UnityEngine;

class SafeAreaDetected : MonoBehaviour
{
    RectTransform Panel;
    Rect LastSafeArea = new Rect(0, 0, 0, 0);

    private void Awake()
    {
        Application.targetFrameRate = 120;
        Panel = GetComponent<RectTransform>();
        Refresh();
    }

    void Update() { Refresh(); }

    void Refresh()
    {
        return;
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

        anchorMin.y = 0f; // iPhone: safeArea Down = 0;

        Panel.anchorMin = anchorMin;
        Panel.anchorMax = anchorMax;

<<<<<<< Updated upstream
        Debug.LogFormat($"New safe area applied: x={r.x}, y={r.y}, w={r.width}, h={r.height} on full extents w={Screen.width}, h={Screen.height}");
=======
        //Debug.LogFormat($"New safe area applied: x={rect.x}, y={rect.y}, w={rect.width}, h={rect.height} on full extents w={Screen.width}, h={Screen.height}");
>>>>>>> Stashed changes
    }
}
