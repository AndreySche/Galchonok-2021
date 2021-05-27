using UnityEngine;
using UnityEngine.UI;

namespace Galchonok
{
    class Main : MonoBehaviour
    {
        [SerializeField] private Image _curtain;
        [SerializeField] private Transform _area;
        private PageSwitch _pageSwitch;

        private void Awake()
        {
            Application.targetFrameRate = 120;
            _pageSwitch = new PageSwitch(_curtain, _area);
            SafeAreaDetected safe = new SafeAreaDetected( GetComponent<RectTransform>() );
            
            Invoke( "SplashShow", 1f );
            Invoke( "SplashHide", 3f );
        }

        private void SplashShow() => _pageSwitch.SplashShow();
        private void SplashHide() => _pageSwitch.SplashHide();
    }
}