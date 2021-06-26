using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Galchonok
{
    public class GameOne : PageInit
    {
        [SerializeField] private GameAsettings _settings;
        [SerializeField] private GameObject _questionField;
        [SerializeField] private GameObject _answersArea;
        [SerializeField] private ProgressBarView _progressBar;
        [SerializeField] private List<GameObject> _nextPrew;

        private ControllerQuestion _controllerQuestion;
        private ControllerAnswers _controllerAnswers;
        private LibrarionTypeOne _librarion;
        private LibraryOne _library;
        private int _step, _max;

        private void Start()
        {
            _step = 0;
            _max = _settings.Questions - 1;
            _library = new LibraryOne();
            _controllerQuestion = new ControllerQuestion(_settings, _questionField, _library);
            _controllerAnswers = new ControllerAnswers(_settings, _answersArea, _library, Click);
            _librarion = new LibrarionTypeOne(_settings, _library);
            _progressBar.Init(_settings);
            FirstStart();
        }

        private void Next()
        {
            BookRound round = _librarion.Next(_step);
            _controllerQuestion.Set(round);
            _controllerAnswers.Set(round, _step);
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
    }
}