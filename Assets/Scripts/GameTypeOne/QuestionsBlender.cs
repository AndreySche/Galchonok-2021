using System.Collections.Generic;
using UnityEngine;

namespace Galchonok
{
    public class QuestionsBlender
    {
        private PullResort _questionResort, _questionResortForAnswer;
        private List<PullResort> _answerResort;
        private List<HistoryQuestion> _history;
        //private System.Random _rnd = new System.Random();
        int _current;

        public QuestionsBlender(int questionCount, int answerCount, List<List<QuestionA>> _pack)
        {
            int count = _pack.Count;
            _current = questionCount;
            _questionResort = new PullResort(count);
            _questionResortForAnswer = new PullResort(count);
            _answerResort = new List<PullResort>();
            for (int i = 0; i < count; i++)
            {
                _answerResort.Add(new PullResort(_pack[i][0].Answer.Count));
            }

            // create History --
            _history = new List<HistoryQuestion>();
            for (int i = 0; i < questionCount; i++)
            {
                int questionId = _questionResort.Next();
                int randomVerbations = Random.Range(0, _pack[questionId].Count - 1);

                var list = GeneratorIdArray(questionId, randomVerbations, answerCount);
                _history.Add(new HistoryQuestion(questionId, randomVerbations, list));
            }
        }

        private List<HistoryAnswer> GeneratorIdArray(int questionId, int arrayId, int answerCount)
        {
            List<HistoryAnswer> list = new List<HistoryAnswer>() { new HistoryAnswer(questionId, _answerResort[questionId].Next()) };

            for (int i = 1; i < answerCount; i++)
            {
                int qId = _questionResortForAnswer.Next(questionId);
                int aId = _answerResort[qId].Next();
                list.Add(new HistoryAnswer(qId, aId));
            }

            return list.RandomList(list.Count);
        }

        public HistoryQuestion HistoryNext()
        {
            if (_current < 1) return null;
            return _history[--_current];
        }

        public string HistoryJson() { return JsonHelper.ToJson(_history); }

        //public void DebugHistory()
        //{
        //    foreach (var child in _history)
        //    {
        //        string q = $"question: {child.QuestionId}, {child.ArrayId}\n";
        //        string a = $"answers: ";
        //        foreach (var line in child.AnswersId) a += $"{line.QuestionId}, {line.AnswerId} | ";
        //        Debug.Log(q + a);
        //    }
        //}
    }
}