using System;
namespace Quiz;

internal class Program
{
    static void Main(string[] arg)
    {
        Question[] questions = new Question[]
        {
           new Question("What is the capitale of Germany",
                        new string[] {" Paris", "Berlin", "Madrid", "London"},
                        1
           ),

           new Question("What is 2 + 2 ",
                        new string[] {"3", "4", "5", "6"},
                        1
           )
        };

        Quiz myQuiz = new Quiz(questions);
        myQuiz.StartQuiz();
        Console.ReadLine();
    }
}

