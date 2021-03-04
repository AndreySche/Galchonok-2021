using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Galchonok
{
    public class QuestionController
    {
        Transform _areaQuestion, _areaAnswer;
        GameObject _prefabGreen, _prefabOrange;
        ProgressBarA _progressBar;
        LibrarionA _librarion;
        GameA _parent;
        GameB _parentB;
        List<bool> _clickSaver = new List<bool>();

        public QuestionController(
            Transform areaQuestion, Transform areaAnswer,
            GameObject prefabGreen, GameObject prefabOrange,
            ProgressBarA progressBar, LibrarionA librarion, GameA parent, GameB parentB)
        {
            _areaQuestion = areaQuestion;
            _areaAnswer = areaAnswer;
            _prefabGreen = prefabGreen;
            _prefabOrange = prefabOrange;

            _progressBar = progressBar;
            _librarion = librarion;
            _parent = parent;
            _parentB = parentB;
        }

        public bool Next()
        {
            int count = _librarion._countAnswer;
            QuestionA question = _parent == null ? _librarion.NextRevers() : _librarion.Next() ;
            if (question == null)
            {
                if(_parent == null) _parentB._controller.OpenResult(_clickSaver);
                else _parent._controller.OpenResult(_clickSaver);
                return false;
            }

            _areaQuestion.Destroy()
                .Attach($"{ question.Question[0] }?", _prefabOrange);

            _areaAnswer.Destroy();
            PullResort _indexRandom = new PullResort(count);
            for (int i = 0; i < count; i++)
            {
                int number = _indexRandom.Next();
                _areaAnswer.Attach(question.Answer[0][number], _prefabGreen).GetOrAddComponent<Button>()
                            .onClick.AddListener(() => ClickAnswer(number == 0));
            }
            return true;
        }

        void ClickAnswer(bool correct)
        {
            if (_parent == null) _parentB._controller.beethoven.Click(correct);
            else _parent._controller.beethoven.Click(correct);

            _progressBar.Set(correct);
            _clickSaver.Add(correct);
            Next();
        }
    }
}
