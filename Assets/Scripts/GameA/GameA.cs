using UnityEngine;

namespace Galchonok
{
    public class GameA : PageInit
    {
        #region Variables
        readonly int _countAnswers = 4;
        readonly int _countQuestions = 20;
        [SerializeField] GameObject _prefabGreen = null, _prefabOrange = null;
        [SerializeField] Transform _areaQuestion = null, _areaAnswer = null;
        [SerializeField] Transform _areaProgressBar = null;

        LibrarionA _librarion;
        ProgressBarA _progressBar;
        QuestionController _questionController;
        #endregion

        void Awake()
        {
            _librarion = new LibrarionA(_countQuestions, _countAnswers);
            _progressBar = new ProgressBarA(_countQuestions, _areaProgressBar);
            _questionController = new QuestionController(_areaQuestion, _areaAnswer, _prefabGreen, _prefabOrange, _progressBar, _librarion, this, null);
            _questionController.Next();
        }
    }
}
