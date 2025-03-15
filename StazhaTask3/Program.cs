using System.Security.Cryptography;

namespace InternshipTask3
{
    internal class Program
    {
        private static Dice[] _dices;
        private static double[,] _winProbabilities;

        static void Main(string[] args)
        {
            try
            {
                _dices = ConsoleParser.Parse(args);
                _winProbabilities = WinningProbabilitiesCalculator.Calculate(_dices);
                var countFaces = _dices[0].CountFaces;

                Console.WriteLine("Let's determine who makes the first move.\r\nI selected a random value in the range 0..1 ");

                var key = HMACGenerator.GenerateRandomKey(32);
                var randomComputerNumber = RandomNumberGenerator.GetInt32(0, 2);
                var hmac = HMACGenerator.GenerateHMACSHA256(key, randomComputerNumber.ToString());
                int userChoice = 0;

                Console.WriteLine($"(HMAC={hmac}).");
                Console.WriteLine("Try to guess my selection.");
                ShowMenu(["0", "1"]);
                while (!TryParseInput(2, ref userChoice)) ;
                Console.WriteLine($"My selection: {randomComputerNumber} (KEY={key}).");

                if (userChoice == randomComputerNumber)
                {
                    Console.WriteLine("You guess my selection correctly.");
                    Console.WriteLine("Choose the dice first.");
                    ShowMenu(_dices.Select(dice => dice.ToString()).ToArray());
                    while (!TryParseInput(_dices.Length, ref userChoice)) ;
                    var userDice = _dices[userChoice];
                    Console.WriteLine($"You choose the {userDice} dice.");
                    var computerDice = _dices[ChooseBestDice(userChoice)];
                    Console.WriteLine($"I choose {computerDice} dice.");

                    Console.WriteLine($"It's time for your roll.");
                    randomComputerNumber = RandomNumberGenerator.GetInt32(0, countFaces);
                    key = HMACGenerator.GenerateRandomKey(32);
                    hmac = HMACGenerator.GenerateHMACSHA256(key, randomComputerNumber.ToString());
                    Console.WriteLine($"I selected a random value in the range 0..{countFaces - 1}");
                    Console.WriteLine($"(HMAC={hmac}).");
                    Console.WriteLine($"Add your number modulo {countFaces}.");
                    ShowMenu(Enumerable.Range(0, countFaces).Select(x => x.ToString()).ToArray());
                    while (!TryParseInput(countFaces, ref userChoice)) ;
                    Console.WriteLine($"My number is {randomComputerNumber} (KEY={key}).");
                    var result = (randomComputerNumber + userChoice) % countFaces;
                    Console.WriteLine($"The fair number generation result is {randomComputerNumber} + {userChoice} = {result} (mod {countFaces}).");
                    var userRoll = userDice.GetNumber(result);
                    Console.WriteLine($"Your roll result is {userRoll}.");

                    Console.WriteLine("It's time for my roll.");
                    randomComputerNumber = RandomNumberGenerator.GetInt32(0, countFaces);
                    key = HMACGenerator.GenerateRandomKey(32);
                    hmac = HMACGenerator.GenerateHMACSHA256(key, randomComputerNumber.ToString());
                    Console.WriteLine($"I selected a random value in the range 0..{countFaces - 1}");
                    Console.WriteLine($"(HMAC={hmac}).");
                    Console.WriteLine($"Add your number modulo {countFaces}.");
                    ShowMenu(Enumerable.Range(0, countFaces).Select(x => x.ToString()).ToArray());
                    while (!TryParseInput(countFaces, ref userChoice)) ;
                    Console.WriteLine($"My number is {randomComputerNumber} (KEY={key}).");
                    result = (randomComputerNumber + userChoice) % countFaces;
                    Console.WriteLine($"The fair number generation result is {randomComputerNumber} + {userChoice} = {result} (mod {countFaces}).");
                    var computerRoll = computerDice.GetNumber(result);
                    Console.WriteLine($"My roll result is {computerRoll}.");

                    if (userRoll > computerRoll)
                        Console.WriteLine($"You win ({userRoll} > {computerRoll})!");
                    else if (userRoll == computerRoll)
                        Console.WriteLine($"It's draw ({userRoll} = {computerRoll})!");
                    else
                        Console.WriteLine($"I win ({computerRoll} > {userRoll})!");
                }
                else
                {
                    var computerDice = _dices[ChooseBestDice()];
                    Console.WriteLine($"I make the first move and choose the {computerDice} dice.");
                    Console.WriteLine("Choose your dice.");
                    var remainingDices = _dices.Where(dice => dice != computerDice);
                    ShowMenu(remainingDices.Select(dice => dice.ToString()).ToArray());
                    while (!TryParseInput(_dices.Length - 1, ref userChoice)) ;
                    var userDice = remainingDices.ElementAt(userChoice);
                    Console.WriteLine($"You choose the {userDice} dice.");
                
                    Console.WriteLine("It's time for my roll.");
                    randomComputerNumber = RandomNumberGenerator.GetInt32(0, countFaces);
                    key = HMACGenerator.GenerateRandomKey(32);
                    hmac = HMACGenerator.GenerateHMACSHA256(key, randomComputerNumber.ToString());
                    Console.WriteLine($"I selected a random value in the range 0..{countFaces - 1}");
                    Console.WriteLine($"(HMAC={hmac}).");
                    Console.WriteLine($"Add your number modulo {countFaces}.");
                    ShowMenu(Enumerable.Range(0, countFaces).Select(x => x.ToString()).ToArray());
                    while (!TryParseInput(countFaces, ref userChoice)) ;
                    Console.WriteLine($"My number is {randomComputerNumber} (KEY={key}).");
                    var result = (randomComputerNumber + userChoice) % countFaces;
                    Console.WriteLine($"The fair number generation result is {randomComputerNumber} + {userChoice} = {result} (mod {countFaces}).");
                    var computerRoll = computerDice.GetNumber(result);
                    Console.WriteLine($"My roll result is {computerRoll}.");
                
                    Console.WriteLine($"It's time for your roll.");
                    randomComputerNumber = RandomNumberGenerator.GetInt32(0, countFaces);
                    key = HMACGenerator.GenerateRandomKey(32);
                    hmac = HMACGenerator.GenerateHMACSHA256(key, randomComputerNumber.ToString());
                    Console.WriteLine($"I selected a random value in the range 0..{countFaces - 1}");
                    Console.WriteLine($"(HMAC={hmac}).");
                    Console.WriteLine($"Add your number modulo {countFaces}.");
                    ShowMenu(Enumerable.Range(0, countFaces).Select(x => x.ToString()).ToArray());
                    while (!TryParseInput(countFaces, ref userChoice)) ;
                    Console.WriteLine($"My number is {randomComputerNumber} (KEY={key}).");
                    result = (randomComputerNumber + userChoice) % countFaces;
                    Console.WriteLine($"The fair number generation result is {randomComputerNumber} + {userChoice} = {result} (mod {countFaces}).");
                    var userRoll = userDice.GetNumber(result);
                    Console.WriteLine($"Your roll result is {userRoll}.");

                    if(userRoll > computerRoll)
                        Console.WriteLine($"You win ({userRoll} > {computerRoll})!");
                    else if (userRoll == computerRoll)
                        Console.WriteLine($"It's draw ({userRoll} = {computerRoll})!");
                    else 
                        Console.WriteLine($"I win ({computerRoll} > {userRoll})!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }

        private static void ShowMenu(string[] userChoices)
        {
            for(int i = 0; i < userChoices.Length; i++)
            {
                Console.WriteLine($"{i} - {userChoices[i]}");
            }
            Console.WriteLine("? - help");
            Console.WriteLine("x - exit");
        }

        private static bool TryParseInput(int maxSelectionNumber, ref int result)
        {
            Console.WriteLine("Your choice: ");
            var answer = Console.ReadLine().ToLower();
            int answerInt = 0;

            if (String.Compare(answer, "x") == 0)
                Environment.Exit(0);
            else if (String.Compare(answer, "?") == 0)
            {
                Console.WriteLine("Probability of the win fоr the user:");
                ConsoleTableDrawer.DrawProbabilitiesTable(_winProbabilities, _dices);
            }
            else if (int.TryParse(answer, out answerInt) && answerInt > -1 && answerInt < maxSelectionNumber)
            {
                result = answerInt;
                Console.WriteLine($"Your selection: {result}");
                return true;
            }
            else
                Console.WriteLine("There is no such menu item.");

            return false;
        }

        private static int ChooseBestDice(int exceptionDice = -1)
        {
            var totalWinProbabilityForDice = new double[_dices.Length];

            for(int i = 0; i < _winProbabilities.GetLength(0); i++)
            {
                if (i == exceptionDice)
                    break;
                for(int j = 0;  j < _winProbabilities.GetLength(1); j++)
                {
                    totalWinProbabilityForDice[j] += _winProbabilities[i, j];
                }
            }

            return Array.IndexOf(totalWinProbabilityForDice, totalWinProbabilityForDice.Max());
        }
    }
}
