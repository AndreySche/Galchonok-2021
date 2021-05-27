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
            SafeAreaDetected safe = new SafeAreaDetected( GetComponent<RectTransform>() );
            
            _pageSwitch = new PageSwitch(_curtain, _area);
            _pageSwitch.LoadPage(Pages.Logo);
            //Invoke( "SplashShow", 1f );
            //Invoke( "SplashHide", 1f );
        }

        private void SplashShow() =>  _pageSwitch.LoadPage(Pages.Logo);
        //private void SplashShow() => _pageSwitch.LoadLogo();
        //private void SplashHide() => _pageSwitch.LoadPage(Pages.Menu);
    }
}