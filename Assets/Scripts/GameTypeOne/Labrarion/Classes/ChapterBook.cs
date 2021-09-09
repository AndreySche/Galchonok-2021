using System.Collections.Generic;

namespace Galchonok
{
    public class ChapterBook
    {
        public int Book { get; }
        public int Chapter { get; }
        public int Answer { get; }
        
        public List<AnswerRound> Answers { get; }

        public ChapterBook(int book, int chapter, int answer, List<AnswerRound> answers)
        {
            Book = book;
            Chapter = chapter;
            Answer = answer;
            Answers = answers;
        }
    }
}