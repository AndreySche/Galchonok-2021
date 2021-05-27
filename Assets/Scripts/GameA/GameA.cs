using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

namespace Galchonok
{
    public class GameA : PageInit
    {
        [SerializeField] private int _countAnswers = 4;
        [SerializeField] private int _countQuestions = 20;
        [SerializeField] private bool _right = true;
        [SerializeField] private Text _questionField;
        [SerializeField] private GameObject _prefabGreen = null;
        [SerializeField] private Transform _areaAnswer = null, _areaProgressBar = null;
        [SerializeField] private List<GameObject> _nextPrew = null;
        [SerializeField] private Ease _ease = Ease.InOutQuint;

        [SerializeField] private GameObject _square, _border;

        private LibrarionOne _librarion;
        private LibrarionTwo _librarion2;
        private ProgressBar _progressBar;

        private void Start()
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
            string question = _right ? $"{ book.Question.ToUpper() }?" : $"{ FirstLetterToUp(book.Question) }";
            float duration = (float)question.Length / 20f;

            Sequence sequence = DOTween.Sequence();
            sequence.Insert( 0, _questionField.DOText(question, duration).SetEase(_ease) );
            sequence.Insert( 0, _questionField.DOFade(0.1f, duration/2).SetEase(Ease.Linear) );
            sequence.Append( _questionField.DOFade(1f, duration/2).SetEase(Ease.Linear) );
            sequence.OnComplete(() => sequence = null);
            
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
        
        private string FirstLetterToUp(string str) => char.ToUpper(str[0]) + str.Substring(1);
    }
}
