using System.Collections.Generic;

namespace Galchonok
{
    public class HistoryToBook
    {
        public int Index;
        public int CorrectBook;
        public string Question;
        public List<Answer> Answers;
        public List<int> Click;

        public HistoryToBook(int index, int correctBook, string question, List<Answer> answers, List<int> click)
        {
            Index = index;
            CorrectBook = correctBook;
            Question = question;
            Answers = answers;
            Click = click;
        }
    }
}
