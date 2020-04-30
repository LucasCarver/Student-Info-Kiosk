using System;
using System.Collections.Generic;

namespace StudentInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> namesList = new List<string>() { "Lucas", "Matthew", "Holly", "David" };
            List<string> foodsList = new List<string>() { "Fried Rice", "Chicken", "Pasta", "Bread" };
            List<string> hometownList = new List<string>() { "Detroit, MI", "Cleveland, OH", "Miami, FL", "Houston, TX" };
            List<string> hobbyList = new List<string>() { "Gaming", "Racing", "Cooking", "Gardening" };
            List<List<string>> listsList = new List<List<string>>() { namesList, foodsList, hometownList, hobbyList };

            int index = 0;
            string name = "";
            int selection = -1;
            string message = "";
            string input = "";
            bool exitBool = false;
            bool validInput;

            Console.WriteLine("Student Information Kiosk");
            Console.WriteLine();

            while (!exitBool)
            {
                validInput = false;
                while (!validInput)
                {
                    message = $"Enter student's name or number to retrieve info about them. (1-{namesList.Count})";
                    input = PromptUser(message).Trim();

                    if (IsInt(input))
                    {
                        index = ParseInt(input);
                        index = index - 1;
                        validInput = DrawStudentInfo(namesList, index);
                    }
                    else if (IsInList(input, namesList))
                    {
                        index = SearchList(input, namesList);
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid entry \"{input}\". Please try again.\n");
                        continue;
                    }
                }

                Console.WriteLine();
                validInput = false;
                while (!validInput)
                {
                    message = $"Which info would you like to retrieve about {namesList[index]}?\n 1. food\n 2. hometown \n 3. hobby";

                    input = PromptUser(message).Trim();
                    selection = SelectList(input);
                    if (selection != -1)
                    {
                        DrawStudentInfo(listsList[selection], index);
                        validInput = true;
                    }
                    else
                    {
                        if (IsInt(input))
                        {
                            Console.WriteLine("Invalid input \"{input}\". Accepted values are (1-3)");
                        }
                        else
                        {
                            Console.WriteLine("Invalid input \"{input}\". Accepted values are \"food\" - \"hometown\" - \"hobby\"");
                        }
                    }
                }
                Console.WriteLine();
                exitBool = ExitCondition("Retrieve additional info? y/n");
            }
        }

        public static string PromptUser(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        public static bool IsInt(string input)
        {
            try
            {
                int.Parse(input);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static int ParseInt(string input)
        {
            return int.Parse(input);
        }

        public static bool IsInList(string input, List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (input.ToLower() == list[i].ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public static int SearchList(string input, List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (input.ToLower() == list[i].ToLower())
                {
                    return i;
                }
            }
            return -1;
        }

        public static int SelectList(string input)
        {
            if (IsInt(input))
            {
                int temp = ParseInt(input);
                if (temp > 0 && temp <= 3)
                {
                    return temp;
                }
            }
            else if (input.ToLower() == "food")
            {
                return 1;
            }
            else if (input.ToLower() == "hometown")
            {
                return 2;
            }
            else if (input.ToLower() == "hobby")
            {
                return 3;
            }
            return -1;
        }

        public static bool DrawStudentInfo(List<string> list, int index)
        {
            try
            {
                string entry = list[index];
                Console.WriteLine(entry);
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine($"Input out of range. Please try again.\n");
                return false;
            }
        }

        public static bool ExitCondition(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine().ToLower().Trim();
                if (input == "n")
                {
                    return true;
                }
                else if (input == "y")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid response.\n");
                    continue;
                }
            }
        }
    }
}