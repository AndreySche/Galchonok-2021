using System.Collections.Generic;
using UnityEngine;

namespace Galchonok
{
    public class LibrarionOne
    {
        private List<List<ChapterA>> _bookList;
        private List<HistoryBook> _history = new List<HistoryBook>();
        private PullResort _booksRandom, _booksRandomForAnswers;
        private List<PullResort> _chaptersRandom = new List<PullResort>();
        private List<List<PullResort>> _answersRandom = new List<List<PullResort>>();
        private LibA _libA = new LibA();
        private int _countQuestions, _countAnswers, _current;

        public LibrarionOne( GameAsettings settings )
        {
            _current = -1;
            _countQuestions = settings.Questions;
            _countAnswers = settings.Answers;
            _bookList = _libA.BookList;
            int countBook = _bookList.Count;
            _booksRandom = new PullResort(countBook);
            _booksRandomForAnswers = new PullResort(countBook);

            for (int book = 0; book < countBook; book++)
            {
                _chaptersRandom.Add(new PullResort(_bookList[book].Count));
                _answersRandom.Add(new List<PullResort>());
                for (int chapter = 0; chapter < _bookList[book].Count; chapter++)
                {
                    _answersRandom[book].Add(new PullResort(_bookList[book][chapter].Answers.Count));
                }
            }
        }

        public HistoryToBook Next(bool next)
        {
            _current = IncrimentCurrent(next, _current);
            bool already = _history.Count > _current;

            int bookIndex = !already ? _booksRandom.Next() : _history[_current].BookIndex;
            int chapterIndex = !already ? _chaptersRandom[bookIndex].Next() : _history[_current].ChapterIndex;
            List<HistoryAnswer> answersList = !already ? AnswerIndexBlender(bookIndex, chapterIndex) : _history[_current].AnswersList;
            List<int> clickHistory = !already ? new List<int>() : _history[_current].Click;
            if (!already) _history.Add(new HistoryBook(bookIndex, chapterIndex, answersList, -1, clickHistory));

            List<Answer> answersString = AnswersString(answersList);
            return new HistoryToBook(_current, bookIndex, _bookList[bookIndex][chapterIndex].Question, answersString, clickHistory);
        }

        private int IncrimentCurrent(bool next, int current)
        {
            current += next ? 1 : -1;
            if (current > _countQuestions - 1) current = _countQuestions - 1;
            else if (current < 0) current = 0;
            return current;
        }

        private List<HistoryAnswer> AnswerIndexBlender(int bookIndex, int chapterIndex)
        {
            int answerIndex = _answersRandom[bookIndex][chapterIndex].Next();
            List<HistoryAnswer> answersList = new List<HistoryAnswer>() { new HistoryAnswer(bookIndex, chapterIndex, answerIndex) };
            for (int i = 1; i < _countAnswers; i++)
            {
                int bookIndex2 = _booksRandomForAnswers.Next(bookIndex);
                chapterIndex = _chaptersRandom[bookIndex2].Next();
                answerIndex = _answersRandom[bookIndex2][chapterIndex].Next();
                answersList.Add(new HistoryAnswer(bookIndex2, chapterIndex, answerIndex));
            }
            return answersList.RandomList(answersList.Count);
        }

        private List<Answer> AnswersString(List<HistoryAnswer> list)
        {
            List<Answer> answersString = new List<Answer>();
            foreach (var child in list)
            {
                answersString.Add(new Answer(child.BookId, _bookList[child.BookId][child.ChapterId].Answers[child.AnswerId]));
            }
            return answersString;
        }
    }
}