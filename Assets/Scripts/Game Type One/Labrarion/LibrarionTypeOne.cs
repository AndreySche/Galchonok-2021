using System.Collections.Generic;
using UnityEngine;

namespace TypeOne
{
    public class LibrarionTypeOne
    {
        private List<ChapterBook> _history;

        public LibrarionTypeOne(Settings settings, LibraryOne library)
        {
            _history = new List<ChapterBook>();
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
                int currentAnswer = indexAnswer[currentBook].Next();

                List<AnswerRound> answers = new List<AnswerRound>();
                answers.Add(new AnswerRound(currentBook, currentChapter, currentAnswer));

                for (int j = 1; j < settings.Answers; j++)
                {
                    int bookAnswersIndex = indexBook.Next(currentBook);
                    int chapterAnswersIndex = indexChapter[bookAnswersIndex].Next();
                    answers.Add(new AnswerRound(bookAnswersIndex, chapterAnswersIndex, indexAnswer[bookAnswersIndex].Next()));
                }

                _history.Add(new ChapterBook(currentBook, currentChapter, currentAnswer, answers.RandomList() ));
            }

            //DebugJson(_history);
        }

        public ChapterBook Next(int current)
        {
            int bookIndex = _history[current].Book;
            int chapterIndex = _history[current].Chapter;
            int answerIndex = _history[current].Answer;
            List<AnswerRound> answers = _history[current].Answers;

            return new ChapterBook(bookIndex, chapterIndex, answerIndex, answers);
        }

        private void DebugJson(List<ChapterBook> game)
        {
            foreach (var round in game)
            {
                string answers = "";
                foreach (var answerChild in round.Answers)
                {
                    answers += $"\n<color='#b3e5fc'>Book: {answerChild.Book}; ";
                    answers += $"Chapter: {answerChild.Chapter}; ";
                    answers += $"Answer: {answerChild.Answer}; </color>";
                }

                Debug.Log($"<b>Book: {round.Book}; Chapter: {round.Chapter};</b> Answer: {round.Answer}\nAnswers: {answers}");
            }
        }
    }
}