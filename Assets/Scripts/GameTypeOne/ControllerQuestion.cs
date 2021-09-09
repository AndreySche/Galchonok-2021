using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Galchonok
{
    public class ControllerQuestion
    {
        private LibraryOne _library;
        private QuestionAnime _anime;

        public ControllerQuestion(TypeOneSettings settings, GameObject area, LibraryOne library, Image imageTarget )
        {
            _library = library;
            _anime = new QuestionAnime(imageTarget, settings.images, area.GetComponentInChildren<Text>());
        }

        public void Set(ChapterBook chapter, int gameId)
        {
            ChapterA chap = _library.Book[chapter.Book][chapter.Chapter];
            string question = gameId == 0 ? $"{chap.Question}?" : chap.Answers[chapter.Answer];
            _anime.Anime(chapter.Book, question.ToUpper());
        }
    }
}