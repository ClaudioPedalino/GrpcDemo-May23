using Common;
using GrpcClient.Clients;

namespace GrpcClient.Helpers
{
    internal static class ConsoleHelper
    {
        internal static ConsoleKeyInfo GetMenuOptionSelected(int selectedIndex, string[] options)
        {
            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(options[i]);
                Console.ResetColor();
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            return keyInfo;
        }

        internal static int HandleIsUpKey(int selectedIndex, string[] options)
        {
            selectedIndex--;
            if (selectedIndex < 0)
            {
                selectedIndex = options.Length - 1;
            }

            return selectedIndex;
        }

        internal static int HandleIsDownKey(int selectedIndex, string[] options)
        {
            selectedIndex++;
            if (selectedIndex >= options.Length)
            {
                selectedIndex = 0;
            }

            return selectedIndex;
        }

        internal static async Task<int> HandleOption(int selectedIndex, string[] options)
        {
            Console.WriteLine();
            Console.WriteLine("Executing: " + options[selectedIndex]);
            Console.WriteLine("");

            if (options[selectedIndex] == Consts.GetPeopleOption)
            {
                await DataClient.GetPeople();
            }
            else if (options[selectedIndex] == Consts.GetPeopleFromRestApiOption)
            {
                await RestApiClient.GetPeople();
            }
            else if (options[selectedIndex] == Consts.GetBitcoinPriceOption)
            {
                await PriceClient.GetBitcoinPrice();
            }
            else if (options[selectedIndex] == Consts.PushToBalanceOption)
            {
                //TODO:
                //await BalanceClient.PushToBalance();
            }
            else
            {
                Console.WriteLine("No avaiable option :), try it again");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey(true);
            selectedIndex = 0;
            Console.Clear();
            return selectedIndex;
        }


        internal static void PrintPersonInfo(PersonReply person)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"ID: {person.Id}");
            Console.ResetColor();
            Console.WriteLine($"Name: {person.FullName} | Email: {person.Email} | Age: {person.Age}");
            Console.WriteLine($"Birthdate: {person.Birth}");
            Console.WriteLine();
        }

        internal static void PrintPersonInfo(Person person)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"ID: {person.Id}");
            Console.ResetColor();
            Console.WriteLine($"Name: {person.FullName} | Email: {person.Email} | Age: {person.Age}");
            Console.WriteLine($"Birthdate: {person.Birth}");
            Console.WriteLine();
        }
    }
}
