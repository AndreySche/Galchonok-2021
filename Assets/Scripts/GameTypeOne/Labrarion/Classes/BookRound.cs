using System.Collections.Generic;

namespace Galchonok
{
    public class BookRound
    {
        public int Book { get; }
        public int Chapter { get; }
        
        public List<AnswerRound> Answers { get; }

        public BookRound(int book, int chapter, List<AnswerRound> answers)
        {
            Book = book;
            Chapter = chapter;
            Answers = answers;
        }
    }
}