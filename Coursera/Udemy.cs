
    using System; // Importing the System namespace
     
    namespace Coding.Exercise // Defining the Coding.Exercise namespace
    {
        public class Exercise // Defining the Exercise class
        {
            public void SimpleCalculator() // Method for a simple calculator
            {
                // Prompt the user to enter the first number
                Console.WriteLine("Enter the first number:");
                // Read the user input, convert it to a double, and store it in num1
                double num1 = Convert.ToDouble(Console.ReadLine());
     
                // Prompt the user to enter the second number
                Console.WriteLine("Enter the second number:");
                // Read the user input, convert it to a double, and store it in num2
                double num2 = Convert.ToDouble(Console.ReadLine());
     
                // Prompt the user to choose an operation
                Console.WriteLine("Choose an operation: +, -, *, /");
                // Read the user input and store it in the operation variable
                string operation = Console.ReadLine();
     
                double result; // Declare a variable to store the result
     
                // Check if the operation is addition
                if (operation == "+")
                {
                    result = num1 + num2; // Perform addition
                    // Display the result
                    Console.WriteLine($"Result: {result}");
                }
                // Check if the operation is subtraction
                else if (operation == "-")
                {
                    result = num1 - num2; // Perform subtraction
                    // Display the result
                    Console.WriteLine($"Result: {result}");
                }
                // Check if the operation is multiplication
                else if (operation == "*")
                {
                    result = num1 * num2; // Perform multiplication
                    // Display the result
                    Console.WriteLine($"Result: {result}");
                }
                // Check if the operation is division
                else if (operation == "/")
                {
                    // Check if the second number is not zero to avoid division by zero
                    if (num2 != 0)
                    {
                        result = num1 / num2; // Perform division
                        // Display the result
                        Console.WriteLine($"Result: {result}");
                    }
                    else
                    {
                        // Display an error message for division by zero
                        Console.WriteLine("Error: Division by zero is not allowed.");
                    }
                }
                else
                {
                    // Display an error message for an invalid operation
                    Console.WriteLine("Invalid operation. Please choose +, -, *, or /.");
                }
            }
        }
    }
