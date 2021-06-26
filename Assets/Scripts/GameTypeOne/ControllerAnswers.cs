using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Galchonok
{
    public class ControllerAnswers
    {
        private GameObject _area, _prefabGreen;
        private BookRound _bookRound;
        private UnityAction<bool> _callback;
        private int _answerCount;
        private LibraryOne _library;

        public ControllerAnswers(GameAsettings settings, GameObject area, LibraryOne library, UnityAction<bool> callback)
        {
            _area = area;
            _prefabGreen = settings.Prefabs[1]; // green
            _answerCount = settings.Answers;
            _callback = callback;
            _library = library;
        }

        public void Set(BookRound round)
        {
            _area.transform.Destroy();
            foreach (var answer in round.Answers)
            {
                // return new RoundTypeOne(bookIndex, chapterIndex, answers);
                bool correct = ( round.Book == answer.Book && round.Chapter == answer.Chapter );
                string title = _library.Book[answer.Book][answer.Chapter].Answers[answer.Answer];
                _area.transform.Attach(title, _prefabGreen.SetNewColor(rgb.White))
                    .GetOrAddComponent<Button>()
                    .onClick.AddListener(() => _callback(correct));
            }
        }
    }
}