using System.Collections.Generic;

namespace TypeOne
{
    public class ChapterA
    {
        public string Question;
        public List<string> Answers;

        public ChapterA(string question, List<string> answers)
        {
            Question = question;
            Answers = answers;
        }
    }
}
