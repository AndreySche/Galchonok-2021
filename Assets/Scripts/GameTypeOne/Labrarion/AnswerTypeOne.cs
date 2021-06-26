namespace Galchonok
{
    public class AnswerTypeOne
    {
        public int BookIndex { get; }
        public string Answer { get; }

        public AnswerTypeOne(int bookIndex, string answer)
        {
            BookIndex = bookIndex;
            Answer = answer;
        }
    }
}