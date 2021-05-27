using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

namespace Galchonok
{
    class Curtain
    {
        private Image _curtain;
        private float _duration = 0.5f;
        private Ease _easeIn = Ease.InQuart;
        private Ease _easeOut = Ease.OutQuart;

        public Curtain(Image curtain)
        {
            _curtain = curtain;
            _curtain.gameObject.SetActive(true);
        }

        public void Show(UnityAction callBack)
        {
            //.SetAutoKill()
            _curtain.DOFade(1.0f, _duration).SetEase(_easeIn).OnComplete(()=>callBack());
        }

        public void Hide()
        {
            _curtain.DOFade(0.0f, _duration).SetEase(_easeOut);
        }
    }
}
