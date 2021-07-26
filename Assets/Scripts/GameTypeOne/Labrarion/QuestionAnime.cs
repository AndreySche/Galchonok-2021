using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Galchonok
{
    public class QuestionAnime
    {
        private Text _area;
        private Image _target;
        private List<Sprite> _imagesList;

        public QuestionAnime(Image target, List<Sprite> imagesList, Text area)
        {
            _area = area;
            _target = target;
            _imagesList = imagesList;
        }

        public void Anime(int bookId, string question)
        {
            float duration = 0.5f;
            AnimeQuestion(duration, question);
            AnimeImage(duration, bookId);
        }

        private void AnimeImage(float duration, int bookId)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Insert(0, _target.DOFade(0.1f, duration / 2.5f).SetEase(Ease.Linear));
            sequence.OnComplete(() =>
                {
                    _target.sprite = _imagesList[bookId];
                    sequence.Append(_target.DOFade(1f, duration / 2.5f).SetEase(Ease.Linear));
                    sequence.OnComplete(() => sequence = null);
                }
            );
        }
        
        private void AnimeQuestion(float duration, string question)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Insert(0, _area.DOFade(0.1f, duration / 2.5f).SetEase(Ease.Linear));
            sequence.Append(_area.DOFade(1f, duration / 2.5f).SetEase(Ease.Linear));
            sequence.Insert(0, _area.DOText(question, duration).SetEase(Ease.InOutQuint));
            sequence.OnComplete(() => sequence = null);
        }
    }
}