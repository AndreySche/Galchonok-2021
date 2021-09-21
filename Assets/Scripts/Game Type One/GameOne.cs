using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using Pages;

namespace TypeOne
{
    public class GameOne : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Settings _settings;
        [SerializeField] private GameObject _questionField;
        [SerializeField] private GameObject _answersArea;
        [SerializeField] private ProgressBarView _progressBar;
        [SerializeField] private List<GameObject> _nextPrew;
        [SerializeField] private Image _imageTarget;
        [SerializeField] private GameObject _backButton;
        
        [HideInInspector] public Beethoven _beethoven;
        private PageSwitch _pageSwitch;
        private Questions _questions;
        private Answers _answers;
        private LibrarionTypeOne _librarion;
        private LibraryOne _library;
        private int _step, _max;
        private int _gameId;
        private Button _back;
        private int _alreadyAnsweredCount;
        #endregion
        
        //public void Init(UnityAction callBack, Beethoven beethoven, int gameId = 0)
        public void Init(PageSwitch pageSwitch, Beethoven beethoven, int gameId = 0)
        {
            _gameId = gameId;
            _beethoven = beethoven;
            _pageSwitch = pageSwitch;
            _alreadyAnsweredCount = 0;
            _back = _backButton.IphoneMove(GetComponentInParent<Main>()._safeArea.hole).GetOrAddComponent<Button>();
            _back.onClick.AddListener(() =>
            {
                Dispose();
                _pageSwitch.LoadPage(Page.Menu);
                //callBack();
            });
        }

        private void Start()
        {
            _step = 0;
            SetFromCookies();
            _max = _settings.Questions - 1;
            _library = new LibraryOne();
            _questions = new Questions(_settings, _questionField, _library, _imageTarget);
            _answers = new Answers(_settings, _answersArea, _library, Click);
            _librarion = new LibrarionTypeOne(_settings, _library);
            _progressBar.Init(_settings);
            FirstStart();
        }

        private void Set(bool answeredNext)
        {
            ChapterBook chapter = _librarion.Next(_step);
            
            //Already Answered
            
            bool alreadyAnswered = _answers.Set(chapter, _gameId, _step, answeredNext);
            if (!alreadyAnswered && answeredNext)
            {
                if (_alreadyAnsweredCount++ >= _max)
                {
                    Debug.Log("Game Over");
                    _pageSwitch.LoadPage(Page.YouWin);
                }
                else
                {
                    Debug.Log("GameOne Next()");
                    Next(true, true);
                    return;
                }
            }
            
            _questions.Set(chapter, _gameId);
            _progressBar.SetBorder(_step);
        }

        private void Next(bool next, bool answeredNext)
        {
            _step += next ? 1 : -1;
            if (_step > _max) _step = 0;
            else if (_step < 0) _step = _max;
            Set(answeredNext);
        }

        private void Click(bool correct)
        {
            if(_settings.Mp3 == 1) _beethoven.Result(correct);
            _progressBar.SetColor(correct);
            if (correct) Next(true, true);
            else Set(true);
        }

        private void FirstStart()
        {
            _nextPrew[0].GetOrAddComponent<Button>().onClick.AddListener(() => Next(false, false));
            _nextPrew[1].GetOrAddComponent<Button>().onClick.AddListener(() => Next(true, false));
            Set(false);
        }

        private void SetFromCookies()
        {
            _settings.Questions = Cookies.Get(Cookie.Questions, _settings.Questions);
            _settings.Answers = Cookies.Get(Cookie.Answers, _settings.Answers);
            _settings.Hint = Cookies.Get(Cookie.Hint, _settings.Hint);
            _settings.Mp3 = Cookies.Get(Cookie.Mp3, _settings.Mp3);
        }

        private void Dispose()
        {
            _back.onClick.RemoveAllListeners();
            //_back.interactable = false;
        }
    }
}