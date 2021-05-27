using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Galchonok
{
    class PageSwitch
    {
        private Curtain _curtain;
        private Transform _area;

        public PageSwitch(Image curtain, Transform area)
        {
            _curtain = new Curtain(curtain);
            _area = area;
        }

        public void SplashShow() => _curtain.View(true);
        public void SplashHide() => _curtain.View(false);
    }
}