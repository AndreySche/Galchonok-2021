using UnityEngine;
using UnityEngine.UI;

namespace Galchonok
{
    class Main : MonoBehaviour
    {
        [SerializeField] private Image _curtain;
        [SerializeField] private Transform _area;
        private PageSwitch _pageSwitch;
        private SafeAreaDetected _safeArea;

        private void Start()
        {
            Application.targetFrameRate = 120;
            _safeArea = new SafeAreaDetected( GetComponent<RectTransform>() );
            
            _pageSwitch = new PageSwitch(_curtain, _area);
            _pageSwitch.LoadPage(Pages.Logo);
        }

        private void Update()
        {
            _safeArea.Update();
        }
    }
}