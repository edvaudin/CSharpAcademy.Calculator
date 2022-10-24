﻿using CalculatorLibrary;
using System.Text.RegularExpressions;

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
                double cleanNum1 = ValidateNumberInput(calculator);

                Console.Write("Type another number, and then press Enter: ");
                double cleanNum2 = ValidateNumberInput(calculator);

                DisplayOperatorOptions();

                string op = ValidateOperatorInput();

                double result = CalculateResult(calculator, cleanNum1, cleanNum2, op);
                PrintResult(result);

                DisplayCalculatorUsage(calculator);

                DisplayMemory(calculator);

                DisplayCleanupOptions();

                endApp = ProcessCleanup(endApp, calculator);

            }
            calculator.Finish();
            return;
        }

        // This seems to be an ugly solution. Where can I put the endApp bool?
        private static bool ProcessCleanup(bool endApp, Calculator calculator)
        {
            string selection = Console.ReadLine();
            switch (selection)
            {
                case "n":
                    endApp = true;
                    break;
                case "c":
                    calculator.ClearHistory();
                    break;
                default:
                    break;
            }

            return endApp;
        }

        private static void DisplayCalculatorUsage(Calculator calculator)
        {
            Console.Write($"This calculator has been used {calculator.CalculationsCompleted} times.\n");
        }

        private static void PrintResult(double result)
        {
            if (double.IsNaN(result))
            {
                Console.WriteLine("This operation will result in a mathematical error.\n");
            }
            else Console.WriteLine("Your result: {0:0.##}\n", result);
        }

        private static double CalculateResult(Calculator calculator, double cleanNum1, double cleanNum2, string op)
        {
            double result = 0;
            try
            {
                result = calculator.DoOperation(cleanNum1, cleanNum2, op);
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }
            return result;
        }

        private static void DisplayMemory(Calculator calculator)
        {
            Console.WriteLine("---------Memory--------\n");
            string memory = string.Empty;
            int i = 0;
            foreach (MemoryCalculation calc in calculator.CalculationMemory)
            {
                memory += $"{i}: {calc}";
                i++;
            }
            Console.WriteLine(memory);
            Console.WriteLine("To reference memory in your input type 'M' followed by the memory reference (e.g. M2)");
        }

        private static void DisplayOperatorOptions()
        {
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tp - Power");
            Console.Write("Your option? ");
        }

        private static void DisplayCleanupOptions()
        {
            Console.WriteLine("-----------------------\n");
            Console.WriteLine("What would you like to do next?:");
            Console.WriteLine("\tn - Exit");
            Console.WriteLine("\tc - Clear History");
            Console.WriteLine("\tOther - Continue calculating!");
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
            string[] validOperators = { "a", "s", "m", "d", "p" };
            foreach (string validOp in validOperators)
            {
                if (op == validOp)
                {
                    return true;
                }
            }
            return false;
        }

        private static double ValidateNumberInput(Calculator calculator)
        {
            string numInput = Console.ReadLine();
            double cleanNum;
            while (!double.TryParse(numInput, out cleanNum))
            {
                if (numInput.StartsWith('M') && int.TryParse(numInput.Substring(1), out int memoryReference))
                {
                    if (memoryReference < calculator.CalculationMemory.Count)
                    {
                        cleanNum = calculator.CalculationMemory[memoryReference].Result;
                        break;
                    }
                    else
                    {
                        Console.Write("Invalid memory reference detected. Please enter a number or memory reference: ");
                    }
                }
                else
                {
                    Console.Write("This is not valid input. Please enter a number or memory reference: ");
                }
                
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


