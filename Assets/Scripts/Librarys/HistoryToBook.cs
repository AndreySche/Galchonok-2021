using System.Collections.Generic;

namespace Galchonok
{
    public class HistoryToBook
    {
        public int Index;
        public int CorrectBookIndex;
        public string Question;
        public List<Answer> Answers;
        public List<int> Click;

        public HistoryToBook(int index, int correctBookIndex, string question, List<Answer> answers, List<int> click)
        {
            Index = index;
            CorrectBookIndex = correctBookIndex;
            Question = question;
            Answers = answers;
            Click = click;
        }
    }
}
