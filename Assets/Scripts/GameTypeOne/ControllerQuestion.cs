using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Galchonok
{
    public class ControllerQuestion
    {
        private Text _area;
        private Ease _ease;
        private LibraryOne _library;

        public ControllerQuestion(TypeOneSettings settings, GameObject area, LibraryOne library )
        {
            _area = area.GetComponentInChildren<Text>();
            _ease = settings.QuestionEase;
            _library = library;
        }

        public void Set(ChapterBook chapter)
        {
            string question = $"{_library.Book[chapter.Book][chapter.Chapter].Question.ToUpper()}?";
            float duration = 0.5f;

            Sequence sequence = DOTween.Sequence();
            sequence.Insert(0, _area.DOFade(0.1f, duration / 2.5f).SetEase(Ease.Linear));
            sequence.Append(_area.DOFade(1f, duration / 2.5f).SetEase(Ease.Linear));
            sequence.Insert(0, _area.DOText(question, duration).SetEase(_ease));
            sequence.OnComplete(() => sequence = null);
        }
    }
}