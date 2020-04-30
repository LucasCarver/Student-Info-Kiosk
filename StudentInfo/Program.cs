using System;
using System.Collections.Generic;


namespace StudentInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> namesList = new List<string>() { "Lucas", "Matthew", "Holly", "David", "Jessica" };
            List<string> foodsList = new List<string>() { "Fried Rice", "Grilled Chicken", "Spaghetti", "Bread", "Pizza" };
            List<string> hometownList = new List<string>() { "Detroit, MI", "Cleveland, OH", "Miami, FL", "Houston, TX", "New York City, NY" };
            List<string> hobbyList = new List<string>() { "Gaming", "Racing", "Cooking", "Gardening", "Dancing" };
            List<string> movieList = new List<string>() { "Mad Max: Fury Road", "RoboCop", "Finding Nemo", "The Shining", "Paul Blart: Mall Cop" };
            //List<List<string>> listsList = new List<List<string>>() { namesList, foodsList, hometownList, hobbyList };
            List<List<string>> listsList = new List<List<string>>() { namesList, foodsList, hometownList, hobbyList, movieList };

            int index = 0;
            string name = "";
            int selection = -1;
            string message = "";
            string input = "";
            bool exitBool = false;
            bool validInput;
            string finalMessage = "";
            string item = "";

            Console.WriteLine("Student Information Kiosk");

            while (!exitBool)
            {
                Console.WriteLine();
                validInput = false;
                while (!validInput)
                {
                    message = $"Enter a student's name or number to retrieve info about them. (1-{namesList.Count})";
                    input = PromptUser(message).Trim();

                    if (IsInt(input))
                    {
                        index = ParseInt(input);
                        index = index - 1;
                        validInput = VerifyInput(namesList, index);
                        if (validInput)
                        {
                            name = GetStringFromList(namesList, index);
                        }
                    }
                    else if (IsInList(input, namesList))
                    {
                        index = SearchList(input, namesList);
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid entry \"{input}\". Please try again.");
                        continue;
                    }
                }
                name = GetStringFromList(namesList, index);
                validInput = false;

                while (!validInput)
                {
                    message = "";
                    message += $"Which info would you like to retrieve about {name}?";
                    message += $"\n 1. {name}'s favorite food";
                    message += $"\n 2. {name}'s hometown";
                    message += $"\n 3. {name}'s hobby";
                    message += $"\n 4. {name}'s favorite movie";
                        //$"\n 1. {name}'s favorite food\n 2. {name}'s hometown \n 3. {name}'s hobby \n 4.";

                    input = PromptUser(message).Trim();
                    selection = SelectList(listsList, input);
                    if (selection != -1)
                    {
                        VerifyInput(namesList, index);
                        item = GetStringFromList(listsList[selection], index);
                        validInput = true;
                    }
                    else
                    {
                        if (IsInt(input))
                        {
                            Console.WriteLine($"Invalid input \"{input}\". Accepted values are (1-{listsList.Count - 1})");
                        }
                        else
                        {
                            Console.WriteLine($"Please enter a number. Accepted values are (1-{listsList.Count - 1})");
                        }
                    }
                }

                finalMessage = FinalMessage(name, selection, item);
                Console.WriteLine(finalMessage);
                Console.WriteLine();
                exitBool = ExitCondition("Retrieve additional info? y/n");
            }
            Console.WriteLine();
            Console.WriteLine("Thank you for using the Student Information Kiosk");
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

        public static int SelectList(List<List<string>> list, string input)
        {
            if (IsInt(input))
            {
                int temp = ParseInt(input);
                if (temp > 0 && temp <= list.Count - 1)
                {
                    return temp;
                }
            }
            return -1;
        }

        public static string GetStringFromList(List<string> list, int index)
        {
            string entry = list[index];
            return entry;
        }

        public static bool VerifyInput(List<string> list, int index)
        {
            string entry = "";
            try
            {
                entry = list[index];
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine($"Invalid input \"{index+1}\". Accepted values are (1-{list.Count})");
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

        public static string FinalMessage(string name, int selection, string item)
        {
            string temp = $"{name}'s ";
            if (selection == 1)
            {
                temp += "favorite food";
            }
            else if (selection == 2)
            {
                temp += "hometown";
            }
            else if (selection == 3)
            {
                temp += "hobby";
            }
            else if (selection == 4)
            {
                temp += "favorite movie";
            }
            temp += $" is {item}.";
            return temp;
        }
    }
}