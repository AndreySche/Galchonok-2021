using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TypeOne
{
    public class Answers
    {
        private GameObject _area, _prefabGreen;
        private ChapterBook _chapterBook;
        private UnityAction<bool> _callback;
        private LibraryOne _library;
        private Dictionary<int, List<AnswerRound>> _clickHistory;
        private bool _hint;

        public Answers(Settings settings, GameObject area, LibraryOne library,
            UnityAction<bool> callback)
        {
            _area = area;
            _library = library;
            _callback = callback;
            _hint = settings.Hint == 1;
            _prefabGreen = settings.AnswerPrefab; // green
            _clickHistory = new Dictionary<int, List<AnswerRound>>();
        }

        public void Set(ChapterBook chapter, int gameId, int step)
        {
            _area.transform.Destroy();
            foreach (var answer in chapter.Answers)
            {
                bool correct = (chapter.Book == answer.Book && chapter.Chapter == answer.Chapter);
                var chap = _library.Book[answer.Book][answer.Chapter];
                string title = gameId == 1 ? chap.Question : chap.Answers[answer.Answer];
                rgb color = GetPrefabColor(step, correct, answer);
                _area.transform.Attach(title, _prefabGreen.SetNewColor(color))
                    .GetOrAddComponent<Button>()
                    .onClick.AddListener(() => Click(step, correct, answer));
            }
        }

        private rgb GetPrefabColor(int step, bool correct, AnswerRound answer)
        {
            if (!_clickHistory.ContainsKey(step)) _clickHistory[step] = new List<AnswerRound>();
            if (Verify(step, answer)) return correct ? rgb.Green : rgb.Red;

            return (correct && _hint ) ? rgb.Rose : rgb.White;
        }

        private void Click(int step, bool correct, AnswerRound answer)
        {
            if (!Verify(step, answer)) _clickHistory[step].Add(answer);
            _callback(correct);
        }

        private bool Verify(int step, AnswerRound answer)
        {
            foreach (var child in _clickHistory[step])
            {
                if (answer.Book == child.Book) return true;
            }

            return false;
        }
    }
}