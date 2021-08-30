using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Galchonok
{
    public class BounceButton : Button
    {
        private Ease _easeDown = Ease.OutBounce;
        private Ease _easeUp = Ease.OutQuint;
        private float _duration = 0.5f;
        private float _strength = 20.0f;
        //[SerializeField] private BounceButtonSettings _data;

        private float _startPosition = 0;
        private RectTransform _rectTransform;
        private Sequence _sequence;

        protected override void OnEnable()
        {
            base.OnEnable();
            _rectTransform = GetComponent<RectTransform>();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if ( !IsInteractable() ) return;
            base.OnPointerDown(eventData);
            _startPosition = SetPosition();
            _sequence = DOTween.Sequence();
            _sequence.Append( _rectTransform.DOAnchorPosY(_startPosition + _strength, _duration/2).SetEase(_easeUp) );
            _sequence.Append( _rectTransform.DOAnchorPosY(_startPosition, _duration).SetEase(_easeDown) );
            _sequence.OnComplete( () => _sequence = null );
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            _sequence.Kill();
            _sequence = null;
        }

        private float SetPosition() => _startPosition == 0 ? _rectTransform.anchoredPosition.y : _startPosition ;


        //public override void OnPointerExit(PointerEventData eventData)
        //public override void OnPointerClick(PointerEventData eventData)
    }
}