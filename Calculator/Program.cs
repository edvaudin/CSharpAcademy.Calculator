namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;

            DisplayTitle();

            Calculator calculator = new Calculator();

            while (!endApp)
            {
                Console.Write("Type a number, and then press Enter: ");
                double cleanNum1 = ValidateNumberInput();

                Console.Write("Type another number, and then press Enter: ");
                double cleanNum2 = ValidateNumberInput();

                DisplayOperatorOptions();

                string op = ValidateOperatorInput();

                double result = GetResult(calculator, cleanNum1, cleanNum2, op);

                DisplayFooter();

                endApp = Console.ReadLine() == "n";
            }
            calculator.Finish();
            return;
        }

        private static double GetResult(Calculator calculator, double cleanNum1, double cleanNum2, string op)
        {
            double result = 0;
            try
            {
                result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else Console.WriteLine("Your result: {0:0.##}\n", result);
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }

            return result;
        }

        private static void DisplayFooter()
        {
            Console.WriteLine("------------------------\n");
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            Console.WriteLine("\n");
        }

        private static void DisplayOperatorOptions()
        {
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option? ");
        }

        private static string ValidateOperatorInput()
        {
            string op = Console.ReadLine();
            while (!IsValidOperator(op))
            {
                Console.Write("This is not valid input. Please enter one of the above operators: ");
                op = Console.ReadLine();
            }
            return op;
        }

        private static bool IsValidOperator(string op)
        {
            string[] validOperators = { "a", "s", "m", "d" };
            foreach (string validOp in validOperators)
            {
                if (op == validOp)
                {
                    return true;
                }
            }
            return false;
        }

        private static double ValidateNumberInput()
        {
            string numInput = Console.ReadLine();
            double cleanNum;
            while (!double.TryParse(numInput, out cleanNum))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                numInput = Console.ReadLine();
            }
            return cleanNum;
        }

        private static void DisplayTitle()
        {
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");
        }
    }
}


