using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MathematicalExpressionEvaluator
{
    public static class StackOperation
    {
        public static bool IsEmpty<T>(this Stack<T> stack) // extension method
        {
            return stack.Count == 0; // return a boolean indicating whether the stack is empty or not
        }

        public static double EvaluateExpression(string expression)
        {
            // The out keyword is used in method parameters to indicate that the parameter is being used for
            // output purposes rather than input. When a parameter is marked with the out keyword, it signifies
            // that the method will assign a value to that parameter, and the assigned value will be available
            // to the caller after the method returns, even if the parameter was not initialized before the method call.
            bool isValid = ToPostFix(expression, out string pf);
            //Console.WriteLine("\tPost-fix  : " + pf); // prints the original expression
            if (isValid)
                return Evaluate(pf); // evaluate the postfix expression
            return default;
        } //EvaluateExpression

        // Implement the Check Bracket Method Here
        

        // This method converts an infix arithmetic expression to postfix notation.
        public static bool ToPostFix(string expression, out string pf)
        {
            if (!CheckBrackets(expression, '(', ')'))
            {
                pf = "Brackets invalid";
                return false;
            }

            //Initialise stack and postfix expression
            Stack<string> stack = new Stack<string>(); // stack is used to store operators
            pf = ""; // the string is used to store postfix expression

            //0. Add spaces where necessary.
            //Spaces are added around operators and operands in the expression to tokenize the expression.
            expression = expression.Replace("*", " * ").Replace("/", " / ").Replace("+", " + ").Replace("-", " - ").Replace("(", " ( ").Replace(")", " ) ");

            //1. Scan all symbols from left to right. Split the expression into symbols and iterates through
            //each symbol:
            string[] symbols = expression.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string symbol in symbols)
            {
                //2. If operand (not operator)
                // If the symbol is an operand (not an operator), add it to the postfix expression
                if (!"( + - * /)".Contains(symbol))
                    pf += symbol + " ";
                //end operand

                //3. Left parenthesis
                // If the symbol is a left parenthesis '(', it pushes it onto the stack.
                else if (symbol == "(")
                    stack.Push(symbol);
                //end symbol == "("

                //4. Right parenthesis
                // If the symbol is a right parenthesis ')', pop operators from the stack and add them to
                // the postfix expression until it encounters a matching left parenthesis '('.
                else if (symbol == ")")
                {
                    while (stack.Peek() != "(")
                        pf += stack.Pop() + " ";
                    stack.Pop();
                } //end symbol == ")"

                //5. Operator (+, -, * , /)
                // If the symbol is an operator (+, -, *, /), compare its precedence with the precedence of
                // operators on the stack and add operators from the stack to the postfix expression until the
                // stack is empty or an operator with lower precedence is encountered. Then, push the
                // current operator onto the stack.
                else // if ("+ - * /".Contains(symbol))
                {
                    //*, / have higher or equal precedence than any other operator
                    while (!stack.IsEmpty() && Precedence(symbol) <= Precedence(stack.Peek()))
                        pf += stack.Pop() + " ";
                    stack.Push(symbol);
                } //end operator                   
            } //foreach symbol

            //6. Remaining symbols
            // After processing all symbols, add any remaining operators from the stack to the postfix expression.
            while (!stack.IsEmpty())
                pf += stack.Pop() + " ";

            //return
            return true;
        } //ToPostFix

     

        /*
Let's use the following infix expression as an example: 3 + 4 * (2 - 1)
Add Spaces: Add spaces between operands, operators, and parentheses to tokenize the expression:

Original: 3 + 4 * (2 - 1)
After adding spaces: 3 + 4 * ( 2 - 1 )
Tokenize: Split the expression into individual tokens:

Tokens: 3, +, 4, *, (, 2, -, 1, )
Conversion:

Start iterating through each token:
Token: '3'
Since it's an operand, append it to the postfix expression: 3
Token: '+'
It's an operator, so push it onto the stack.
Token: '4'
Operand, so append it to the postfix expression: 3 4
Token: '*'
Operator with higher precedence than '+', so push onto the stack.
Token: '('
Push onto the stack.
Token: '2'
Operand, so append to the postfix expression: 3 4 2
Token: '-'
Operator, push onto the stack.
Token: '1'
Operand, append to the postfix expression: 3 4 2 1
Token: ')'
Pop operators from the stack and append to the postfix expression until '(' is encountered: 3 4 2 1 -
Stack: '+ *'
End of expression
Pop any remaining operators from the stack and append to the postfix expression: 3 4 2 1 - * +
So, the postfix expression equivalent to the infix expression '3 + 4 * (2 - 1)' is '3 4 2 1 - * +'
*/

        // This method returns the precedence of an operator (+, -, *, /).
        private static int Precedence(string op)
        {
            if (op == null)
                return -1;
            string operators = "+-*/^";
            return operators.IndexOf(op);
        } //Precedence

        // This method evaluates a postfix arithmetic expression and returns the result.
        public static double Evaluate(string pf)
        {
            Stack<double> stack = new Stack<double>(); // Initialize a stack to store operands.

            // split the postfix expression into symbols and iterates through each symbol:
            string[] symbols = pf.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

            foreach (string symbol in symbols)
            {
                if (double.TryParse(symbol, out double value)) // If the symbol is a number, it pushes it onto the stack.
                    stack.Push(value);
                else
                {
                    // If the symbol is an operator, it pops two operands from the stack, performs the operation,
                    // and pushes the result back onto the stack.
                    double v2 = stack.Pop(),
                        v1 = stack.Pop();
                    stack.Push(DoOperation(v1, v2, symbol));
                }
            } //foreach

            // After processing all symbols, it returns the result.
            return stack.Peek();
        } //Evaluate

        // This method performs arithmetic operations (+, -, *, /) on two operands and returns the result.
        private static double DoOperation(double v1, double v2, string op)
        {
            double result = 0;
            switch (op)
            {
                case "+": result = v1 + v2; break;
                case "-": result = v1 - v2; break;
                case "*": result = v1 * v2; break;
                case "/": result = v1 / v2; break;
            }
            return result;
        } //DoOperation

        internal static bool CheckBrackets(string input, char v1, char v2)
        {
            StaticGenericStack<char> stack = new StaticGenericStack<char>();

            foreach (char c in input)
            {
                if (c == v1)
                {
                    stack.Push(c);
                }
                else if (c == v2)
                {

                    if (stack.Count == 0)
                    {
                        return false;
                    }
                    stack.Pop();

                }
            }
            return stack.Count == 0;
        }
    }
}
