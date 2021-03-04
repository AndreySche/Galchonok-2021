using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Galchonok
{
    public class LibrarionA
    {
        PullResort _qustionResort = null;
        List<PullResort> _answerResort = new List<PullResort>();
        System.Random _rnd = new System.Random();

        readonly List<QuestionA> _pack = new List<QuestionA>()
        {
            new QuestionA(
                new List<string>(){"КТО"},
                new List<List<string>>{
                    new List<string> {
                    "мама", "папа", "мальчик", "девочка", "бабушка", "дедушка", "собака", "кошка", "лошадь", "учитель", "доктор", "космонавт", "балерина", "водитель", "полицейский", "уборщик", "сосед", "друг", "подруга", "соседка", "продавец", "продавщица", "парикмахер", "кассир", "танцор", "певец" }
                    }
                ),

            new QuestionA(
                new List<string>(){"ЧТО"},
                new List<List<string>>{
                    new List<string>{ "музыка", "вода", "машина", "троллейбус", "трамвай", "метро", "банан", "яблоко", "картошка" }
                    }
                ),

            new QuestionA(
                new List<string>(){"ЧТО ДЕЛАЕТ", "ЧТО ДЕЛАЮТ", "ЧТО СДЕЛАЛ", "ЧТО СДЕЛАЛА", "ЧТО СДЕЛАЛИ" },
                new List<List<string>>{
                    new List<string>{ "играет", "пишет", "читает", "идёт", "бежит", "ищет", "смотрит", "гуляет", "сажает", "умеет", "делает", "кричит", "плачет", "может", "просит", "говорит", "спрашивает", "учится", "прыгает", "видит" }, // ЧТО ДЕЛАЕТ

                    new List<string>{ "играют", "пишут", "читают", "идут", "бегут", "ищут", "смотрят", "гуляют", "сажают", "умеют", "делают", "кричат", "плачут", "могут", "просят", "говорят", "спрашивают", "учатся", "прыгают", "видят" }, // ЧТО ДЕЛАЮТ

                    new List<string>{ "поиграл", "написал", "почитал", "пришел", "прибежал", "нашел", "посмотрел", "погулял", "посадил", "сумел", "сделал", "покричал", "поплакал", "смог", "попросил", "сказал", "сросил", "научился", "пригнул", "увидел" }, // ЧТО СДЕЛАЛ

                    new List<string>{ "наигралась", "подписала", "прочитала", "пришла", "прибежала", "нашла", "посмотрела", "погуляла", "посадила", "сумела", "сделала", "накричалась", "наплакалась", "смогла", "попросила", "поговорила", "спросила", "научилась", "прыгнула", "увидела" }, // ЧТО СДЕЛАЛА

                    new List<string>{ "наигрались", "написали", "прочитали", "пришли", "прибежали", "нашли", "посмотрели", "погуляли", "посадили", "сумели", "сделали", "покричали", "поплакали", "смогли", "попросили", "поговорили", "спросили", "научились", "прыгнули", "увидели" } // ЧТО СДЕЛАЛИ
                    }
                ),

            new QuestionA(
                new List<string>(){"КАКОЙ" },
                new List<List<string>>{
                    new List<string>{ "большой", "маленький", "красивый", "умный", "хороший", "плохой", "круглый", "зелёный", "красный", "жёлтый", "синий", "белый", "чёрный", "коричневый", "злой", "добрый", "глупый", "высокий", "низкий", "тяжёлый", "лёгкий", "твёрдый", "мягкий", "старый", "молодой", "грустный", "весёлый", "сердитый" }
                    }
                ),

            new QuestionA(
                new List<string>(){"ГДЕ" },
                new List<List<string>>{
                    new List<string>{ "в школе", "дома", "на улице", "на горке", "на площадке", "в магазине", "в зоопарке", "на кассе", "в кафе", "в туалете", "в ванной", "в комнате", "в кабинете" }
                    }
                ),

            new QuestionA(
                new List<string>(){"КОГДА" },
                new List<List<string>>{
                    new List<string>{ "вчера", "завтра", "сегодня", "потом", "сначала", "позже", "раньше", "в понедельник", "во вторник", "в среду", "в четверг", "в пятницу", "в субботу", "в воскресенье", "в январе", "в феврале", "в марте", "в апреле", "в мае", "в июне", "в июле", "в августе", "в сентябре", "в октябре", "в ноябре", "в декабре" }
                    }
                )
        };

        public int _countAnswer { get; private set; }
        int _countQuestion = 0;

        public LibrarionA(int countQuestions, int countAnswer)
        {
            _countQuestion = countQuestions;
            _countAnswer = countAnswer;
            _qustionResort = new PullResort(_pack.Count);
            for (int i = 0; i < _pack.Count; i++)
            {
                _answerResort.Add(new PullResort(_pack[i].Answer[0].Count));
            }
        }

        public QuestionA Next()
        {
            if (--_countQuestion < 0) return null;


            int index = _qustionResort.Next();
            int rnd = Random.Range(0, _pack[index].Question.Count);

            List<string> answers = new List<string>();
            answers.Add(_pack[index].Answer[rnd][_answerResort[index].Next()]);

            for (int i = 0; i < _pack.Count; i++)
            {
                int iNew = _qustionResort.Numbers[i];
                if (iNew == index || answers.Count >= _countAnswer) continue;

                int at = 0;
                if (_pack[iNew].Answer.Count > 1) at = Random.Range(0, _pack[iNew].Answer.Count);
                //Debug.Log($"at={at}");

                string word = _pack[iNew].Answer[at][_answerResort[iNew].Next()];
                //Debug.Log($"word={word}");
                answers.Add(word);
            }

            QuestionA question = new QuestionA(new List<string> { _pack[index].Question[rnd] }, new List<List<string>>() { answers });
            return question;
        }

        public QuestionA NextRevers()
        {
            if (--_countQuestion < 0) return null;

            int rnd = Random.Range(0, _pack.Count);
            int index = _answerResort[rnd].Next();

            List<string> answers = new List<string>();
            answers.Add(_pack[rnd].Question[0]);

            for (int i = 0; i < _pack.Count; i++)
            {
                int iNew = _qustionResort.Numbers[i];
                if (iNew == rnd || answers.Count >= _countAnswer) continue;

                string word = _pack[iNew].Question[0];
                Debug.Log($"word={word}");
                answers.Add(word);
            }

            QuestionA question = new QuestionA(new List<string> { _pack[rnd].Answer[0][_answerResort[rnd].Next()] }, new List<List<string>>() { answers });
            return question;
        }
    }
}
