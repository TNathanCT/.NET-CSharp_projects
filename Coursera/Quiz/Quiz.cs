using System;

namespace Quiz;

public class Quiz
{
    private Question[] questions;
    public Quiz(Question[] questionsList)
    {
        questions = questionsList;
        score = 0;
    }

    private int score;




    public void StartQuiz()
    {
        Console.WriteLine("Welcome to the Quiz!");
        int questionNumber = 1;

        foreach(Question question in questions)
        {
            Console.WriteLine($"Question {questionNumber++} :");
            DisplayQuestion(question);
            int userChoice = GetUserChoice(question);
            if (question.IsCorrectAnswer(userChoice))
            {
                Console.WriteLine("Correct!");
                score++;
            }
            else
            {
                Console.WriteLine($"Wrong! The correct answer was : {question.Answers[question.CorrectAnswerIndex]}");
            }
        }
    }

    public void DisplayQuestion(Question question)
    {

        Console.WriteLine(question.QuestionText);

        for (int i = 0; i < question.Answers.Length; i++)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("    ");
            Console.Write(i + 1);
            Console.ResetColor();
            Console.WriteLine($". {question.Answers[i]}");
        }


    }

    void DisplayResults()
    {
        Console.WriteLine($"Quiz finished, your result is : {score} out of {questions.Length}");
        double percentage = (double)score / questions.Length;

        if (percentage >= 0.8)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Excellent work!");
        }

        if (percentage >= 0.5)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Good work!");
        }

        if (percentage < 0.5)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Fail!");
        }
        Console.ResetColor();
    }


    private int GetUserChoice(Question question)
    {
        Console.WriteLine("Your answer (number) : ");
        string input = Console.ReadLine();
        int choice = 0;
        while (!int.TryParse(input, out choice) || choice < 0 || choice > question.Answers.Length)
        {
            Console.WriteLine("Invalid Choice. Please choose between 1 and 4!");
            input = Console.ReadLine();
        }

        return choice - 1;
    }


}
