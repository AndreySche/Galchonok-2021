﻿using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Galchonok
{
    public class GameAview
    {
        private Ease _ease;
        private Text _questionField;
        private int _answerCount;
        private GameObject _answerArea, _prefabGreen;

        public GameAview( GameAsettings settings, Text questionField, GameObject answerArea )
        {
            _ease = settings.QuestionEase;
            _questionField = questionField;
            _answerCount = settings.Answers;
            _answerArea = answerArea;
            _prefabGreen = settings.Prefabs[1]; // green
        }

        public void SetQuestion(HistoryToBook book)
        {
            string question = $"{book.Question.ToUpper()}?";
            float duration = 0.5f;

            Sequence sequence = DOTween.Sequence();
            sequence.Insert(0, _questionField.DOFade(0.1f, duration / 2.5f).SetEase(Ease.Linear));
            sequence.Append(_questionField.DOFade(1f, duration / 2.5f).SetEase(Ease.Linear));
            sequence.Insert(0, _questionField.DOText(question, duration).SetEase(_ease));
            sequence.OnComplete(() => sequence = null);
        }

        public void SetAnswers(HistoryToBook book, UnityAction<int> callback)
        {
            for (int i = 0; i < _answerCount; i++)
            {
                int index = i;
                rgb buttonColor = rgb.White;
                _answerArea.transform.Attach(book.Answers[i].Word, _prefabGreen.SetNewColor(buttonColor))
                    .GetOrAddComponent<Button>()
                    .onClick.AddListener( () => callback(index) );
            }
        }
    }
}