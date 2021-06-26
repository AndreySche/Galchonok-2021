using System.Collections.Generic;
using UnityEngine;

namespace Galchonok
{
    public class LibrarionTypeOne
    {
        private List<BookRound> _history;

        public LibrarionTypeOne(GameAsettings settings, LibraryOne library)
        {
            _history = new List<BookRound>();
            RandomRange book = new RandomRange(library.Book.Count);
            RandomRange indexBook = new RandomRange(library.Book.Count);

            List<RandomRange> indexChapter = new List<RandomRange>();
            List<RandomRange> indexAnswer = new List<RandomRange>();
            for (int i = 0; i < library.Book.Count; i++)
            {
                indexChapter.Add(new RandomRange(library.Book[i].Count));
                indexAnswer.Add(new RandomRange(library.Book[i][0].Answers.Count));
            }

            for (int i = 0; i < settings.Questions; i++)
            {
                int currentBook = book.Next();
                int currentChapter = indexChapter[currentBook].Next();

                List<AnswerRound> answers = new List<AnswerRound>();
                answers.Add(new AnswerRound(currentBook, currentChapter, indexAnswer[currentBook].Next()));

                for (int j = 1; j < settings.Answers; j++)
                {
                    int bookAnswersIndex = indexBook.Next(currentBook);
                    int chapterAnswersIndex = indexChapter[bookAnswersIndex].Next();
                    answers.Add(new AnswerRound(bookAnswersIndex, chapterAnswersIndex, indexAnswer[bookAnswersIndex].Next()));
                }

                _history.Add(new BookRound(currentBook, currentChapter, answers.RandomList() ));
            }

            //DebugJson(_history);
        }

        public BookRound Next(int current)
        {
            int bookIndex = _history[current].Book;
            int chapterIndex = _history[current].Chapter;
            List<AnswerRound> answers = _history[current].Answers;

            return new BookRound(bookIndex, chapterIndex, answers);
        }

        private void DebugJson(List<BookRound> game)
        {
            foreach (var round in game)
            {
                List<int> answrs = new List<int>();

                string answers = "";
                foreach (var answerChild in round.Answers)
                {
                    answers += $"\n<color='#b3e5fc'>Book: {answerChild.Book}; ";
                    answers += $"Chapter: {answerChild.Chapter}; ";
                    answers += $"Answer: {answerChild.Answer}; </color>";
                }

                Debug.Log($"<b>Book: {round.Book}; Chapter: {round.Chapter};</b> Answers: {answers}");
            }
        }
    }
}