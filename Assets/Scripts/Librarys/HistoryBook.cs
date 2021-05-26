using System.Collections.Generic;

namespace Galchonok
{
    public class HistoryBook
    {
        public int BookIndex;
        public int ChapterIndex;
        public int AnswerIndex;
        public List<HistoryAnswer> AnswersList;
        public List<int> Click;

        public HistoryBook(int bookIndex, int chapterIndex, List<HistoryAnswer> answersList, int answerIndex, List<int> click)
        {
            BookIndex = bookIndex;
            ChapterIndex = chapterIndex;
            AnswersList = answersList;
            AnswerIndex = answerIndex;
            Click = click;
        }

        public void HistoryClick(int number)
        {
            Click.Add(number);
        }
    }
}
