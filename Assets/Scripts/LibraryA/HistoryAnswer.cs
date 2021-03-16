using System.Collections.Generic;

namespace Galchonok
{
    public class HistoryAnswer
    {
        public int BookId;
        public int ChapterId;
        public int AnswerId;

        public HistoryAnswer(int bookId, int chapterId, int answerId)
        {
            BookId = bookId;
            ChapterId = chapterId;
            AnswerId = answerId;
        }
    }
}
