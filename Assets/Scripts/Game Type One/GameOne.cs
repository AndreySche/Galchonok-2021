using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;

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
        private Questions _questions;
        private Answers _answers;
        private LibrarionTypeOne _librarion;
        private LibraryOne _library;
        private int _step, _max;
        private int _gameId;
        private Button _back;
        #endregion
        
        public void Init(UnityAction callBack, Beethoven beethoven, int gameId = 0)
        {
            _gameId = gameId;
            _beethoven = beethoven;
            _back = _backButton.IphoneMove(GetComponentInParent<Main>()._safeArea.hole).GetOrAddComponent<Button>();
            _back.onClick.AddListener(() =>
            {
                Dispose();
                callBack();
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

        private void Next()
        {
            ChapterBook chapter = _librarion.Next(_step);
            _questions.Set(chapter, _gameId);
            _answers.Set(chapter, _gameId, _step);
            _progressBar.SetBorder(_step);
        }

        private void Next(bool next)
        {
            _step += next ? 1 : -1;
            if (_step > _max) _step = _max;
            else if (_step < 0) _step = 0;
            Next();
        }

        private void Click(bool correct)
        {
            if(_settings.Mp3 == 1) _beethoven.Result(correct);
            _progressBar.SetColor(correct);
            if (correct) Next(true);
            else Next();
        }

        private void FirstStart()
        {
            _nextPrew[0].GetOrAddComponent<Button>().onClick.AddListener(() => Next(false));
            _nextPrew[1].GetOrAddComponent<Button>().onClick.AddListener(() => Next(true));
            Next();
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