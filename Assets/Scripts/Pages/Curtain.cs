using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Galchonok
{
    class Curtain
    {
        private Image _curtain;
        private float _duration = 0.5f;
        private Ease _easeIn = Ease.InBounce, _easeOut = Ease.OutBounce;

        public Curtain(Image curtain)
        {
            _curtain = curtain;
            _curtain.gameObject.SetActive(true);
        }

        public void View(bool show)
        {
            _curtain.DOKill();
            if (show) Show();
            else Hide();
        }

        private void Show() => _curtain.DOFade(0.0f, _duration).SetEase(_easeIn);
        private void Hide() => _curtain.DOFade(0.0f, _duration).SetEase(_easeOut);
    }
}
