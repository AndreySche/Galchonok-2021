using System.Collections.Generic;
using UnityEngine;

namespace Galchonok
{
    public class LibrarionTwo
    {
        private List<List<ChapterA>> _bookList;
        private List<HistoryBook> _history = new List<HistoryBook>();
        private PullResort _booksRandom, _booksRandomForAnswers;
        private List<PullResort> _chaptersRandom = new List<PullResort>();
        private List<PullResort> _chaptersForAnswerRandom = new List<PullResort>();
        private List<List<PullResort>> _answersRandom = new List<List<PullResort>>();
        private LibA _libA = new LibA();
        private int _countQuestions, _countAnswers, _current;

        public LibrarionTwo(int countQuestions, int countAnswers)
        {
            _current = -1;
            _countQuestions = countQuestions;
            _countAnswers = countAnswers;
            _bookList = _libA.BookList;
            int countBook = _bookList.Count;
            _booksRandom = new PullResort(countBook);
            _booksRandomForAnswers = new PullResort(countBook);

            for (int book = 0; book < countBook; book++)
            {
                _chaptersRandom.Add(new PullResort(_bookList[book].Count));
                _chaptersForAnswerRandom.Add(new PullResort(_bookList[book].Count));
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
            int answerIndex = !already ? _answersRandom[bookIndex][chapterIndex].Next() : _history[_current].AnswerIndex;
            List<HistoryAnswer> answersList = !already ? AnswerIndexBlender(bookIndex, chapterIndex) : _history[_current].AnswersList;
            List<int> clickHistory = !already ? new List<int>() : _history[_current].Click;
            if (!already) _history.Add(new HistoryBook(bookIndex, chapterIndex, answersList, answerIndex, clickHistory));

            List<Answer> answersString = AnswersString(answersList);
            
            string question = _bookList[bookIndex][chapterIndex].Answers[answerIndex];
            return new HistoryToBook(_current, bookIndex, question, answersString, clickHistory);
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
            int answerIndex = _chaptersRandom[bookIndex].Next();
            List<HistoryAnswer> answersList = new List<HistoryAnswer>() { new HistoryAnswer(bookIndex, chapterIndex, answerIndex) };
            for (int i = 1; i < _countAnswers; i++)
            {
                int bookIndex2 = _booksRandomForAnswers.Next(bookIndex);
                chapterIndex = _chaptersForAnswerRandom[bookIndex2].Next();
                answersList.Add(new HistoryAnswer(bookIndex2, chapterIndex, -1));
            }
            return answersList.RandomList();
        }

        private List<Answer> AnswersString(List<HistoryAnswer> list)
        {
            List<Answer> answersString = new List<Answer>();
            foreach (var child in list)
            {
                answersString.Add(new Answer(child.BookId, _bookList[child.BookId][child.ChapterId].Question));
            }
            return answersString;
        }

        public void AddClickToHistory(int buttonIndex)
        {
            _history[_current].HistoryClick(buttonIndex);
        }
    }
}