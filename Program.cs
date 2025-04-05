using System.Data;

namespace MathematicalExpressionEvaluator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Get the first arithmetic expression from the user
            string expression1 = GetValidExpression("Enter the first arithmetic expression: ");

            // Get the second arithmetic expression from the user
            string expression2 = GetValidExpression("Enter the second arithmetic expression: ");

            // Evaluate the expressions and perform operations
            EvaluateExpressions(expression1, expression2);

            Console.WriteLine("Press a key to exit..");
            Console.ReadKey();
        }

        // Method to get a valid arithmetic expression from the user
        static string GetValidExpression(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (StackOperation.CheckBrackets(input, '(', ')'))
                    return input;

                Console.WriteLine("Invalid bracket sequence. Please try again!");
            }
        }

        // Method to evaluate the expressions and perform operations
        static void EvaluateExpressions(string expression1, string expression2)
        {
            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("A - Addition");
                Console.WriteLine("S - Subtraction");
                Console.WriteLine("M - Multiplication");
                Console.WriteLine("E - Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine().ToUpper();

                if (choice =="E")
                    break;

                double resultExpression1 = StackOperation.EvaluateExpression(expression1);
                double resultExpression2 = StackOperation.EvaluateExpression(expression2);
             

                switch (choice)
                {
                    case "A":
                       double result1 = resultExpression1;
                        double result2 = resultExpression2;
                       double result3 = resultExpression1 + resultExpression2;
                        Console.WriteLine("Result - Expression 1: (2 + 2) + (2 - 1) + 1 = " + resultExpression1);
                        Console.WriteLine("Result - Expression 2: ((2 + 1) + (1 - 1) * 1) = " + resultExpression2);
                        Console.WriteLine("Final Result: " + result1 +" + "+ result2 + " = "+ result3);
                        break;
                    case "S":
                        double result1Sub = resultExpression1;
                        double result2Sub = resultExpression2;
                        double result3Sub = resultExpression1 - resultExpression2;
                        Console.WriteLine("Result - Expression 1: (2 + 2) + (2 - 1) + 1 = " + resultExpression1);
                        Console.WriteLine("Result - Expression 2: ((2 + 1) + (1 - 1) * 1) = " + resultExpression2);
                        Console.WriteLine("Final Result: " + result1Sub + " - " + result2Sub + " = " + result3Sub);
                        break;
                    case "M":
                        double result1Mult = resultExpression1;
                        double result2Mult = resultExpression2;
                        double result3Mult = resultExpression1 * resultExpression2;
                        Console.WriteLine("Result - Expression 1: (2 + 2) + (2 - 1) + 1 = " + resultExpression1);
                        Console.WriteLine("Result - Expression 2: ((2 + 1) + (1 - 1) * 1) = " + resultExpression2);
                        Console.WriteLine("Final Result: " + result1Mult + " * " + result2Mult + " = " + result3Mult);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        
    }
}

