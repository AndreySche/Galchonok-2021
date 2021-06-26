namespace Galchonok
{
    public class AnswerRound
    {
        public int Book { get; }
        public int Chapter { get; }
        public int Answer { get; }

        public AnswerRound(int book, int chapter, int answer)
        {
            Book = book;
            Chapter = chapter;
            Answer = answer;
        }
    }
}