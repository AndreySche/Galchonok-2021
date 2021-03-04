using System.Collections.Generic;

namespace Galchonok
{
    public class QuestionA
    {
        public List<string> Question;
        public List<List<string>> Answer;

        public QuestionA(List<string> question, List<List<string>> answer)
        {
            Question = question;
            Answer = answer;
        }
    }
}
