using System.Collections.Generic;
using UnityEngine;

namespace Galchonok
{
    public class LibrarionA
    {
        private QuestionsBlender _blender;
        public List<bool> clickSaver { private set; get; }
        readonly List<List<QuestionA>> _pack = new List<List<QuestionA>>()
        {
            new List<QuestionA>(){
                new QuestionA( "кто",
                    new List<string> { "мама", "папа", "мальчик", "девочка", "бабушка", "дедушка", "собака", "кошка", "лошадь", "учитель", "доктор", "космонавт", "балерина", "водитель", "полицейский", "уборщик", "сосед", "друг", "подруга", "соседка", "продавец", "продавщица", "парикмахер", "кассир", "танцор", "певец" }
                )
            },

            new List<QuestionA>(){
                new QuestionA( "что",
                    new List<string>{ "музыка", "вода", "машина", "троллейбус", "трамвай", "метро", "банан", "яблоко", "картошка" }
                )
            },

            new List<QuestionA>(){
                new QuestionA( "что делает",
                    new List<string>{ "играет", "пишет", "читает", "идёт", "бежит", "ищет", "смотрит", "гуляет", "сажает", "умеет", "делает", "кричит", "плачет", "может", "просит", "говорит", "спрашивает", "учится", "прыгает", "видит" }
                ),


                new QuestionA( "что делают",
                    new List<string>{ "играют", "пишут", "читают", "идут", "бегут", "ищут", "смотрят", "гуляют", "сажают", "умеют", "делают", "кричат", "плачут", "могут", "просят", "говорят", "спрашивают", "учатся", "прыгают", "видят" }
                ),

                new QuestionA( "что сделал",
                    new List<string>{ "поиграл", "написал", "почитал", "пришел", "прибежал", "нашел", "посмотрел", "погулял", "посадил", "сумел", "сделал", "покричал", "поплакал", "смог", "попросил", "сказал", "сросил", "научился", "пригнул", "увидел" }
                ),

                new QuestionA( "что сделала",
                    new List<string>{ "наигралась", "подписала", "прочитала", "пришла", "прибежала", "нашла", "посмотрела", "погуляла", "посадила", "сумела", "сделала", "накричалась", "наплакалась", "смогла", "попросила", "поговорила", "спросила", "научилась", "прыгнула", "увидела" }
                ),

                new QuestionA( "что сделали",
                    new List<string>{ "наигрались", "написали", "прочитали", "пришли", "прибежали", "нашли", "посмотрели", "погуляли", "посадили", "сумели", "сделали", "покричали", "поплакали", "смогли", "попросили", "поговорили", "спросили", "научились", "прыгнули", "увидели" }
                )
            },

            new List<QuestionA>(){
                new QuestionA( "какой",
                    new List<string>{ "большой", "маленький", "красивый", "умный", "хороший", "плохой", "круглый", "зелёный", "красный", "жёлтый", "синий", "белый", "чёрный", "коричневый", "злой", "добрый", "глупый", "высокий", "низкий", "тяжёлый", "лёгкий", "твёрдый", "мягкий", "старый", "молодой", "грустный", "весёлый", "сердитый" }
                )
            },

            new List<QuestionA>(){
                new QuestionA( "где",
                    new List<string>{ "в школе", "дома", "на улице", "на горке", "на площадке", "в магазине", "в зоопарке", "на кассе", "в кафе", "в туалете", "в ванной", "в комнате", "в кабинете" }
                )
            },

            new List<QuestionA>(){
                new QuestionA( "когда",
                    new List<string>{ "вчера", "завтра", "сегодня", "потом", "сначала", "позже", "раньше", "в понедельник", "во вторник", "в среду", "в четверг", "в пятницу", "в субботу", "в воскресенье", "в январе", "в феврале", "в марте", "в апреле", "в мае", "в июне", "в июле", "в августе", "в сентябре", "в октябре", "в ноябре", "в декабре" }
                )
            },
        };

        public LibrarionA(int countQuestions, int countAnswer)
        {
            clickSaver = new List<bool>();
            _blender = new QuestionsBlender(countQuestions, countAnswer, _pack);
            //_blender.JsonHistory();
        }

        public HistoryToQuestionPull Next()
        {
            HistoryQuestion historyQuestion = _blender.HistoryNext();
            if (historyQuestion == null) return null;

            int questionId = historyQuestion.QuestionId;
            int arrayId = historyQuestion.ArrayId;
            int correct = -1;
            List<HistoryAnswer> answersId = historyQuestion.AnswersId;

            List<string> answers = new List<string>();

            int i = 0;
            foreach (var child in answersId)
            {
                int at = child.QuestionId == questionId ? arrayId : 0;
                if (child.QuestionId == questionId) correct = i;
                answers.Add(_pack[child.QuestionId][at].Answer[child.AnswerId]);
                i++;
            }

            return new HistoryToQuestionPull(correct, _pack[questionId][arrayId].Question, answers);
        }

        public void ClickSaver(bool correct, int click) => clickSaver.Add(correct);
    }
}
