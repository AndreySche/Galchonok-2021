using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Galchonok
{
    public class GameA : PageInit
    {
        [SerializeField] int _countAnswers = 4;
        [SerializeField] int _countQuestions = 20;
        [SerializeField] bool _right = true;
        [SerializeField] GameObject _prefabGreen = null, _prefabOrange = null;
        [SerializeField] Transform _areaQuestion = null, _areaAnswer = null, _areaProgressBar = null;
        [SerializeField] List<GameObject> _nextPrew = null;

        [SerializeField] private GameObject _square, _border;

        LibrarionOne _librarion;
        LibrarionTwo _librarion2;
        ProgressBar _progressBar;

        void Awake()
        {
            if (_right) _librarion = new LibrarionOne(_countQuestions, _countAnswers);
            else _librarion2 = new LibrarionTwo(_countQuestions, _countAnswers);

            _progressBar = new ProgressBar(_countQuestions, _areaProgressBar, _square, _border);
            _nextPrew[0].GetOrAddComponent<Button>().onClick.AddListener(() => Next(false));
            _nextPrew[1].GetOrAddComponent<Button>().onClick.AddListener(() => Next(true));
            Next(true);
        }

        private void Next(bool next)
        {
            HistoryToBook book = _right ? _librarion.Next(next) : _librarion2.Next(next);
            if (book == null) return;

            bool[] click = SetClickButton(_countAnswers, book.Click);
            _progressBar.SetBorder(book.Index);
            string question = _right ? $"{ book.Question.ToUpper() }?" : $"{ book.Question }";

            _areaQuestion.Destroy()
                .Attach(question, _prefabOrange);
            _areaAnswer.Destroy();
            for (int i = 0; i < _countAnswers; i++)
            {
                bool correct = book.CorrectBook == book.Answers[i].BookId;
                int index = book.Index;
                int buttonIndex = i;
                rgb buttonColor = click[i] ? (correct ? rgb.LightGreen : rgb.LightYellow) : rgb.White;
                _areaAnswer.Attach(book.Answers[i].Word, _prefabGreen.SetNewColor(buttonColor))
                    .GetOrAddComponent<Button>()
                    .onClick.AddListener(() => ClickAnswer(correct, index, buttonIndex));
            }
        }

        void ClickAnswer(bool correct, int index, int buttonIndex)
        {
            if (_right) _librarion.AddClickToHistory(buttonIndex);
            else _librarion2.AddClickToHistory(buttonIndex);

            _progressBar.SetColor(index, correct);
            Next(true);
            if (!correct) Next(false);
        }

        private bool[] SetClickButton(int count, List<int> list)
        {
            bool[] numbers = new bool[count];
            foreach (var child in list)
            {
                numbers[child] = true;
            }
            return numbers;
        }
    }
}
