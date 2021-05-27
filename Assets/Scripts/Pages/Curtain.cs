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
            Off(false);
        }

        public void Show(UnityAction callBack)
        {
            Off(false);
            _curtain.DOFade(1.0f, _duration).SetEase(_easeIn).OnComplete(()=>callBack());
            //.SetAutoKill()
        }

        public void Hide()
        {
            _curtain.DOFade(0.0f, _duration).SetEase(_easeOut).OnComplete( () => Off(true) );
        }

        private void Off(bool off) => _curtain.gameObject.SetActive(!off);
    }
}
