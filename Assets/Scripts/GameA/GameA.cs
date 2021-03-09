using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Galchonok
{
    public class GameA : PageInit
    {
        readonly int _countAnswers = 4;
        readonly int _countQuestions = 20;
        [SerializeField] GameObject _prefabGreen = null, _prefabOrange = null;
        [SerializeField] Transform _areaQuestion = null, _areaAnswer = null;
        [SerializeField] Transform _areaProgressBar = null;

        LibrarionA _librarion;
        ProgressBarA _progressBar;

        void Awake()
        {
            _librarion = new LibrarionA(_countQuestions, _countAnswers);
            _progressBar = new ProgressBarA(_countQuestions, _areaProgressBar);
            Next();
        }

        private void Next()
        {
            HistoryToQuestionPull questionPull = _librarion.Next();
            if (questionPull == null)
            {
                _controller.OpenResult(_librarion.clickSaver);
                return;
            }

            _areaQuestion.Destroy().Attach($"{ questionPull.Question.ToUpper() }?", _prefabOrange);
            _areaAnswer.Destroy();
            for (int i = 0; i < _countAnswers; i++)
            {
                bool correct = questionPull.Correct == i;
                int number = i;
                _areaAnswer.Attach(questionPull.Answers[i], _prefabGreen).GetOrAddComponent<Button>()
                            .onClick.AddListener(() => ClickAnswer(correct, number));
            }
        }

        void ClickAnswer(bool correct, int click)
        {
            _controller.beethoven.Click(correct);
            _progressBar.Set(correct);
            _librarion.ClickSaver(correct, click);
            Next();
        }
    }
}
