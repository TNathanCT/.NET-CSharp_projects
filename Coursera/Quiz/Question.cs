using System;

namespace Quiz;

public class Question
{
    public string QuestionText { get; set; } //what we want to display
    public string[] Answers { get; }// the possible answers
    public int CorrectAnswerIndex { get; } // the right answer amongst the Answrs

    //Pass all the information
    public Question(string questionText, string[] answers, int correctanswer)
    {
        QuestionText = questionText;
        Answers = answers;
        CorrectAnswerIndex = correctanswer;
    }

    public bool IsCorrectAnswer(int iscorrectAnswer)
    {
        return CorrectAnswerIndex == iscorrectAnswer;
    }
}
