using UnityEngine;
using UnityEngine.UI;

namespace TypeOne
{
    public class Questions
    {
        private LibraryOne _library;
        private QuestionAnime _anime;

        public Questions(Settings settings, GameObject area, LibraryOne library, Image imageTarget )
        {
            _library = library;
            _anime = new QuestionAnime(imageTarget, settings.Images, area.GetComponentInChildren<Text>());
        }

        public void Set(ChapterBook chapter, int gameId)
        {
            ChapterA chap = _library.Book[chapter.Book][chapter.Chapter];
            string question = gameId == 0 ? $"{chap.Question}?" : chap.Answers[chapter.Answer];
            _anime.Anime(chapter.Book, question.ToUpper());
        }
    }
}