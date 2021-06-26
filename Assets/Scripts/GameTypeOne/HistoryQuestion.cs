using System.Collections.Generic;
using System;

[Serializable]
public class HistoryQuestion
{
    public int QuestionId;
    public int ArrayId;
    public List<HistoryAnswer> AnswersId;

    public HistoryQuestion(int questionId, int arrayId, List<HistoryAnswer> answersId)
    {
        QuestionId = questionId;
        ArrayId = arrayId;
        AnswersId = answersId;
    }
}

[Serializable]
public class HistoryAnswer
{
    public int QuestionId;
    public int AnswerId;

    public HistoryAnswer(int question, int answer)
    {
        QuestionId = question;
        AnswerId = answer;
    }
}

public class HistoryToQuestionPull
{
    public int Correct;
    public string Question;
    public List<string> Answers;

    public HistoryToQuestionPull(int correct, string question, List<string> answers)
    {
        Correct = correct;
        Question = question;
        Answers = answers;
    }
}