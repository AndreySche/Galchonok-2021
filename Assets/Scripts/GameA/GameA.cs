using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

namespace Galchonok
{
    public class GameA : PageInit
    {
        [SerializeField] private GameAsettings _settings;
        [SerializeField] private Text _questionField;
        [SerializeField] private GameObject _answerArea;
        [SerializeField] private ProgressBarView _progressBar;
        [SerializeField] private List<GameObject> _nextPrew;

        private Sequence _sequence;
        private GameAview _view;
        private LibrarionOne _librarion;
        private HistoryToBook _book;
        
        private void Start()
        {
            _view = new GameAview( _settings, _questionField, _answerArea );
            _librarion = new LibrarionOne( _settings );
            _progressBar.GetComponent<ProgressBarView>().Init( _settings );
            
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

        private void Click(int index)
        {
            bool correct = _book.Answers[index].BookId == _book.CorrectBook;
            _librarion.AddClickToHistory(index);
            _progressBar.SetColor( correct );
            if( correct) Next(true);
        }
    }
}