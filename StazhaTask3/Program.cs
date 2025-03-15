namespace InternshipTask3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //foreach (string arg in args)
            //{
            //    Console.WriteLine(arg);
            //}

            var dices = ConsoleParser.Parse(args);
            var winProbabilities = WinningProbabilitiesCalculator.Calculate(dices);

            ConsoleTableDrawer.DrawProbabilitiesTable(winProbabilities, args);
        }
    }
}
