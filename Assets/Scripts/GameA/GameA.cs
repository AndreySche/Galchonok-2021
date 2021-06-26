using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Galchonok
{
    public class GameA : PageInit
    {
        [SerializeField] private GameAsettings _settings;
        [SerializeField] private Text _questionField;
        [SerializeField] private GameObject _answerArea;
        [SerializeField] private ProgressBarView _progressBar;
        [SerializeField] private List<GameObject> _nextPrew;

        private GameAview _view;
        private LibrarionOne _librarion;
        private HistoryToBook _book;

        private void Start()
        {
            _view = new GameAview(_settings, _questionField, _answerArea);
            _librarion = new LibrarionOne(_settings);
            _progressBar.Init(_settings);

            _nextPrew[0].GetOrAddComponent<Button>().onClick.AddListener(() => Next(false));
            _nextPrew[1].GetOrAddComponent<Button>().onClick.AddListener(() => Next(true));
            Next(true);
        }

        private void Next(bool next)
        {
            _book = _librarion.Next(next);
            _view.SetQuestion(_book);
            _answerArea.transform.Destroy();
            _view.SetAnswers(_book, Click);
            _progressBar.SetBorder(_book.Index);
        }

        private void Click(bool correct)
        {
            _progressBar.SetColor(correct);
            if (correct) Next(true);
        }
    }
}