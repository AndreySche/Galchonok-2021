using UnityEngine;
using UnityEngine.UI;
using Pages;

class Main : MonoBehaviour
{
    [SerializeField] private Image _curtain;
    [SerializeField] private Transform _area;
    [SerializeField] private Beethoven _beethoven;
    public SafeAreaDetected _safeArea;
    private PageSwitch _pageSwitch;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();

        _safeArea = new SafeAreaDetected(GetComponent<RectTransform>());
        _pageSwitch = new PageSwitch(_curtain, _area, _beethoven);
        _pageSwitch.LoadPage(Page.Menu);
    }

    private void Update()
    {
        _safeArea.Update();
    }
}